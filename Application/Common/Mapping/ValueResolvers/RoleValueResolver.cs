using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;
namespace Application.Common.Mapping.ValueResolvers;

class RoleValueResolver<T> : IValueResolver<Role, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public RoleValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(Role source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslateRoles.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);
        return res is null ? source.RoleName : res.TranslateText;
    }
}
