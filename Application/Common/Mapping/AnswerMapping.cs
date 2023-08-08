using Application.UseCases.DefaultAnswers.Commands;
using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class DefaultAnswerMapping : Profile
{
    public DefaultAnswerMapping()
    {
        CreateMap<CreateDefaultAnswerCommand, DefaultAnswer>();
        CreateMap<UpdateDefaultAnswerCommand, DefaultAnswer>();
        CreateMap<DefaultAnswer, DefaultAnswerResponse>();
    }

}
