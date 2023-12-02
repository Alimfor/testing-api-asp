using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Exam.Utils;
using Newtonsoft.Json;

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
            
            var TableName = ConvertToSnakeCase(typeof(T).Name);
            
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
            
            var TableName = ConvertToSnakeCase(typeof(T).Name);
            
            var responseEntity = connection.QueryFirstOrDefault<T>(
                "GetTById", 
                new {TableName,Id = id},
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
    
    public ResponseResult Delete(int id)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            
            var TableName = typeof(T).Name;
            var Condition = $"{ConvertToSnakeCase(TableName)}_id = {id}";

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
    
    private string ConvertToSnakeCase(string input)
    {
        var output = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                if (i > 0 && input[i - 1] == 's')
                {
                    output = output.Substring(0, output.Length - 1) + "_";
                }
                else if (i > 0)
                {
                    output += "_";
                }

                output += char.ToLower(input[i]);
            }
            else
            {
                output += input[i];
            }
        }

        return output;
    }
    
}