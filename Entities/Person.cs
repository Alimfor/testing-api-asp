using Exam.Utils;

namespace Exam.Entities;

public class Person : EntityStoreDate
{
    public int PersonId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}