using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, Booking>
{
    public async Task<Result<Booking>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var room = await rooms.GetByIdAsync(command.RoomId, cancellationToken);
        if (room is null)
        {
            return Result<Booking>.Failure(RoomErrors.NotFound);
        }

        var activeBookings = await bookings.GetActiveByRoomIdAsync(command.RoomId, cancellationToken);

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
            return Result<Booking>.Failure(result.Error!);
        }

        bookings.Add(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Booking>.Success(result.Value);
    }
}
