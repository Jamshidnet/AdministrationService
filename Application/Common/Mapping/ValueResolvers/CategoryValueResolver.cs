using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping.ValueResolvers;
public class CategoryValueResolver<T> : IValueResolver<Category, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public CategoryValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(Category source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslateCategories.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);
        if (res is null)
        {
            return source.CategoryName;
        }

        else return res.TranslateText;
    }
}

