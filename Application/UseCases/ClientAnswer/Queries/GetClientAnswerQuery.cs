using Application.Common.Exceptions;
using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Queries;


public record GetByIdClientAnswerQuery(Guid Id) : IRequest<ClientAnswerResponse>;


public class GetByIdClientAnswerQueryHandler : IRequestHandler<GetByIdClientAnswerQuery, ClientAnswerResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdClientAnswerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClientAnswerResponse> Handle(GetByIdClientAnswerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClientAnswers.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(DefaultAnswer), request.Id);

        var result = _mapper.Map<ClientAnswerResponse>(entity);
        return result;
    }
}
