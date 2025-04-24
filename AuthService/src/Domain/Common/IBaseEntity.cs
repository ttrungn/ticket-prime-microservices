namespace AuthService.Domain.Common
{
    public interface IBaseEntity
    {
        IReadOnlyCollection<BaseEvent> DomainEvents { get; }
        void AddDomainEvent(BaseEvent domainEvent);
        void RemoveDomainEvent(BaseEvent domainEvent);
        void ClearDomainEvents();
    }
}
