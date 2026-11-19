using System.Reflection;

namespace RoomHub.Api.Common.Cqrs;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerInterfaceType = typeof(ICommandHandler<,>);

        var implementations = assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false })
            .SelectMany(type => type.GetInterfaces(), (type, interfaceType) => new { Type = type, InterfaceType = interfaceType })
            .Where(entry => entry.InterfaceType.IsGenericType
                && entry.InterfaceType.GetGenericTypeDefinition() == handlerInterfaceType);

        foreach (var implementation in implementations)
        {
            services.AddScoped(implementation.InterfaceType, implementation.Type);
        }

        return services;
    }
}
