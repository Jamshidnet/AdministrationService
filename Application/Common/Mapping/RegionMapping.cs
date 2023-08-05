using Application.UseCases.Regions.Commands;
using Application.UseCases.Regions.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class RegionMapping : Profile
{
    public RegionMapping()
    {

        CreateMap<CreateRegionCommand, Region>();
        CreateMap<UpdateRegionCommand, Region>();
        CreateMap<Region, RegionResponse>();

    }

}
