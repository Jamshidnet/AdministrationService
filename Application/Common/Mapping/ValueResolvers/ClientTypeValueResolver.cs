using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping.ValueResolvers;

public class ClientTypeValueResolver<T> : IValueResolver<ClientType, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public ClientTypeValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(ClientType source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslateClientTypes.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);
        return res is null ? source.TypeName : res.TranslateText;
    }
}
