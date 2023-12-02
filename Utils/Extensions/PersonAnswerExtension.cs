using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class PersonAnswerExtension
{
    public static PersonAnswerResponseDto ToPersonAnswerResponseDto(
        this PersonAnswer personAnswer
    ) => new PersonAnswerResponseDto
        { PersonAnswerText = personAnswer.PersonAnswerText };

    public static PersonAnswer ToPersonAnswer(
        this PersonAnswerRequestDto personAnswerRequestDto
    ) => fillInstance(personAnswerRequestDto);
    
    public static PersonAnswer ToPersonAnswer(
        this PersonAnswerRequestIdDto personAnswerRequest
    )
    {
        var personAnswer = fillInstance(personAnswerRequest);
        personAnswer.PersonAnswerId = personAnswerRequest.PersonAnswerId;
        return personAnswer;
    }

    private static PersonAnswer fillInstance(PersonAnswerRequestDto instance)
    {
        return new PersonAnswer
        {
            PersonAnswerText = instance.PersonAnswerText,
            TestSessionId = instance.TestSessionId,
            PersonId = instance.PersonId,
            QuestionId = instance.QuestionId
        };
    }
}