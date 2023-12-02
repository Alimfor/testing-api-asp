using Dapper.FluentMap.Mapping;
using Exam.Entities;

namespace Exam.Utils.Mapper;

public class PersonMapper : EntityMap<Person>
{
    public PersonMapper()
    {
        Map(person => person.PersonId).ToColumn("person_id");
        Map(person => person.Email).ToColumn("email");
        Map(person => person.FirstName).ToColumn("first_name");
        Map(person => person.LastName).ToColumn("last_name");
    }
}