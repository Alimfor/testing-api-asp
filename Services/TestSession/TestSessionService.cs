using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class TestSessionService : ITestSessionService
{
    private readonly ITestSessionRepository _testSessionRepository;

    public TestSessionService(ITestSessionRepository testSessionRepository)
    {
        _testSessionRepository = testSessionRepository;
    }

    public OperationResult<IEnumerable<TestSession>> GetAllTestSessions()
    {
        return _testSessionRepository.GetAll();
    }

    public OperationResult<TestSession> GetTestSessionById(int id)
    {
        return _testSessionRepository.GetById(id);
    }

    public OperationResult<int> GetTestSessionIdAfterAdding(TestSession TestSession)
    {
        return _testSessionRepository.GetIdAfterAdding(TestSession);
    }

    public ResponseResult AddTestSession(TestSession TestSession)
    {
        return _testSessionRepository.Add(TestSession);
    }

    public ResponseResult UpdateTestSession(TestSession TestSession)
    {
        return _testSessionRepository.Update(TestSession);
    }

    public ResponseResult DeleteTestSession(int id)
    {
        return _testSessionRepository.Delete(id);
    }
}