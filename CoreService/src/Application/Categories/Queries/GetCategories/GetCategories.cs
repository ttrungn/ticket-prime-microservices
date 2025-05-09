using CoreService.Application.Categories.Queries.GetCategories.Dtos;
using CoreService.Application.Common.Interfaces;
using CoreService.Application.Common.Models;

namespace CoreService.Application.Categories.Queries.GetCategories;

public record GetCategoriesQuery : IRequest<GetCategoriesResult>
{
}

public record GetCategoriesResult()
{
    public string Message { get; init; } = null!;
    public int Total { get; init; }
    public IReadOnlyCollection<CategoryDto> Results { get; init; } = [];
};

public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
{
    public GetCategoriesQueryValidator()
    {
    }
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categoriesDtos = await _context.Categories
            .Include(c => c.SubCategories)
            .AsNoTracking()
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new GetCategoriesResult()
        {
            Message = "success",
            Total = categoriesDtos.Count,
            Results = categoriesDtos
        };
    }
}
