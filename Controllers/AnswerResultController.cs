using Exam.Entities;
using Exam.Services;
using Exam.Utils;
using Exam.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswerResultController : ControllerBase
{
    private readonly IAnswerResultService _answerResultService;

    public AnswerResultController(IAnswerResultService answerResultService)
    {
        _answerResultService = answerResultService;
    }

    private const string GET_ALL_ANSWER_RESULTS = "all";
    private const string GET_ANSWER_RESULT_BY_ID = "{id}";
        
    [HttpGet(GET_ALL_ANSWER_RESULTS)]
    public IActionResult GetAllAnswerResults()
    {
        var operationResult = _answerResultService.GetAllAnswerResults();
        var result = operationResult.result;
        var answerResults = operationResult.data;

        var enumerable = answerResults.ToList();
        if (!enumerable.Any() && result.code == 200)
        {
            return Ok(Enumerable.Empty<Person>());
        }

        var answerResultResponseDtos =
            enumerable.Select(
                answerResult => answerResult.ToAnswerResultResponseDto()
            );

        return ActionResultUtil.ResultState(
            this, result, answerResultResponseDtos
        );
    }

    [HttpGet(GET_ANSWER_RESULT_BY_ID)]
    public IActionResult GetAnswerResultById(int id)
    {
        var operationResult = _answerResultService.GetAnswerResultById(id);
        var result = operationResult.result;
            
        if (result.code != 200)
            return StatusCode(result.code, result.message);
            
        var answerResultResponseDto = 
            operationResult.data.ToAnswerResultResponseDto();
        return ActionResultUtil.ResultState(
            this, result, answerResultResponseDto
        );
    }
}