using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Bookings;

public static class BookingErrors
{
    public static Error InvalidBookedBy()
    {
        return Error.Validation("InvalidBookedBy", "BookedBy is required.");
    }

    public static Error InvalidRange()
    {
        return Error.Validation("InvalidRange", "The end must be after the start.");
    }

    public static Error InvalidAttendees()
    {
        return Error.Validation("InvalidAttendees", "Attendees must be positive.");
    }

    public static Error ExceedsCapacity()
    {
        return Error.Validation("AttendeesExceedCapacity", "The room does not have enough seats.");
    }

    public static Error InPast()
    {
        return Error.Validation("BookingInPast", "The booking cannot start in the past.");
    }

    public static Error TooLong()
    {
        return Error.Validation("BookingTooLong", "A booking cannot exceed four hours.");
    }

    public static Error RoomOccupied()
    {
        return Error.Conflict("RoomOccupied", "The room is occupied in this interval.");
    }

    public static readonly Error NotFound = Error.NotFound("BookingNotFound", "The booking does not exist.");

    public static Error AlreadyCancelled()
    {
        return Error.Conflict("BookingAlreadyCancelled", "The booking is already cancelled.");
    }
}
