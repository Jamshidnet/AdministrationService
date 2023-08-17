using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.ClientTypes.Commands;
using Application.UseCases.ClientTypes.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping
{
    public class ClientTypeMapping : Profile
    {

        public ClientTypeMapping()
        {
            _ = CreateMap<CreateClientTypeCommand, ClientType>()
                .ForMember(x => x.TypeName, y => y.MapFrom(y => y.clientTypes.First().TranslateText));

            _ = CreateMap<ClientType, ClientTypeResponse>()
           .ForMember(cr => cr.TypeName, cfg => cfg
             .MapFrom<ClientTypeValueResolver<ClientTypeResponse>>());

            _ = CreateMap<ClientType, GetListClientTypeResponse>()
                .ForMember(cr => cr.TypeName, cfg => cfg
             .MapFrom<ClientTypeValueResolver<GetListClientTypeResponse>>());

            _ = CreateMap<CreateCommandTranslate, TranslateClientType>();

            _ = CreateMap<UpdateCommandTranslate, TranslateClientType>();


        }

    }
}
