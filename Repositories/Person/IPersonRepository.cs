using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    ResponseResult Add(Person entity);
    ResponseResult Update(Person entity);
}