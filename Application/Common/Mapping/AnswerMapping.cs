using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.DefaultAnswers.Commands;
using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class DefaultAnswerMapping : Profile
{
    public DefaultAnswerMapping()
    {
        _ = CreateMap<CreateDefaultAnswerCommand, DefaultAnswer>()
            .ForMember(y => y.AnswerText, z => z.MapFrom(t => t.defaultAnswers
            .First().TranslateText));

        _ = CreateMap<UpdateDefaultAnswerCommand, DefaultAnswer>();

        _ = CreateMap<DefaultAnswer, DefaultAnswerResponse>()
            .ForMember(cr => cr.AnswerText, cfg => cfg
            .MapFrom<DefaultAnswerValueResolver<DefaultAnswerResponse>>());

        _ = CreateMap<CreateCommandTranslate, TranslateDefaultAnswer>();

        _ = CreateMap<UpdateCommandTranslate, TranslateDefaultAnswer>();
    }

}
