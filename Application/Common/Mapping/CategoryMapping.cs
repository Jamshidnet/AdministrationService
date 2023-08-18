using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
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
             CreateMap<CreateCategoryCommand, Category>()
                .ForMember(sr => sr.CategoryName, des => des
                .MapFrom(y => y.categories
                .FirstOrDefault().TranslateText));

            //CreateMap<UpdateCategoryCommand, Category>()
            //    .ForMember(x => x.TranslateCategories, des => des.MapFrom(y => y.categories))
            //    .ForMember(x => x.CategoryName, des => des.MapFrom(y => y.categories.First().TranslateText));

             CreateMap<Category, CategoryResponse>()
                .ForMember(cr => cr.CategoryName, cfg => cfg
                .MapFrom<CategoryValueResolver<CategoryResponse>>());

             CreateMap<Category, GetListCategoryResponse>()
                .ForMember(cr => cr.CategoryName, cfg => cfg
                .MapFrom<CategoryValueResolver<GetListCategoryResponse>>());

             CreateMap<CreateCommandTranslate, TranslateCategory>();

             CreateMap<UpdateCommandTranslate, TranslateCategory>();

        }
    }
}
