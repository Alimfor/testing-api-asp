using Exam.DTO;
using Exam.Services;
using Exam.Utils;
using Exam.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    private const string GET_ALL_QUESTIONS = "all";
    private const string GET_QUESTION_BY_ID = "{id}";
    private const string POST_ADD_QUESTION = "new";
    private const string PUT_UPDATE_QUESTION = "renew";
    private const string DELETE_QUESTION_BY_ID = "{id}";
        
    [HttpGet(GET_ALL_QUESTIONS)]
    public IActionResult GetAllQuestions()
    {
        var operationResult = _questionService.GetAllQuestions();
        var result = operationResult.result;
        var questions = operationResult.data;

        var enumerable = questions.ToList();
        if (!enumerable.Any() && result.code == 200)
            return NoContent();

        var questionResponseDtos = enumerable.Select(
            question => question.ToQuestionDto()
        );

        return ActionResultUtil.ResultState(
            this, result, questionResponseDtos
        );
    }

    [HttpGet(GET_QUESTION_BY_ID)]
    public IActionResult GetQuestionById(int id)
    {
        var operationResult = _questionService.GetQuestionById(id);
        var result = operationResult.result;
            
        if (result.code != 200)
            return StatusCode(result.code, result.message);
            
        var authorDto = operationResult.data.ToQuestionDto();
        return ActionResultUtil.ResultState(this, result,authorDto);
    }

    [HttpPost(POST_ADD_QUESTION)]
    public IActionResult AddQuestion([FromBody] QuestionResponseDto questionResponseDto)
    {
        var result = _questionService.AddQuestion(questionResponseDto.ToQuestion());
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpPut(PUT_UPDATE_QUESTION)]
    public IActionResult UpdateQuestion([FromBody] QuestionRequestDto questionRequestDto)
    {
        var result = _questionService.UpdateQuestion(questionRequestDto.ToQuestion());
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpDelete(DELETE_QUESTION_BY_ID)]
    public IActionResult DeleteQuestion(int id)
    {
        var result = _questionService.DeleteQuestion(id);
        return ActionResultUtil.ResultState(this, result);
    }
}