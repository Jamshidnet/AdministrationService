using Application.Common.Abstraction;
using AutoMapper;
using Domein.Entities;
namespace Application.Common.Mapping.ValueResolvers;

class QuestionValueResolver<T> : IValueResolver<Question, T, string>
{
    private readonly ICurrentUserService _currentUser;

    public QuestionValueResolver(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public string Resolve(Question source, T destination, string destMember, ResolutionContext context)
    {
        var res = source.TranslateQuestions.AsQueryable()
                    .FirstOrDefault(t => t.LanguageId.ToString() == _currentUser.LanguageId);
        if (res is null)
        {
            return source.QuestionText;
        }

        else return res.TranslateText;
    }
}
