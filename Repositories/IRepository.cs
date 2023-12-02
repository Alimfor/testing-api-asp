using Exam.Entities;
using Exam.Utils;

namespace Exam.Repositories;

public interface IRepository<T>
{
    OperationResult<IEnumerable<T>> GetAll();
    OperationResult<T> GetById(int id);
    ResponseResult Delete(int id);
}