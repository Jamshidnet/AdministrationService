using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<Category, GetListCategoryResponse>();

        }

    }
}
