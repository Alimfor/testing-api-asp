using Exam.DTO;
using Exam.Entities;

namespace Exam.Utils.Extensions;

public static class TestSessionExtension
{
    public static TestSessionDto ToTestSessionDto(this TestSession testSession)
        => new()
        {
            TestStartDate = testSession.TestStartDate,
            TestEndDate = testSession.TestEndDate
        };

    public static TestSession ToTestSession(this TestSessionDto testSessionDto)
        => new()
        {
            TestStartDate = testSessionDto.TestStartDate,
            TestEndDate = testSessionDto.TestEndDate
        };
}