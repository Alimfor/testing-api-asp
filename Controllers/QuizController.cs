using Exam.Services;
using Exam.Utils;
using Exam.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IAnswerOptionService _answerOptionService;
    private readonly IQuestionService _questionService;

    public QuizController(IAnswerOptionService answerOptionService, IQuestionService questionService)
    {
        _answerOptionService = answerOptionService;
        _questionService = questionService;
    }

    private const string GET_QUIZ = "start";
        
    [HttpGet(GET_QUIZ)]
    public IActionResult GetQuiz()
    {
        var questionOperRes =
            _questionService.GetAllQuestions();
        var result = questionOperRes.result;
        var questions = questionOperRes.data;

        var qEnumerable = questions.ToList();
        if (!qEnumerable.Any() && result.code == 200)
            return NoContent();

        var answerOptionOperRes = 
            _answerOptionService.GetAllAnswerOption();
        var answerOptionResult = answerOptionOperRes.result;
        var answerOptions = answerOptionOperRes.data;
            
        var anOptEnumerable = answerOptions.ToList();
        if (!anOptEnumerable.Any() && answerOptionResult.code == 200)
            return NoContent();

        var anwerOptionDtos =
            anOptEnumerable.ToAnswerOptionDto(qEnumerable);

        return ActionResultUtil.ResultState(
            this, result, anwerOptionDtos
        );
    }
}