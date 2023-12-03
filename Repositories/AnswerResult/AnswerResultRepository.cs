using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class AnswerResultRepository : DapperByEmailRepository<AnswerResult>, IAnswerResultRepository
{
    public AnswerResultRepository(IConfiguration configuration) : base(configuration)
    {
    }
}