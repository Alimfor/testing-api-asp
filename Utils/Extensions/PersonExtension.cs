using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class PersonExtension
{
    public static PersonResponseDto ToPersonDto(this Person person)
    {
        return new PersonResponseDto
        {
            Email = person.Email,
            FirstName = person.FirstName,
            LastName = person.LastName
        };
    }

    public static Person ToPerson(this PersonResponseDto personResponseDto)
        => fillInstance(personResponseDto);

    public static Person ToPerson(this PersonRequestDto personRequestDto)
    {
        var person = fillInstance(personRequestDto);
        person.PersonId = personRequestDto.PersonId;

        return person;
    }

    private static Person fillInstance(PersonResponseDto instance)
    {
        return new Person
        {
            Email = instance.Email,
            FirstName = instance.FirstName,
            LastName = instance.LastName
        };
    }
}