﻿using System.ComponentModel.DataAnnotations.Schema;
using AuthService.Domain.Common;
using AuthService.Domain.Events;
using AuthService.Domain.Events.Users;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IBaseAuditableEntity, IBaseEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    public DateTimeOffset DeleteAt { get; set; }
    public bool DeleteFlag { get; set; }
    private readonly List<BaseEvent> _domainEvents = [];
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public ApplicationUser() : base()
    {
        CreatedAt = DateTimeOffset.UtcNow;
        LastModifiedAt = DateTimeOffset.UtcNow;
        DeleteFlag = false;
    }

    public ApplicationUser(string username, string email) : this()
    {
        UserName = username;
        Email = email;
        AddDomainEvent(new UserRegisteredEvent(Id, Email));
    }

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
