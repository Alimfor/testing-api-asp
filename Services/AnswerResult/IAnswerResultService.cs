using Exam.Entities;
using Exam.Utils;

namespace Exam.Services;

public interface IAnswerResultService
{
    OperationResult<IEnumerable<AnswerResult>> GetAllAnswerResults();
    OperationResult<AnswerResult> GetAnswerResultById(int id);
    ResponseResult AddAnswerResult(AnswerResult answerResult);
    ResponseResult UpdateAnswerResult(AnswerResult answerResult);
    ResponseResult DeleteAnswerResult(int id);
}