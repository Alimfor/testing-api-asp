using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class AnswerResultService : IAnswerResultService
{
    private readonly IAnswerResultRepository _answerResultRepository;

    public AnswerResultService(IAnswerResultRepository answerResultRepository)
     => _answerResultRepository = answerResultRepository;

    public OperationResult<IEnumerable<AnswerResult>> GetAllAnswerResults()
     => _answerResultRepository.GetAll();

    public OperationResult<AnswerResult> GetAnswerResultById(int id)
     => _answerResultRepository.GetById(id);

    public ResponseResult AddAnswerResult(AnswerResult session)
     => _answerResultRepository.Add(session);

    public ResponseResult UpdateAnswerResult(AnswerResult session)
     => _answerResultRepository.Update(session);

    public ResponseResult DeleteAnswerResult(int id)
     => _answerResultRepository.Delete(id);

    public OperationResult<AnswerResult> GetAnswerResultByPersonEmail(string email)
     => _answerResultRepository.GetByEmail(email);
}