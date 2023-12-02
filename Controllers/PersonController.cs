using Exam.DTO;
using Exam.Entities;
using Exam.Services;
using Exam.Utils;
using Exam.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    private const string GET_ALL_PERSONS = "all";
    private const string GET_PERSON_BY_ID = "{id}";
    private const string POST_ADD_PERSON = "new";
    private const string PUT_UPDATE_PERSON = "renew";
    private const string DELETE_PERSON_BY_ID = "{id}";
        
    [HttpGet(GET_ALL_PERSONS)]
    public IActionResult GetAllPersons()
    {
        var operationResult = _personService.GetAllPersons();
        var result = operationResult.result;
        var persons = operationResult.data;

        var enumerable = persons.ToList();
        if (!enumerable.Any() && result.code == 200)
        {
            return Ok(Enumerable.Empty<Person>());
        }

        var personDtos = enumerable.Select(
            person => person.ToPersonDto()
        );
            
        return ActionResultUtil.ResultState(this, result,personDtos);
    }

    [HttpGet(GET_PERSON_BY_ID)]
    public IActionResult GetPersonById(int id)
    {
        var operationResult = _personService.GetPersonById(id);
        var result = operationResult.result;
            
        if (result.code != 200)
            return ActionResultUtil.ResultState(this, result);
            
        var authorDto = operationResult.data.ToPersonDto();
        return ActionResultUtil.ResultState(this, result,authorDto);
    }

    [HttpPost(POST_ADD_PERSON)]
    public IActionResult AddPerson([FromBody] PersonResponseDto personResponseDto)
    {
        var result = _personService.AddPerson(personResponseDto.ToPerson());
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpPut(PUT_UPDATE_PERSON)]
    public IActionResult UpdatePerson([FromBody] PersonRequestDto personRequestDto)
    {
        var result = _personService.UpdatePerson(personRequestDto.ToPerson());
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpDelete(DELETE_PERSON_BY_ID)]
    public IActionResult DeletePerson(int id)
    {
        var result = _personService.DeletePerson(id);
        return ActionResultUtil.ResultState(this, result);
    }
}