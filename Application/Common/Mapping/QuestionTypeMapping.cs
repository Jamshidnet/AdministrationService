using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.QuestionTypes.Commands;
using Application.UseCases.QuestionTypes.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class QuestionTypeMapping : Profile
{
    public QuestionTypeMapping()
    {

        _ = CreateMap<CreateQuestionTypeCommand, QuestionType>()
            .ForMember(x => x.QuestionTypeName, y => y.MapFrom(z => z.questionTypes.First().TranslateText));
        _ = CreateMap<UpdateQuestionTypeCommand, QuestionType>();


        _ = CreateMap<QuestionType, QuestionTypeResponse>()
             .ForMember(cr => cr.QuestionTypeName, cfg => cfg
               .MapFrom<QuestionTypeValueResolver<QuestionTypeResponse>>());

        _ = CreateMap<QuestionType, GetLIstQuestionTypeResponse>()
            .ForMember(cr => cr.QuestionTypeName, cfg => cfg
         .MapFrom<QuestionTypeValueResolver<GetLIstQuestionTypeResponse>>());

        _ = CreateMap<CreateCommandTranslate, TranslateQuestionType>();

        _ = CreateMap<UpdateCommandTranslate, TranslateQuestionType>();
    }
}
