using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public sealed class CancelBookingHandler(BookingRepository bookings)
    : ICommandHandler<CancelBookingCommand, Unit>
{
    public Task<Result<Unit>> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
    {
        var booking = bookings.Get(command.BookingId);
        if (booking is null)
        {
            return Task.FromResult(Result<Unit>.Failure(BookingErrors.NotFound));
        }

        return Task.FromResult(booking.Cancel());
    }
}
