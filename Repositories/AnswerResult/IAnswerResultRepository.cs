using Exam.Entities;
using Exam.Utils;
using NuGet.Protocol.Core.Types;

namespace Exam.Repositories;

public interface IAnswerResultRepository : IRepository<AnswerResult>
{
    ResponseResult Add(AnswerResult entity);
    ResponseResult Update(AnswerResult entity);
}