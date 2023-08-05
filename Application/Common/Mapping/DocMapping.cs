using Application.UseCases.Docs.Commands;
using Application.UseCases.Docs.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class DocMapping : Profile
{
    public DocMapping()
    {
        CreateMap<CreateDocCommand, Doc>();
        CreateMap<UpdateDocCommand, Doc>();
        CreateMap<Doc, DocResponse>()
            .ForMember(x => x.UserName, des => des
                .MapFrom(doc => (doc.User.Person.FirstName + " " + doc.User.Person.LastName)))
            .ForMember(x => x.ClientName, des => des
                .MapFrom(doc => (doc.Client.Person.FirstName + " " + doc.Client.Person.LastName)));
            

    }

}
