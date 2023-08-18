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
         CreateMap<CreateDefaultAnswerCommand, DefaultAnswer>()
            .ForMember(y => y.AnswerText, z => z.MapFrom(t => t.defaultAnswers
            .First().TranslateText));

         CreateMap<UpdateDefaultAnswerCommand, DefaultAnswer>();

         CreateMap<DefaultAnswer, DefaultAnswerResponse>()
            .ForMember(cr => cr.AnswerText, cfg => cfg
            .MapFrom<DefaultAnswerValueResolver<DefaultAnswerResponse>>());

         CreateMap<CreateCommandTranslate, TranslateDefaultAnswer>();

         CreateMap<UpdateCommandTranslate, TranslateDefaultAnswer>();
    }

}
