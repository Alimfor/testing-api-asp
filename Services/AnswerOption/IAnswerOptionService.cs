using Exam.Utils;
using Exam.Entities;

namespace Exam.Services;

public interface IAnswerOptionService
{
    OperationResult<IEnumerable<AnswerOption>> GetAllAnswerOption();
    OperationResult<IEnumerable<string>> GetAllOptionsByQuestionId(int questionId);
    ResponseResult AddAnswerOption(AnswerOption answerOption);
    ResponseResult UpdateAnswerOption(AnswerOption answerOption);
    ResponseResult DeleteAnswerOption(int id);
}