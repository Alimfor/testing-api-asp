namespace Exam.Utils;

public class OperationResult<T>
{
    public T data { get; init; }
    public ResponseResult result { get; init; }
}