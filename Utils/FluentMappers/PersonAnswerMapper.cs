using Dapper.FluentMap.Mapping;
using Exam.Entities;

namespace Exam.Utils.Mapper;

public class PersonAnswerMapper : EntityMap<PersonAnswer>
{
    public PersonAnswerMapper()
    {
        Map(pAnswer => pAnswer.PersonAnswerId).ToColumn("person_answer_id");
        Map(pAnswer => pAnswer.PersonAnswerText).ToColumn("person_answer_text");
        Map(pAnswer => pAnswer.TestSessionId).ToColumn("test_session_id");
        Map(pAnswer => pAnswer.PersonEmail).ToColumn("person_email");
        Map(pAnswer => pAnswer.QuestionId).ToColumn("question_id");
    }
}