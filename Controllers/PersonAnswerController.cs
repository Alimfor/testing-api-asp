using Exam.DTO;
using Exam.Entities;
using Exam.Services;
using Exam.Utils;
using Exam.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonAnswerController : ControllerBase
{
    private readonly IPersonAnswerService _personAnswerService;

    public PersonAnswerController(IPersonAnswerService personAnswerService)
    {
        _personAnswerService = personAnswerService;
    }

    private const string GET_ALL_PERSON_ANSWERS = "all";
    private const string GET_PERSON_ANSWER_BY_ID = "{id}";
    private const string POST_ADD_PERSON_ANSWER = "new";
    private const string PUT_UPDATE_PERSON_ANSWER = "renew";
    private const string DELETE_PERSON_ANSWER_BY_ID = "{id}";
    
    [HttpGet(GET_ALL_PERSON_ANSWERS)]
    public IActionResult GetAllPersonAnswers()
    {
        var operationResult = _personAnswerService.GetAllPersonAnswers();
        var result = operationResult.result;
        var personsAnswers = operationResult.data;

        var enumerable = personsAnswers.ToList();
        if (!enumerable.Any() && result.code == 200)
        {
            return Ok(Enumerable.Empty<Person>());
        }

        var personAnswersDtos =
            enumerable.Select(
                person => person.ToPersonAnswerResponseDto()
            );

        return ActionResultUtil.ResultState(
            this, result, personAnswersDtos
        );
    }

    [HttpGet(GET_PERSON_ANSWER_BY_ID)]
    public IActionResult GetPersonAnswerById(int id)
    {
        var operationResult = _personAnswerService.GetPersonAnswerById(id);
        var result = operationResult.result;
            
        if (result.code != 200)
            return ActionResultUtil.ResultState(this, result);
            
        var authorDto = operationResult.data.ToPersonAnswerResponseDto();
        return ActionResultUtil.ResultState(this, result,authorDto);
    }

    [HttpPost(POST_ADD_PERSON_ANSWER)]
    public IActionResult AddPersonAnswer(
        [FromBody] PersonAnswerRequestDto personAnswerRequestDto
    )
    {
        var result = _personAnswerService.AddPersonAnswer(
            personAnswerRequestDto.ToPersonAnswer()
        );
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpPut(PUT_UPDATE_PERSON_ANSWER)]
    public IActionResult UpdatePersonAnswer(
        [FromBody] PersonAnswerRequestIdDto PersonAnswerRequestIdDto
    )
    {
        var result = _personAnswerService.UpdatePersonAnswer(
            PersonAnswerRequestIdDto.ToPersonAnswer()
        );
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpDelete(DELETE_PERSON_ANSWER_BY_ID)]
    public IActionResult DeletePersonAnswer(int id)
    {
        var result = _personAnswerService.DeletePersonAnswer(id);
        return ActionResultUtil.ResultState(this, result);
    }
        

}