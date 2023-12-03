using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class AnswerOptionExtension
{
    public static List<AnswerOptionDto> ToAnswerOptionDto(
        this List<AnswerOption> answerOptions,
        List<Question> questions
    )
    {
        var result = new List<AnswerOptionDto>();

        foreach (var question in questions)
        {
            var answerOptionDto = new AnswerOptionDto
            {
                QuestionId = question.QuestionId,
                QuestionText = question.QuestionText,
                CorrectAnswer = question.CorrectAnswer,
                Answers = answerOptions
                    .Where(option => option.QuestionId == question.QuestionId)
                    .Select(option => option.Answer)
                    .Take(4)
                    .ToList()
            };

            result.Add(answerOptionDto);
        }

        return result;
    }

    public static AnswerOption ToAnswerOption(this AnswerOptionAddDto addDto)
        => new()
        {
            Answer = addDto.Answer,
            QuestionId = addDto.QuestionId
        };
}