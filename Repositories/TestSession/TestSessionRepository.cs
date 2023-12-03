using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class TestSessionRepository: DapperRepository<TestSession>, ITestSessionRepository
{
    private readonly IConfiguration _configuration;
    
    public TestSessionRepository(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }  

   public OperationResult<int> GetIdAfterAdding(TestSession entity)
    {
        try
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            
            int insertedId = connection.ExecuteScalar<int>("pGetTestSessionIdAfterAddition",
                new
                {
                    entity.TestStartDate, entity.TestEndDate,
                    entity.CreatedAt
                },
                commandType: CommandType.StoredProcedure
            );
    
            return new OperationResult<int>
            {
                data = insertedId,
                result = new ResponseResult
                {
                    message = "success",
                    code = 200,
                    status = ResponseStatus.SUCCESSFUL
                }
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<int>
            {
                data = -1,
                result = new ResponseResult
                {
                    message = ex.Message,
                    code = 500,
                    status = ResponseStatus.INTERNAL_SERVER_ERROR
                }
            };
        }
    }

   public OperationResult<TestSession> GetByEmail(string personEmail)
   {
       try
       {
           using var connection = new SqlConnection(_configuration.GetConnectionString("conStr"));
            
           var testSession = connection.Query<TestSession>("pGetAnswerResultByPersonEmail",
               new { email = personEmail },
               commandType: CommandType.StoredProcedure
           ).FirstOrDefault();

           if (testSession != null)
           {
               return new OperationResult<TestSession>
               {
                   data = testSession,
                   result = new ResponseResult
                   {
                       message = "success",
                       code = 200,
                       status = ResponseStatus.SUCCESSFUL
                   }
               };
           }
           
           return new OperationResult<TestSession>
           {
               data = new TestSession(),
               result = new ResponseResult
               {
                   message = "wrong request",
                   code = 400,
                   status = ResponseStatus.SUCCESSFUL
               }
           };
       }
       catch (Exception ex)
       {
           return new OperationResult<TestSession>
           {
               data = new TestSession(),
               result = new ResponseResult
               {
                   message = ex.Message,
                   code = 500,
                   status = ResponseStatus.INTERNAL_SERVER_ERROR
               }
           };
       }
   }
}