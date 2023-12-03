using Exam.Utils;

namespace Exam.Entities;

public class PersonAnswer : EntityStoreDate
{
    public int PersonAnswerId { get; set; }
    public string PersonAnswerText { get; set; }
    public int TestSessionId { get; set; }
    public string PersonEmail { get; set; }
    public int QuestionId { get; set; }
}