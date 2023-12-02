using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class PersonAnswerRepository : DapperRepository<PersonAnswer>, IPersonAnswerRepository
{
    private readonly IConfiguration _configuration;
    
    public PersonAnswerRepository(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }
    
    public ResponseResult Add(PersonAnswer entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pAddPersonAnswer",
                new
                {
                    entity.PersonAnswerText,entity.PersonId,entity.QuestionId,entity.TestSessionId,
                    entity.CreatedAt,entity.UpdatedAt
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

    public ResponseResult Update(PersonAnswer entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pUpdatePersonAnswer",
                new
                {
                    entity.PersonAnswerId,
                    entity.PersonAnswerText,entity.PersonId,entity.QuestionId,entity.TestSessionId,
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