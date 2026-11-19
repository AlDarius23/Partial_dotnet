using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Common.Cqrs;

public interface ICommandHandler<in TCommand, TResult>
{
    Task<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken);
}
