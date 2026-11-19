using RoomHub.Api.Services;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public record CancelBookingRequest(Guid Id);

public record CancelBookingResponse;

public static class CancelBookingEndpoint
{
    public static RouteHandlerBuilder MapCancelBooking(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/{id:guid}/cancel", (Guid id, BookingService service) =>
        {
            service.Cancel(id);
            return TypedResults.NoContent();
        });
    }
}
