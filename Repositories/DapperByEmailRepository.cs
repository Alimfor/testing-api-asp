using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Utils;
using Exam.Utils.StringMutationForRepository;

namespace Exam.Repositories;

public class DapperByEmailRepository<T> : DapperRepository<T>, IRequestByEmailReposiotory<T>
{
    private readonly IConfiguration _configuration;

    public DapperByEmailRepository(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

    public OperationResult<T> GetByEmail(string personEmail)
    {
        var defaultEntity = Activator.CreateInstance<T>();
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));

            var TableName = ConvertToSpecifyCase.ConvertToSnakeCase(typeof(T).Name);
            var responseEntity = connection.QueryFirstOrDefault<T>(
                "GetTByPersonEmail",
                new { TableName, Email = personEmail },
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
}