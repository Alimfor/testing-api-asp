using System.Globalization;
using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public OperationResult<IEnumerable<Question>> GetAllQuestions()
    {
        return _questionRepository.GetAll();
    }

    public OperationResult<Question> GetQuestionById(int id)
    {
        return _questionRepository.GetById(id);
    }

    public ResponseResult AddQuestion(Question question)
    {
        return _questionRepository.Add(question);
    }

    public ResponseResult UpdateQuestion(Question question)
    {
        return _questionRepository.Update(question);
    }

    public ResponseResult DeleteQuestion(int id)
    {
        return _questionRepository.Delete(id);
    }
}