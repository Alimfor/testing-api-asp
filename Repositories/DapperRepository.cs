using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Utils;
using Exam.Utils.StringMutationForRepository;

namespace Exam.Repositories;

public class DapperRepository<T> : IRepository<T>
{
    private readonly IConfiguration _configuration;

    public DapperRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public OperationResult<IEnumerable<T>> GetAll()
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));

            var TableName = ConvertToSpecifyCase.ConvertToSnakeCase(typeof(T).Name);

            var Tlist = connection.Query<T>(
                "getAllT", new
                {
                    TableName
                },
                commandType: CommandType.StoredProcedure
            );

            return new OperationResult<IEnumerable<T>>
            {
                data = Tlist,
                result = new ResponseResult
                {
                    code = 200,
                    message = "done",
                    status = ResponseStatus.SUCCESSFUL
                }
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<IEnumerable<T>>
            {
                data = Enumerable.Empty<T>(),
                result = new ResponseResult
                {
                    code = 500,
                    message = ex.Message,
                    status = ResponseStatus.INTERNAL_SERVER_ERROR
                }
            };
        }
    }

    public OperationResult<T> GetById(int id)
    {
        var defaultEntity = Activator.CreateInstance<T>();
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));

            var TableName = ConvertToSpecifyCase.ConvertToSnakeCase(typeof(T).Name);
            var responseEntity = connection.QueryFirstOrDefault<T>(
                "GetTById",
                new { TableName, Id = id },
                commandType: CommandType.StoredProcedure
            );

            if (responseEntity != null)
            {
                return new OperationResult<T>
                {
                    data = responseEntity,
                    result = new ResponseResult
                    {
                        code = 200,
                        message = "done",
                        status = ResponseStatus.SUCCESSFUL
                    }
                };
            }

            return new OperationResult<T>
            {
                data = defaultEntity,
                result = new ResponseResult
                {
                    code = 400,
                    message = "one, either all dates is wrong",
                    status = ResponseStatus.WRONG_REQUEST
                }
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<T>
            {
                data = defaultEntity,
                result = new ResponseResult
                {
                    code = 500,
                    message = ex.Message,
                    status = ResponseStatus.INTERNAL_SERVER_ERROR
                }
            };
        }
    }

    public ResponseResult Add(T entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            
            var tableName = ConvertToSpecifyCase.ConvertToSnakeCase(typeof(T).Name);
            var columnValuesJson = GetColumnValues(entity);
            var parameters = new
            {
                TableName = tableName,
                ColumnValues = columnValuesJson
            };

            connection.Execute(
                "InsertTData",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return new ResponseResult
            {
                message = "success",
                code = 200,
                status = ResponseStatus.SUCCESSFUL
            };
        }
        catch (Exception ex)
        {
            return new ResponseResult
            {
                message = ex.Message,
                code = 500,
                status = ResponseStatus.INTERNAL_SERVER_ERROR
            };
        }
    }

    public ResponseResult Update(T entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            
            var tableName = ConvertToSpecifyCase.ConvertToSnakeCase(typeof(T).Name);
            var columnValuesJson = GetUpdateSetValues(entity);
            var idPropertyValue = 
                typeof(T).GetProperty($"{typeof(T).Name}Id")?.GetValue(entity);
            var parameters = new
            {
                TableName = tableName,
                SetColumnValues = columnValuesJson,
                Condition = $"{tableName}_id = {idPropertyValue}"
            };

            connection.Execute(
                "UpdateData",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return new ResponseResult
            {
                message = "success",
                code = 200,
                status = ResponseStatus.SUCCESSFUL
            };
        }
        catch (Exception ex)
        {
            return new ResponseResult
            {
                message = ex.Message,
                code = 500,
                status = ResponseStatus.INTERNAL_SERVER_ERROR
            };
        }
    }

    public ResponseResult Delete(int id)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));

            var TableName = typeof(T).Name;
            var Condition = $"{ConvertToSpecifyCase.ConvertToSnakeCase(TableName)}_id = {id}";

            connection.Execute("DeleteData",
                new { TableName, Condition },
                commandType: CommandType.StoredProcedure
            );

            return new ResponseResult
            {
                message = "success",
                code = 200,
                status = ResponseStatus.SUCCESSFUL
            };
        }
        catch (Exception ex)
        {
            return new ResponseResult
            {
                message = ex.Message,
                code = 500,
                status = ResponseStatus.INTERNAL_SERVER_ERROR
            };
        }
    }

    private string GetColumnValues(T entity)
    {
        var properties = typeof(T).GetProperties()
            .Where(p => p.Name != $"{typeof(T).Name}Id")
            .Select(p => p.GetValue(entity));

        var formattedValues = properties
            .Select(value =>
            {
                if (value is string)
                    return $"'{value}'";

                return value is DateTime time
                    ? $"'{time:dd-MM-yyyy HH:mm:ss}'"
                    : value.ToString();
            });

        return string.Join(", ", formattedValues);
    }

    private string GetUpdateSetValues(T entity)
    {
        var properties = typeof(T).GetProperties()
            .Where(p =>
                p.Name != $"{typeof(T).Name}Id" &&
                p.Name != "CreatedAt"
            )
            .Select(p =>
                $"{ConvertToSpecifyCase.ConvertToSnakeCase(p.Name)} = " +
                (p.PropertyType == typeof(string)
                    ? $"'{p.GetValue(entity)}'"
                    : p.PropertyType == typeof(DateTime)
                        ? $"'{p.GetValue(entity):dd-MM-yyyy HH:mm:ss}'"
                        : p.GetValue(entity).ToString()));

        return string.Join(", ", properties);
    }
}