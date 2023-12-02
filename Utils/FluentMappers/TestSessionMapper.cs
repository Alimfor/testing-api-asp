using Dapper.FluentMap.Mapping;
using Exam.Entities;

namespace Exam.Utils.Mapper;

public class TestSessionMapper : EntityMap<TestSession>
{
    public TestSessionMapper()
    {
        Map(session => session.TestSessionId).ToColumn("test_session_id");
        Map(session => session.TestStartDate).ToColumn("test_start_date");
        Map(session => session.TestEndDate).ToColumn("test_end_date");
        Map(session => session.CreatedAt).ToColumn("created_at");
    }
}