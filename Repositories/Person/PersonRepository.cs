using System.Data;
using System.Data.SqlClient;
using Dapper;
using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public class PersonRepository : DapperByEmailRepository<Person>, IPersonRepository
{
    public PersonRepository(IConfiguration configuration) : base(configuration)
    {
    }
}