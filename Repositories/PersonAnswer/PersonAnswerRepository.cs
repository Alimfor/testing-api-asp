using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class PersonAnswerRepository : DapperRepository<PersonAnswer>, IPersonAnswerRepository
{
    public PersonAnswerRepository(IConfiguration configuration) : base(configuration)
    {
    }
}