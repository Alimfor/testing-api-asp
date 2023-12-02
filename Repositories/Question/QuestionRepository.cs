using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class QuestionRepository : DapperRepository<Question>, IQuestionRepository
{
    private readonly IConfiguration _configuration;

    public QuestionRepository(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }
    
    public ResponseResult Add(Question entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pAddQuestion",
                new
                {
                    entity.QuestionText,entity.CorrectAnswer,
                    entity.CreatedAt, entity.UpdatedAt
                },
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

    public ResponseResult Update(Question entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pUpdateQuestion",
                new
                {
                    entity.QuestionId,
                    entity.QuestionText,entity.CorrectAnswer,
                    entity.UpdatedAt
                },
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
}