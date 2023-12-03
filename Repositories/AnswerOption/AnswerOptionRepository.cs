using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class AnswerOptionRepository : DapperRepository<AnswerOption>,IAnswerOptionRepository
{
    private readonly IConfiguration _configuration;
    
    public AnswerOptionRepository(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

    public OperationResult<IEnumerable<string>> GetAllOptionsByQuestionId(int questionId)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));

            var answerOptions = connection.Query<string>(
                "getAllOptions", new { questionId },
                commandType: CommandType.StoredProcedure
            );

            return new OperationResult<IEnumerable<string>>
            {
                data = answerOptions,
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
            return new OperationResult<IEnumerable<string>>
            {
                data = Enumerable.Empty<string>(),
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