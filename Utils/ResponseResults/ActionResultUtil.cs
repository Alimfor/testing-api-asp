
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Utils;

public static class ActionResultUtil
{
    public static IActionResult ResultState<T>(
        ControllerBase controllerBase,ResponseResult result,T? data
        ) => StatusResult(controllerBase,result,data);
    
    public static IActionResult ResultState(
        ControllerBase controllerBase,ResponseResult result
        ) => StatusResult<object>(controllerBase,result);

    private static IActionResult StatusResult<T>(
        ControllerBase controllerBase, 
        ResponseResult result, T? data = default
    )
    {
        return result.code switch
        {
            200 => controllerBase.Ok(data == null ? result : data),
            204 => controllerBase.NoContent(),
            400 => controllerBase.BadRequest(result.message),
            500 => controllerBase.StatusCode(500, result.message),
            _ => controllerBase.StatusCode(result.code, result.message)
        };
    }
}