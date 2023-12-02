using Exam.Entities;
using Exam.Utils;

namespace Exam.Services;

public interface IQuestionService
{
    OperationResult<IEnumerable<Question>> GetAllQuestions();
    OperationResult<Question> GetQuestionById(int id);
    ResponseResult AddQuestion(Question question);
    ResponseResult UpdateQuestion(Question question);
    ResponseResult DeleteQuestion(int id);
}