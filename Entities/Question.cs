using Exam.Utils;

namespace Exam.Entities;

public class Question : EntityStoreDate
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string CorrectAnswer { get; set; }
}