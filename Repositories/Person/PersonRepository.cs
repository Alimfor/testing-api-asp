using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class PersonRepository : DapperRepository<Person>, IPersonRepository
{
    private readonly IConfiguration _configuration;
    
    public PersonRepository(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }
    
    public ResponseResult Add(Person entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pAddPerson",
                new
                {
                    entity.Email, entity.FirstName, entity.LastName, entity.UpdatedAt,
                    entity.CreatedAt
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

    public ResponseResult Update(Person entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            connection.Execute("pUpdatePerson",
                new
                {
                    entity.PersonId, entity.Email, entity.FirstName,
                    entity.LastName, entity.UpdatedAt
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