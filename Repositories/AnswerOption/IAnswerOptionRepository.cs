using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface IAnswerOptionRepository : IRepository<AnswerOption>
{
    OperationResult<IEnumerable<string>> GetAllOptionsByQuestionId(int questionId);
}