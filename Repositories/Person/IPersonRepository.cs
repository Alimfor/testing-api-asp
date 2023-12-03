using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface IPersonRepository : IRepository<Person>, IRequestByEmailReposiotory<Person>
{
}