using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class RoomRepository(RoomHubDbContext dbContext) : IRoomRepository
{
    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Rooms.FirstOrDefaultAsync(room => room.Id == id, cancellationToken);
    }

    public void Add(Room room)
    {
        dbContext.Rooms.Add(room);
    }
}
