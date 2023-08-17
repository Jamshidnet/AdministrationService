using Application.UseCases.Regions.Commands;
using Application.UseCases.Regions.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class RegionMapping : Profile
{
    public RegionMapping()
    {

        _ = CreateMap<CreateRegionCommand, Region>();
        _ = CreateMap<UpdateRegionCommand, Region>();
        _ = CreateMap<Region, RegionResponse>();
        _ = CreateMap<Region, GetListRegionResponse>();

    }

}
