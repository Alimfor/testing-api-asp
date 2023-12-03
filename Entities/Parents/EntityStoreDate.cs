namespace Exam.Utils;

public class EntityStoreDate
{
    private DateTime _createdAt;
    private DateTime _updatedAt;

    public DateTime CreatedAt
    {
        get => DateTime.Now;
        set => _createdAt = value;
    }

    public DateTime UpdatedAt
    {
        get => DateTime.Now;
        set => _updatedAt = value;
    }
}