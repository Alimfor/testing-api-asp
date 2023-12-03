using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class PersonAnswerService : IPersonAnswerService
{
    private readonly IPersonAnswerRepository _personAnswerRepository;

    public PersonAnswerService(IPersonAnswerRepository personAnswerRepository)
        => _personAnswerRepository = personAnswerRepository;

    public OperationResult<IEnumerable<PersonAnswer>> GetAllPersonAnswers()
        => _personAnswerRepository.GetAll();

    public OperationResult<PersonAnswer> GetPersonAnswerById(int id)
        => _personAnswerRepository.GetById(id);

    public ResponseResult AddPersonAnswer(PersonAnswer personAnswer)
        => _personAnswerRepository.Add(personAnswer);

    public ResponseResult UpdatePersonAnswer(PersonAnswer personAnswer)
        => _personAnswerRepository.Update(personAnswer);

    public ResponseResult DeletePersonAnswer(int id)
        => _personAnswerRepository.Delete(id);
}