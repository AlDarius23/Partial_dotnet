using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public record CancelBookingRequest(Guid Id);

public record CancelBookingResponse;

public static class CancelBookingEndpoint
{
    public static RouteHandlerBuilder MapCancelBooking(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/{id:guid}/cancel", async Task<IResult> (Guid id, ICommandHandler<CancelBookingCommand, Unit> handler, CancellationToken cancellationToken) =>
        {
            var command = new CancelBookingCommand(id);
            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error!.ToProblem();
            }

            return TypedResults.NoContent();
        });
    }
}
