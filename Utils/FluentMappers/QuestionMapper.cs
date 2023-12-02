using Dapper.FluentMap.Mapping;
using Exam.Entities;

namespace Exam.Utils.Mapper;

public class QuestionMapper : EntityMap<Question>
{
    public QuestionMapper()
    {
        Map(question => question.QuestionId).ToColumn("question_id");
        Map(question => question.QuestionText).ToColumn("question_text");
        Map(question => question.CorrectAnswer).ToColumn("correct_answer");
    }
}