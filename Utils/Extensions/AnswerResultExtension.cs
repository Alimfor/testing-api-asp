using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class AnswerResultExtension
{
    public static AnswerResultResponseDto ToAnswerResultResponseDto(
        this AnswerResult answerResult, Person person, TestSession session
    )
    {
        int totalQuestions = answerResult.CorrectAnsweredCount + answerResult.IncorrectAnsweredCount;
        var percentage = (double) answerResult.CorrectAnsweredCount / totalQuestions * 100;
        return new AnswerResultResponseDto
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            CorrectAnsweredCount = answerResult.CorrectAnsweredCount,
            IncorrectAnsweredCount = answerResult.IncorrectAnsweredCount,
            ResultByProcent = percentage,
            TestStartDate = session.TestStartDate,
            TestEndDate = session.TestEndDate
        };
    }
    
    public static AnswerResult ToAnswerResult(
        this AnswerResultResponseDto answerResultResponseDto,
        int personId
    ) => new()
    {
        CorrectAnsweredCount = answerResultResponseDto.CorrectAnsweredCount,
        IncorrectAnsweredCount = answerResultResponseDto.IncorrectAnsweredCount,
        PersonId = personId
    };
}