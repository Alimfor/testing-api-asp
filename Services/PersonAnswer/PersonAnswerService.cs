using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class PersonAnswerService : IPersonAnswerService
{
    private readonly IPersonAnswerRepository _personAnswerRepository;

    public PersonAnswerService(IPersonAnswerRepository personAnswerRepository)
    {
        _personAnswerRepository = personAnswerRepository;
    }

    public OperationResult<IEnumerable<PersonAnswer>> GetAllPersonAnswers()
    {
        return _personAnswerRepository.GetAll();
    }

    public OperationResult<PersonAnswer> GetPersonAnswerById(int id)
    {
        return _personAnswerRepository.GetById(id);
    }

    public ResponseResult AddPersonAnswer(PersonAnswer personAnswer)
    {
        return _personAnswerRepository.Add(personAnswer);
    }

    public ResponseResult UpdatePersonAnswer(PersonAnswer personAnswer)
    {
        return _personAnswerRepository.Update(personAnswer);
    }

    public ResponseResult DeletePersonAnswer(int id)
    {
        return _personAnswerRepository.Delete(id);
    }
}