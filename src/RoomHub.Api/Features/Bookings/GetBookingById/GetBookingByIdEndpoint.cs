using RoomHub.Api.Models;
using RoomHub.Api.Services;

namespace RoomHub.Api.Features.Bookings.GetBookingById;

public record GetBookingByIdRequest(Guid Id);

public record GetBookingByIdResponse(Guid Id, Guid RoomId, string BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees, BookingStatus Status);

public static class GetBookingByIdEndpoint
{
    public static RouteHandlerBuilder MapGetBookingById(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id:guid}", (Guid id, BookingService service) =>
        {
            var booking = service.Get(id);
            var response = new GetBookingByIdResponse(booking.Id, booking.RoomId, booking.BookedBy, booking.StartUtc, booking.EndUtc, booking.Attendees, booking.Status);
            return TypedResults.Ok(response);
        });
    }
}
