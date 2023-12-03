using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class QuestionExtension
{
    public static QuestionResponseDto ToQuestionDto(this Question question)
        => new()
        {
            QuestionText = question.QuestionText,
            CorrectAnswer = question.CorrectAnswer
        };

    public static Question ToQuestion(this QuestionResponseDto questionResponseDto)
        => fillInstance(questionResponseDto);

    public static Question ToQuestion(this QuestionRequestDto questionRequestDto)
    {
        var question = fillInstance(questionRequestDto);
        question.QuestionId = questionRequestDto.QuestionId;
        return question;
    }

    private static Question fillInstance(QuestionResponseDto questionResponseDto)
        => new()
        {
            QuestionText = questionResponseDto.QuestionText,
            CorrectAnswer = questionResponseDto.CorrectAnswer
        };
}