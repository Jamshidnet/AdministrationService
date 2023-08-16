using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;
namespace Application.Common.Mapping.ValueResolvers;

class UserTypeValueResolver<T> : IValueResolver<UserType, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public UserTypeValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(UserType source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslateUserTypes.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);
        if (res is null)
        {
            return source.TypeName;
        }

        else return res.TranslateText;
    }
}
