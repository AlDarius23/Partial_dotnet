using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class BookingRepository(RoomHubDbContext dbContext) : IBookingRepository
{
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Bookings.FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Booking>> GetActiveByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Bookings
            .Where(booking => booking.RoomId == roomId && booking.Status == BookingStatus.Active)
            .ToListAsync(cancellationToken);
    }

    public void Add(Booking booking)
    {
        dbContext.Bookings.Add(booking);
    }
}
