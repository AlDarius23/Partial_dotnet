namespace RoomHub.Api.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Booking>> GetActiveByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default);
    void Add(Booking booking);
}
