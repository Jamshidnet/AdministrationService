using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Queries;


public record GetAllClientAnswerQuery : IRequest<List<ClientAnswerResponse>>;


public class GetAllClientAnswerQueryHandler : IRequestHandler<GetAllClientAnswerQuery, List<ClientAnswerResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllClientAnswerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClientAnswerResponse>> Handle(GetAllClientAnswerQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClientAnswers.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<ClientAnswerResponse>>(entities);
        return result;
    }
}
