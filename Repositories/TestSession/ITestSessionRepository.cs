using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface ITestSessionRepository : IRepository<TestSession>
{
    OperationResult<int> GetIdAfterAdding(TestSession entity);
    OperationResult<TestSession> GetByEmail(string personEmail);
    
}