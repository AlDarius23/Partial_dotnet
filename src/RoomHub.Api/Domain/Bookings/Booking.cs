using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Domain.Bookings;

public sealed class Booking
{
    private Booking()
    {
    }

    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateRange Range { get; private set; } = null!;
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    public static Result<Booking> Create(
        Room room,
        string bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        DateTime nowUtc,
        IEnumerable<Booking> activeBookingsForRoom)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
        {
            return BookingErrors.InvalidBookedBy();
        }

        var rangeResult = DateRange.Create(startUtc, endUtc);
        if (rangeResult.IsFailure)
        {
            return rangeResult.Error!;
        }

        if (attendees <= 0)
        {
            return BookingErrors.InvalidAttendees();
        }

        if (attendees > room.Capacity)
        {
            return BookingErrors.ExceedsCapacity();
        }

        if (startUtc < nowUtc)
        {
            return BookingErrors.InPast();
        }

        foreach (var existing in activeBookingsForRoom)
        {
            if (existing.Status == BookingStatus.Active && rangeResult.Value.Overlaps(existing.Range))
            {
                return BookingErrors.RoomOccupied();
            }
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = room.Id,
            BookedBy = bookedBy.Trim(),
            Range = rangeResult.Value,
            Attendees = attendees,
            Status = BookingStatus.Active
        };

        return Result<Booking>.Success(booking);
    }

    public Result<Unit> Cancel()
    {
        if (Status == BookingStatus.Cancelled)
        {
            return BookingErrors.AlreadyCancelled();
        }

        Status = BookingStatus.Cancelled;
        return Result<Unit>.Success(Unit.Value);
    }
}
