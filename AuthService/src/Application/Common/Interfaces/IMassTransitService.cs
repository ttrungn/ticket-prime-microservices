namespace AuthService.Application.Common.Interfaces
{
    public interface IMassTransitService<TMessage>
    {
        Task Produce(TMessage message, CancellationToken cancellationToken = default);
    }
}
