using Application.UseCases.Quarters.Commands;
using Application.UseCases.Quarters.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class QuarterMapping : Profile
{
    public QuarterMapping()
    {

        CreateMap<CreateQuarterCommand, Quarter>();
        CreateMap<UpdateQuarterCommand, Quarter>();
        CreateMap<Quarter, QuarterResponse>();

    }

}
