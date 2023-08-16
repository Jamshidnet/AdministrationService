using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.Categories.Responses;
using Application.UseCases.Clients.Commands;
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
            CreateMap<CreateClientTypeCommand, ClientType>()
                .ForMember(x => x.TypeName, y => y.MapFrom(y => y.clientTypes.First().TranslateText));

            CreateMap<UpdateClientCommand, ClientType>();


            CreateMap<ClientType, ClientTypeResponse>()
           .ForMember(cr => cr.TypeName, cfg => cfg
             .MapFrom<ClientTypeValueResolver<ClientTypeResponse>>());

            CreateMap<ClientType, GetListClientTypeResponse>()
                .ForMember(cr => cr.TypeName, cfg => cfg
             .MapFrom<ClientTypeValueResolver<GetListClientTypeResponse>>());

            CreateMap<CreateCommandTranslate, TranslateClientType>();

            CreateMap<UpdateCommandTranslate, TranslateClientType>();


        }

    }
}
