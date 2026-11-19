using System.Text.Json.Serialization;
using RoomHub.Api.Features.Bookings;
using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Middleware;
using RoomHub.Api.Repositories;
using RoomHub.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RoomRepository>();
builder.Services.AddSingleton<BookingRepository>();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<BookingService>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/", () => "RoomHub API is running.");
app.MapRooms();
app.MapBookings();

app.Run();
