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

        CreateMap<CreateQuestionTypeCommand, QuestionType>()
            .ForMember(x => x.QuestionTypeName, y => y.MapFrom(z => z.questionTypes.First().TranslateText));
        CreateMap<UpdateQuestionTypeCommand, QuestionType>();
        CreateMap<QuestionType, QuestionTypeResponse>();
        CreateMap<QuestionType, GetLIstQuestionTypeResponse>();

        CreateMap<CreateCommandTranslate, TranslateQuestionType>();

        CreateMap<UpdateCommandTranslate, TranslateQuestionType>();
    }
}
