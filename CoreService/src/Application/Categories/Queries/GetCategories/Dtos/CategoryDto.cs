using CoreService.Application.Common.Models;
using CoreService.Domain.Entities;

namespace CoreService.Application.Categories.Queries.GetCategories.Dtos;

public class CategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public IReadOnlyCollection<SubCategoryDto> SubCategories { get; init; } = null!;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SubCategory, SubCategoryDto>();

            CreateMap<Category, CategoryDto>();
        }
    }
}
