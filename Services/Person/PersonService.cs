using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
        => _personRepository = personRepository;

    public OperationResult<IEnumerable<Person>> GetAllPersons()
        => _personRepository.GetAll();

    public OperationResult<Person> GetPersonById(int id)
        => _personRepository.GetById(id);

    public OperationResult<Person> GetPersonByEmail(string email)
        => _personRepository.GetByEmail(email);

    public ResponseResult AddPerson(Person person)
        => _personRepository.Add(person);

    public ResponseResult UpdatePerson(Person person)
        => _personRepository.Update(person);

    public ResponseResult DeletePerson(int id)
        => _personRepository.Delete(id);
}