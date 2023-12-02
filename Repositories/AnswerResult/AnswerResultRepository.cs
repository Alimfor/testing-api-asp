using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class AnswerResultRepository : DapperRepository<AnswerResult>, IAnswerResultRepository
{
    private readonly IConfiguration _configuration;
    
    public AnswerResultRepository(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }
    
    public ResponseResult Add(AnswerResult entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pAddAnswerResult",
                new
                {
                    entity.CorrectAnsweredCount, entity.IncorrectAnsweredCount,
                    entity.PersonId, entity.CreatedAt, entity.UpdatedAt
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

    public ResponseResult Update(AnswerResult entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pUpdateAnswerResult",
                new
                {
                    entity.AnswerResultId, entity.CorrectAnsweredCount, entity.IncorrectAnsweredCount,
                    entity.PersonId, entity.UpdatedAt
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