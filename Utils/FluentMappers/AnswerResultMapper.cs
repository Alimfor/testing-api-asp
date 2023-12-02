using Dapper.FluentMap.Mapping;
using Exam.Entities;

namespace Exam.Utils.Mapper;

public class AnswerResultMapper : EntityMap<AnswerResult>
{
    public AnswerResultMapper()
    {
        Map(result => result.AnswerResultId).ToColumn("answer_result_id");
        Map(result => result.CorrectAnsweredCount).ToColumn("correct_answered_count");
        Map(result => result.IncorrectAnsweredCount).ToColumn("incorrect_answered_count");
        Map(result => result.PersonId).ToColumn("person_id");
    }
}