using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class TestSessionExtension
{
    public static TestSessionDto ToTestSessionDto(this TestSession testSession)
    {
        return new TestSessionDto
        {
            TestStartDate = testSession.TestStartDate,
            TestEndDate = testSession.TestEndDate
        };
    }

    public static TestSession ToTestSession(this TestSessionDto testSessionDto)
    {
        return new TestSession
        {
            TestStartDate = testSessionDto.TestStartDate,
            TestEndDate = testSessionDto.TestEndDate
        };
    }
}