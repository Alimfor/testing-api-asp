using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.DTO;
using Exam.Entities;
using Exam.Services;
using Exam.Utils;
using Exam.Utils.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSessionController : ControllerBase
    {
        private readonly ITestSessionService _testSessionService;

    public TestSessionController(ITestSessionService testSessionService)
    {
        _testSessionService = testSessionService;
    }

    private const string GET_ALL_TEST_SESSIONS = "all";
    private const string GET_TEST_SESSION_BY_ID = "{id}";
    private const string POST_ADD_TEST_SESSION = "new";
    private const string PUT_UPDATE_TEST_SESSION = "renew";
    private const string DELETE_TEST_SESSION_BY_ID = "{id}";
        
    [HttpGet(GET_ALL_TEST_SESSIONS)]
    public IActionResult GetAllTestSessions()
    {
        var operationResult = _testSessionService.GetAllTestSessions();
        var result = operationResult.result;
        var testSessions = operationResult.data;

        var enumerable = testSessions.ToList();
        if (!enumerable.Any() && result.code == 200)
        {
            return Ok(Enumerable.Empty<TestSession>());
        }

        var testSessionDtos = enumerable.Select(
            testSession => testSession.ToTestSessionDto()
        );
            
        return ActionResultUtil.ResultState(this, result,testSessionDtos);
    }

    [HttpGet(GET_TEST_SESSION_BY_ID)]
    public IActionResult GetTestSessionById(int id)
    {
        var operationResult = _testSessionService.GetTestSessionById(id);
        var result = operationResult.result;
            
        if (result.code != 200)
            return ActionResultUtil.ResultState(this, result);
            
        var authorDto = operationResult.data.ToTestSessionDto();
        return ActionResultUtil.ResultState(this, result,authorDto);
    }

    [HttpPost(POST_ADD_TEST_SESSION)]
    public IActionResult AddTestSession([FromBody] TestSessionDto testSessionDto)
    {
        var result = _testSessionService.AddTestSession(testSessionDto.ToTestSession());
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpPut(PUT_UPDATE_TEST_SESSION)]
    public IActionResult UpdateTestSession([FromBody] TestSessionDto testSessionDto)
    {
        var result = _testSessionService.UpdateTestSession(testSessionDto.ToTestSession());
        return ActionResultUtil.ResultState(this, result);
    }

    [HttpDelete(DELETE_TEST_SESSION_BY_ID)]
    public IActionResult DeleteTestSession(int id)
    {
        var result = _testSessionService.DeleteTestSession(id);
        return ActionResultUtil.ResultState(this, result);
    }
    }
}
