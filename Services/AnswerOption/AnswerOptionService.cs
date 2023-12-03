using Exam.Entities;
using Exam.Repositories;
using Exam.Utils;

namespace Exam.Services;

public class AnswerOptionService : IAnswerOptionService
{
    private readonly IAnswerOptionRepository _optionRepository;

    public AnswerOptionService(IAnswerOptionRepository optionService)
        => _optionRepository = optionService;

    public OperationResult<IEnumerable<AnswerOption>> GetAllAnswerOption()
        => _optionRepository.GetAll();

    public OperationResult<IEnumerable<string>> GetAllOptionsByQuestionId(int questionId)
        => _optionRepository.GetAllOptionsByQuestionId(questionId);

    public ResponseResult AddAnswerOption(AnswerOption answerOption)
        => _optionRepository.Add(answerOption);

    public ResponseResult UpdateAnswerOption(AnswerOption answerOption)
        => _optionRepository.Update(answerOption);

    public ResponseResult DeleteAnswerOption(int id)
        => _optionRepository.Delete(id);
}