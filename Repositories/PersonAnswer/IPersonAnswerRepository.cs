using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface IPersonAnswerRepository : IRepository<PersonAnswer>
{
    ResponseResult Add(PersonAnswer entity);
    ResponseResult Update(PersonAnswer entity);
}