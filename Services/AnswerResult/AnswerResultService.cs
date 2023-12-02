using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class AnswerResultService : IAnswerResultService
{
    private readonly IAnswerResultRepository _answerResultRepository;

    public AnswerResultService(IAnswerResultRepository answerResultRepository)
    {
        _answerResultRepository = answerResultRepository;
    }

    public OperationResult<IEnumerable<AnswerResult>> GetAllAnswerResults()
    {
        return _answerResultRepository.GetAll();
    }

    public OperationResult<AnswerResult> GetAnswerResultById(int id)
    {
        return _answerResultRepository.GetById(id);
    }

    public ResponseResult AddAnswerResult(AnswerResult session)
    {
        return _answerResultRepository.Add(session);
    }

    public ResponseResult UpdateAnswerResult(AnswerResult session)
    {
        return _answerResultRepository.Update(session);
    }

    public ResponseResult DeleteAnswerResult(int id)
    {
        return _answerResultRepository.Delete(id);
    }
}