using Application.Common.Abstraction;
using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Responses;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domein.Entities;

namespace Application.Common.Mapping
{
    public class CategoryMapping : Profile
    {
        ICurrentUserService _user;
        public CategoryMapping()
        {

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<Category, GetListCategoryResponse>();

            CreateMap<Category, CategoryResponse>()
                .ForMember(cr => cr.CategoryName, cfg => cfg
                .MapFrom(c => c.TranslateCategories
                .FirstOrDefault(t => t.LangaugeId.ToString() == _user.Language)
                .TranslateText ?? c.CategoryName));

            CreateMap<Category, GetListCategoryResponse>()
                .ForMember(cr => cr.CategoryName, cfg => cfg
                .MapFrom(c => c.TranslateCategories
                .FirstOrDefault(t => t.Langauge.Id.ToString() == _user.Language)
                .TranslateText ?? c.CategoryName));

        }

        public CategoryMapping(ICurrentUserService user) : base ()
        {
            _user = user;
        }
    }
}
