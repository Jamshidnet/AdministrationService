using Application.UseCases.Questions.Commands;
using Application.UseCases.Questions.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class QuestionMapping : Profile
{
    public QuestionMapping()
    {
        CreateMap<CreateQuestionCommand, Question>();
        CreateMap<UpdateQuestionCommand, Question>();
        CreateMap<Question, GetListQuestionResponse>()
        .ForMember(x => x.QuestionType, y => y.MapFrom(z => z.QuestionType.QuestionTypeName))
        .ForMember(x => x.CreatorUser, y => y.MapFrom(z => z.CreatorUser.Username))
        .ForMember(x => x.Category, y => y.MapFrom(z => z.Category.CategoryName));


        CreateMap<Question, QuestionResponse>()
       .ForMember(x => x.CreatorUser, y => y.MapFrom(z => z.CreatorUser.Username));
    }
}
