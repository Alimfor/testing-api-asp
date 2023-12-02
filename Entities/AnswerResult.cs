using Exam.Utils;

namespace Exam.Entities;

public class AnswerResult : EntityStoreDate
{
    public int AnswerResultId { get; set; }
    public int CorrectAnsweredCount { get; set; }
    public int IncorrectAnsweredCount { get; set; }
    public int PersonId { get; set; }
}