using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Bookings;

public sealed record DateRange
{
    private DateRange(DateTime startUtc, DateTime endUtc)
    {
        StartUtc = startUtc;
        EndUtc = endUtc;
    }

    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }

    public static Result<DateRange> Create(DateTime startUtc, DateTime endUtc)
    {
        if (endUtc <= startUtc)
        {
            return BookingErrors.InvalidRange();
        }

        if (endUtc - startUtc > TimeSpan.FromHours(4))
        {
            return BookingErrors.TooLong();
        }

        return Result<DateRange>.Success(new DateRange(startUtc, endUtc));
    }

    public bool Overlaps(DateRange other)
    {
        if (StartUtc < other.EndUtc && other.StartUtc < EndUtc)
        {
            return true;
        }

        return false;
    }
}
