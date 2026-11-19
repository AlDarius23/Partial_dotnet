using RoomHub.Api.Models;
using RoomHub.Api.Services;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public record CreateBookingRequest(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);

public record CreateBookingResponse(Guid Id, Guid RoomId, BookingStatus Status);

public static class CreateBookingEndpoint
{
    public static RouteHandlerBuilder MapCreateBooking(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/{roomId:guid}/bookings", (Guid roomId, CreateBookingRequest request, BookingService service) =>
        {
            var booking = service.Create(roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees);
            var response = new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status);
            return TypedResults.Created($"/api/bookings/{booking.Id}", response);
        });
    }
}
