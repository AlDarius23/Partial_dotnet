using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(booking => booking.Id);
        builder.Property(booking => booking.Id).ValueGeneratedNever();
        builder.Property(booking => booking.RoomId).IsRequired();
        builder.Property(booking => booking.BookedBy).IsRequired();
        builder.Property(booking => booking.Attendees).IsRequired();
        builder.Property(booking => booking.Status).HasConversion<string>().IsRequired();

        builder.ComplexProperty(booking => booking.Range, rangeBuilder =>
        {
            rangeBuilder.Property(range => range.StartUtc)
                .HasConversion(new UtcDateTimeConverter())
                .IsRequired();
            rangeBuilder.Property(range => range.EndUtc)
                .HasConversion(new UtcDateTimeConverter())
                .IsRequired();
        });
    }
}
