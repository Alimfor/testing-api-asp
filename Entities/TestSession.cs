namespace Exam.Entities;

public class TestSession
{
    public int TestSessionId { get; set; }
    public DateTime TestStartDate { get; set; }
    public DateTime TestEndDate { get; set; }
    public DateTime CreatedAt
    {
        get => DateTime.Now;
        set => _createdAt = value;
    }
    private DateTime _createdAt;
}