using Exam.Utils;

namespace Exam.Entities;

public class AnswerOption : EntityStoreDate
{
    public int AnswerOptionId { get; set; }
    public string Answer { get; set; }
    public int QuestionId { get; set; }
}