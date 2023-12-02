using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface IQuestionRepository : IRepository<Question>
{
    ResponseResult Add(Question entity);
    ResponseResult Update(Question entity);
}