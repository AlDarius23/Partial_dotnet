using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Services;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public record CreateRoomRequest(string? Name, int Capacity);

public record CreateRoomResponse(Guid Id, string Name, int Capacity);

public static class CreateRoomEndpoint
{
    public static RouteHandlerBuilder MapCreateRoom(this IEndpointRouteBuilder app)
    {
        return app.MapPost("", (CreateRoomRequest request, RoomService service) =>
        {
            var room = service.Create(request.Name, request.Capacity);
            var response = new CreateRoomResponse(room.Id, room.Name, room.Capacity);
            return TypedResults.Created($"/api/rooms/{room.Id}", response);
        });
    }
}
