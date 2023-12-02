using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class AnswerResultExtension
{
    public static AnswerResultResponseDto ToAnswerResultResponseDto(
        this AnswerResult answerResult
    )
    {
        return new AnswerResultResponseDto
        {
            CorrectAnsweredCount = answerResult.CorrectAnsweredCount,
            IncorrectAnsweredCount = answerResult.IncorrectAnsweredCount
        };
    }

    public static AnswerResult ToAnswerResult(
        this AnswerResultResponseDto answerResultResponseDto,
        int personId
    )
    {
        return new AnswerResult
        {
            CorrectAnsweredCount = answerResultResponseDto.CorrectAnsweredCount,
            IncorrectAnsweredCount = answerResultResponseDto.IncorrectAnsweredCount,
            PersonId = personId
        };
    }
}