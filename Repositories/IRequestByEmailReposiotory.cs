using Exam.Utils;

namespace Exam.Repositories;

public interface IRequestByEmailReposiotory<T>
{
    OperationResult<T> GetByEmail(string personEmail);
}