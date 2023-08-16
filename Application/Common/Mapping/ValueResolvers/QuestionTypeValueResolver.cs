using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;
namespace Application.Common.Mapping.ValueResolvers;

class QuestionTypeValueResolver<T> : IValueResolver<QuestionType, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public QuestionTypeValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(QuestionType source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslateQuestionTypes.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);
        if (res is null)
        {
            return source.QuestionTypeName;
        }

        else return res.TranslateText;
    }
}
