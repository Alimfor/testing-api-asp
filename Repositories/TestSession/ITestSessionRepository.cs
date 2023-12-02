using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface ITestSessionRepository : IRepository<TestSession>
{
    OperationResult<int> GetIdAfterAdding(TestSession entity);
    ResponseResult Add(TestSession entity);
    ResponseResult Update(TestSession entity);
}