using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.GetBookingById;

public record GetBookingByIdRequest(Guid Id);

public record GetBookingByIdResponse(Guid Id, Guid RoomId, string BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees, BookingStatus Status);

public static class GetBookingByIdEndpoint
{
    public static RouteHandlerBuilder MapGetBookingById(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id:guid}", IResult (Guid id, BookingRepository bookings) =>
        {
            var booking = bookings.Get(id);
            if (booking is null)
            {
                return BookingErrors.NotFound.ToProblem();
            }

            var response = new GetBookingByIdResponse(booking.Id, booking.RoomId, booking.BookedBy, booking.Range.StartUtc, booking.Range.EndUtc, booking.Attendees, booking.Status);
            return TypedResults.Ok(response);
        });
    }
}
