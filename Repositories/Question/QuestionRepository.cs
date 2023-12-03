using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;
using Newtonsoft.Json;

namespace Exam.Repositories;

public class QuestionRepository : DapperRepository<Question>, IQuestionRepository
{
    public QuestionRepository(IConfiguration configuration) : base(configuration)
    {
    }
}