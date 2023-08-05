using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Queries;


public record GetAllDefaultAnswerQuery : IRequest<List<DefaultAnswerResponse>>;


public class GetAllDefaultAnswerQueryHandler : IRequestHandler<GetAllDefaultAnswerQuery, List<DefaultAnswerResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllDefaultAnswerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DefaultAnswerResponse>> Handle(GetAllDefaultAnswerQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.DefaultAnswers.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<DefaultAnswerResponse>>(entities);
        return result;
    }
}
