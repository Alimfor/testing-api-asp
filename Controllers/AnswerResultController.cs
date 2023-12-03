using ClosedXML.Excel;
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
    private readonly IPersonService _personService;
    private readonly IPersonAnswerService _personAnswerService;
    private readonly ITestSessionService _testSessionService;

    public AnswerResultController(
        IAnswerResultService answerResultService,
        IPersonService personService,
        IPersonAnswerService personAnswerService,
        ITestSessionService testSessionService
    )
    {
        _answerResultService = answerResultService;
        _personService = personService;
        _personAnswerService = personAnswerService;
        _testSessionService = testSessionService;
    }

    private const string GET_ALL_ANSWER_RESULTS = "all";
    private const string GET_ANSWER_RESULT_BY_PERSON_EMAIL = "{email}";
        
    [HttpGet(GET_ALL_ANSWER_RESULTS)]
    public IActionResult GetAllAnswerResults()
    {
        var operationResult = _answerResultService.GetAllAnswerResults();
        var result = operationResult.result;
        var answerResults = operationResult.data;

        var personOperRes = _personService.GetAllPersons();
        var personResult = personOperRes.result;
        var people = personOperRes.data;
        
        var answerOperRes = _personAnswerService.GetAllPersonAnswers();
        var answerResult = answerOperRes.result;
        var answers = answerOperRes.data;
        
        var sessionOperRes = _testSessionService.GetAllTestSessions();
        var sessionResult = sessionOperRes.result;
        var sessions = sessionOperRes.data;
        
        var enumerable = answerResults.ToList();
        var personEnumerable = people.ToList();
        var answerEnumerable = answers.ToList();
        var sessionEnumerable = sessions.ToList();
        
        if (!enumerable.Any() && result.code == 200 &&
            !personEnumerable.Any() && personResult.code == 200 &&
            !answerEnumerable.Any() && answerResult.code == 200 &&
            !sessionEnumerable.Any() && sessionResult.code == 200
           )
            return NoContent();

        var answerResultResponseDtos = enumerable
            .Join(people, ar => ar.PersonId, p => p.PersonId, (ar, p) => new { AnswerResult = ar, Person = p })
            .Join(sessions, joined => joined.AnswerResult.PersonId, s => s.TestSessionId, (joined, s) => new { AnswerResult = joined.AnswerResult, Person = joined.Person, Session = s })
            .Select(joined => joined.AnswerResult.ToAnswerResultResponseDto(joined.Person, joined.Session))
            .ToList();
        
        return ActionResultUtil.ResultState(
            this, result, answerResultResponseDtos
        );
    }

    [HttpGet(GET_ANSWER_RESULT_BY_PERSON_EMAIL)]
    public IActionResult GetAnswerResultById(string email)
    {
        var answerOperRes = _answerResultService.GetAnswerResultByPersonEmail(email);
        var answerResult = answerOperRes.result;
            
        if (answerResult.code != 200)
            return StatusCode(answerResult.code, answerResult.message);

        var personOperRes = _personService.GetPersonByEmail(email);
        var personResult = answerOperRes.result;
            
        if (personResult.code != 200)
            return StatusCode(personResult.code, personResult.message);

        var sessionOperRes = _testSessionService.GetPersonByEmail(email);
        var sessionResult = answerOperRes.result;
            
        if (sessionResult.code != 200)
            return StatusCode(sessionResult.code, sessionResult.message);

        var person = personOperRes.data;
        var session = sessionOperRes.data;
        var answerResultResponseDto = 
            answerOperRes.data.ToAnswerResultResponseDto(person,session);
        
        byte[] content = null;
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("MySample");
            worksheet.Cell(1, 1).Value = "firstName";
            worksheet.Cell(1, 2).Value = "lastName";
            worksheet.Cell(1, 3).Value = "email";
            worksheet.Cell(1, 4).Value = "correctAnsweredCount";
            worksheet.Cell(1, 5).Value = "incorrectAnsweredCount";
            worksheet.Cell(1, 6).Value = "resultByProcent";
            worksheet.Cell(1, 7).Value = "testStartDate";
            worksheet.Cell(1, 8).Value = "testEndDate";

            worksheet.Cell(2, 1).Value = answerResultResponseDto.FirstName;
            worksheet.Cell(2, 2).Value = answerResultResponseDto.LastName;
            worksheet.Cell(2, 3).Value = answerResultResponseDto.Email;
            worksheet.Cell(2, 4).Value = answerResultResponseDto.CorrectAnsweredCount;
            worksheet.Cell(2, 5).Value = answerResultResponseDto.IncorrectAnsweredCount;
            worksheet.Cell(2, 6).Value = answerResultResponseDto.ResultByProcent;
            worksheet.Cell(2, 7).Value = answerResultResponseDto.TestStartDate;
            worksheet.Cell(2, 8).Value = answerResultResponseDto.TestEndDate;
            
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.SaveAs(ms);
                content = ms.ToArray();
            }
        }

        return File(content,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Processes.xlsx");
    }
}