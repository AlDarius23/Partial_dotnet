using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(RoomRepository rooms)
    : ICommandHandler<CreateRoomCommand, Room>
{
    public Task<Result<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var result = Room.Create(command.Name, command.Capacity);
        if (result.IsFailure)
        {
            return Task.FromResult(Result<Room>.Failure(result.Error!));
        }

        rooms.Add(result.Value);
        return Task.FromResult(Result<Room>.Success(result.Value));
    }
}
