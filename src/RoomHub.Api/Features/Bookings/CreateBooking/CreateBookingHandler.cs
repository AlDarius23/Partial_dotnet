using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    RoomRepository rooms,
    BookingRepository bookings,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, Booking>
{
    public Task<Result<Booking>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var room = rooms.Get(command.RoomId);
        if (room is null)
        {
            return Task.FromResult(Result<Booking>.Failure(RoomErrors.NotFound));
        }

        var activeBookings = bookings.ListForRoom(command.RoomId)
            .Where(existing => existing.Status == BookingStatus.Active);

        var result = Booking.Create(
            room,
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            timeProvider.GetUtcNow().UtcDateTime,
            activeBookings);

        if (result.IsFailure)
        {
            return Task.FromResult(Result<Booking>.Failure(result.Error!));
        }

        bookings.Add(result.Value);
        return Task.FromResult(Result<Booking>.Success(result.Value));
    }
}
