using RoomHub.Api.Features.Bookings.CancelBooking;
using RoomHub.Api.Features.Bookings.CreateBooking;
using RoomHub.Api.Features.Bookings.GetBookingById;

namespace RoomHub.Api.Features.Bookings;

public static class BookingsEndpoints
{
    public static void MapBookings(this IEndpointRouteBuilder app)
    {
        var roomsGroup = app.MapGroup("/api/rooms").WithTags("Bookings");
        CreateBookingEndpoint.MapCreateBooking(roomsGroup);

        var bookingsGroup = app.MapGroup("/api/bookings").WithTags("Bookings");
        GetBookingByIdEndpoint.MapGetBookingById(bookingsGroup);
        CancelBookingEndpoint.MapCancelBooking(bookingsGroup);
    }
}
