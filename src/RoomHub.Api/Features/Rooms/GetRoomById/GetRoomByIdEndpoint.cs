using RoomHub.Api.Services;

namespace RoomHub.Api.Features.Rooms.GetRoomById;

public record GetRoomByIdRequest(Guid Id);

public record GetRoomByIdResponse(Guid Id, string Name, int Capacity);

public static class GetRoomByIdEndpoint
{
    public static RouteHandlerBuilder MapGetRoomById(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id:guid}", (Guid id, RoomService service) =>
        {
            var room = service.Get(id);
            var response = new GetRoomByIdResponse(room.Id, room.Name, room.Capacity);
            return TypedResults.Ok(response);
        });
    }
}
