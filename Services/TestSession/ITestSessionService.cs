using Exam.Entities;
using Exam.Utils;

namespace Exam.Services;

public interface ITestSessionService
{
    OperationResult<IEnumerable<TestSession>> GetAllTestSessions();
    OperationResult<TestSession> GetTestSessionById(int id);
    OperationResult<int> GetTestSessionIdAfterAdding(TestSession TestSession);
    OperationResult<TestSession> GetPersonByEmail(string email);
    ResponseResult AddTestSession(TestSession TestSession);
    ResponseResult UpdateTestSession(TestSession TestSession);
    ResponseResult DeleteTestSession(int id);
}