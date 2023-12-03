using Exam.Entities;
using Exam.Utils;

namespace Exam.Services;

public interface IPersonService
{
    OperationResult<IEnumerable<Person>> GetAllPersons();
    OperationResult<Person> GetPersonById(int id);
    OperationResult<Person> GetPersonByEmail(string email);
    ResponseResult AddPerson(Person person);
    ResponseResult UpdatePerson(Person person);
    ResponseResult DeletePerson(int id);
}