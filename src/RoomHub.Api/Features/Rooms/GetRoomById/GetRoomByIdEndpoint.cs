using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.GetRoomById;

public record GetRoomByIdRequest(Guid Id);

public record GetRoomByIdResponse(Guid Id, string Name, int Capacity);

public static class GetRoomByIdEndpoint
{
    public static RouteHandlerBuilder MapGetRoomById(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id:guid}", IResult (Guid id, RoomRepository rooms) =>
        {
            var room = rooms.Get(id);
            if (room is null)
            {
                return RoomErrors.NotFound.ToProblem();
            }

            var response = new GetRoomByIdResponse(room.Id, room.Name, room.Capacity);
            return TypedResults.Ok(response);
        });
    }
}
