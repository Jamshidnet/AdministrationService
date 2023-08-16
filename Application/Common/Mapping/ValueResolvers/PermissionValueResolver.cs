using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;
namespace Application.Common.Mapping.ValueResolvers;

class PermissionValueResolver<T> : IValueResolver<Permission, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public PermissionValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(Permission source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslatePermissions.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);

        if (res is null)
        {
            return source.PermissionName;
        }

        else return res.TranslateText;
    }
}
