namespace CoreService.Domain.Common
{
    public interface IBaseAuditableEntity
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset LastModifiedAt { get; set; }
        DateTimeOffset DeleteAt { get; set; }
        bool DeleteFlag { get; set; }
    }
}
