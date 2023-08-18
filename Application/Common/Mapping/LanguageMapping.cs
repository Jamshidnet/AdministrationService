using Application.UseCases.Languages.Commands;
using Application.UseCases.Languages.Response;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class LanguageMapping : Profile
{

    public LanguageMapping()
    {
         CreateMap<CreateLanguageCommand, Language>();
         CreateMap<Language, LanguageResponse>();
    }

}
