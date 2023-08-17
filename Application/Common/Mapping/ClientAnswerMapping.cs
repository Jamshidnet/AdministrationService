using Application.UseCases.DefaultAnswers.Commands;
using Application.UseCases.DefaultAnswers.Responses;
using Application.UseCases.Docs.Commands;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class ClientAnswerMapping : Profile
{
    public ClientAnswerMapping()
    {
        _ = CreateMap<CreateClientAnswerCommand, ClientAnswer>();
        _ = CreateMap<ClientInDocUpdateResponse, ClientAnswer>();
        _ = CreateMap<ClientInDocResponse, ClientAnswer>();
        _ = CreateMap<ClientAnswer, ClientAnswerResponse>()
            .ForMember(x => x.Question, m => m.MapFrom(y => y.Question.QuestionText))
            .ForMember(x => x.AnswerText, m => m.MapFrom(y => y.AnswerText ?? y.DefaultAnswer.AnswerText));
    }
}
