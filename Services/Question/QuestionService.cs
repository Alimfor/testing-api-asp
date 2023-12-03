using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
        => _questionRepository = questionRepository;

    public OperationResult<IEnumerable<Question>> GetAllQuestions()
        => _questionRepository.GetAll();


    public OperationResult<Question> GetQuestionById(int id)
        => _questionRepository.GetById(id);


    public ResponseResult AddQuestion(Question question)
        => _questionRepository.Add(question);


    public ResponseResult UpdateQuestion(Question question)
        => _questionRepository.Update(question);


    public ResponseResult DeleteQuestion(int id)
        => _questionRepository.Delete(id);

}