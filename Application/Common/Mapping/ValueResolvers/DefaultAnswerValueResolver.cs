using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping.ValueResolvers;

public class DefaultAnswerValueResolver<T> : IValueResolver<DefaultAnswer, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public DefaultAnswerValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(DefaultAnswer source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslateDefaultAnswers.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);
        return res is null ? source.AnswerText : res.TranslateText;
    }
}
