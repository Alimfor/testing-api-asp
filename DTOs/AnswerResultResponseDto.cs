namespace Exam.DTO;

public class AnswerResultResponseDto : TestSessionDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int CorrectAnsweredCount { get; set; }
    public int IncorrectAnsweredCount { get; set; }
    public double ResultByProcent { get; set; }
}