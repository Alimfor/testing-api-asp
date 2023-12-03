namespace Exam.DTO;

public class PersonAnswerRequestDto
{
    public string PersonAnswerText { get; set; }
    public int TestSessionId { get; set; }
    public string PersonEmail { get; set; }
    public int QuestionId { get; set; }
}