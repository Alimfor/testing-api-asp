using Exam.Entities;
using Exam.Utils;

namespace Exam.Services;

public interface IPersonAnswerService
{
    OperationResult<IEnumerable<PersonAnswer>> GetAllPersonAnswers();
    OperationResult<PersonAnswer> GetPersonAnswerById(int id);
    ResponseResult AddPersonAnswer(PersonAnswer personAnswer);
    ResponseResult UpdatePersonAnswer(PersonAnswer personAnswer);
    ResponseResult DeletePersonAnswer(int id);
}