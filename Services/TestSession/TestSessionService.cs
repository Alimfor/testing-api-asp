using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class TestSessionService : ITestSessionService
{
    private readonly ITestSessionRepository _testSessionRepository;

    public TestSessionService(ITestSessionRepository testSessionRepository)
        => _testSessionRepository = testSessionRepository;

    public OperationResult<IEnumerable<TestSession>> GetAllTestSessions()
        => _testSessionRepository.GetAll();


    public OperationResult<TestSession> GetTestSessionById(int id)
        => _testSessionRepository.GetById(id);


    public OperationResult<int> GetTestSessionIdAfterAdding(TestSession TestSession)
        => _testSessionRepository.GetIdAfterAdding(TestSession);

    public OperationResult<TestSession> GetPersonByEmail(string email)
        => _testSessionRepository.GetByEmail(email);

    public ResponseResult AddTestSession(TestSession TestSession)
        => _testSessionRepository.Add(TestSession);


    public ResponseResult UpdateTestSession(TestSession TestSession)
        => _testSessionRepository.Update(TestSession);


    public ResponseResult DeleteTestSession(int id)
        => _testSessionRepository.Delete(id);

}