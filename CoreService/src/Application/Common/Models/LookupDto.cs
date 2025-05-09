using CoreService.Domain.Entities;

namespace CoreService.Application.Common.Models;

public class LookupDto
{
    public string Id { get; init; } = null!;

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            
        }
    }
}
