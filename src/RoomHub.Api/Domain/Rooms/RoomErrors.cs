using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Rooms;

public static class RoomErrors
{
    public static Error InvalidName()
    {
        return Error.Validation("InvalidName", "Room name is required.");
    }

    public static Error InvalidCapacity()
    {
        return Error.Validation("InvalidCapacity", "Capacity must be positive.");
    }

    public static readonly Error NotFound = Error.NotFound("RoomNotFound", "The room does not exist.");
}
