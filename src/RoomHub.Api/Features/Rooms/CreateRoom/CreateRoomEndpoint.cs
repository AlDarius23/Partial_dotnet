using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public record CreateRoomRequest(string? Name, int Capacity);

public record CreateRoomResponse(Guid Id, string Name, int Capacity);

public static class CreateRoomEndpoint
{
    public static RouteHandlerBuilder MapCreateRoom(this IEndpointRouteBuilder app)
    {
        return app.MapPost("", async Task<IResult> (CreateRoomRequest request, ICommandHandler<CreateRoomCommand, Domain.Rooms.Room> handler, CancellationToken cancellationToken) =>
        {
            var command = new CreateRoomCommand(request.Name ?? string.Empty, request.Capacity);
            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error!.ToProblem();
            }

            var room = result.Value;
            var response = new CreateRoomResponse(room.Id, room.Name, room.Capacity);
            return TypedResults.Created($"/api/rooms/{room.Id}", response);
        });
    }
}
