using Dapper.FluentMap.Mapping;
using Exam.Entities;

namespace Exam.Utils.Mapper;

public class AnswerOptionMapper : EntityMap<AnswerOption>
{
    public AnswerOptionMapper()
    {
        Map(option => option.AnswerOptionId).ToColumn("answer_option_id");
        Map(option => option.Answer).ToColumn("answer");
        Map(option => option.QuestionId).ToColumn("question_id");
    }
}