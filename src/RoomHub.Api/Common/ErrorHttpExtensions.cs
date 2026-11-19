using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Common;

public static class ErrorHttpExtensions
{
    public static ProblemHttpResult ToProblem(this Error error)
    {
        int statusCode;
        if (error.Type == ErrorType.Validation)
        {
            statusCode = StatusCodes.Status400BadRequest;
        }
        else if (error.Type == ErrorType.NotFound)
        {
            statusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            statusCode = StatusCodes.Status409Conflict;
        }

        return TypedResults.Problem(detail: error.Message, statusCode: statusCode, title: error.Code);
    }
}
