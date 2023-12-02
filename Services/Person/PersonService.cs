using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public OperationResult<IEnumerable<Person>> GetAllPersons()
    {
        return _personRepository.GetAll();
    }

    public OperationResult<Person> GetPersonById(int id)
    {
        return _personRepository.GetById(id);
    }

    public ResponseResult AddPerson(Person person)
    {
        return _personRepository.Add(person);
    }

    public ResponseResult UpdatePerson(Person person)
    {
        return _personRepository.Update(person);
    }

    public ResponseResult DeletePerson(int id)
    {
        return _personRepository.Delete(id);
    }
}