using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public record CreateBookingRequest(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);

public record CreateBookingResponse(Guid Id, Guid RoomId, BookingStatus Status);

public static class CreateBookingEndpoint
{
    public static RouteHandlerBuilder MapCreateBooking(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/{roomId:guid}/bookings", async Task<IResult> (Guid roomId, CreateBookingRequest request, ICommandHandler<CreateBookingCommand, Booking> handler, CancellationToken cancellationToken) =>
        {
            var command = new CreateBookingCommand(roomId, request.BookedBy ?? string.Empty, request.StartUtc, request.EndUtc, request.Attendees);
            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error!.ToProblem();
            }

            var booking = result.Value;
            var response = new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status);
            return TypedResults.Created($"/api/bookings/{booking.Id}", response);
        });
    }
}
