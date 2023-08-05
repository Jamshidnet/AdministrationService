using Application.UseCases.Quarters.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Quarters.Queries;

public record GetAllQuarterQuery : IRequest<List<QuarterResponse>>;


public class GetAllQuarterQueryHandler : IRequestHandler<GetAllQuarterQuery, List<QuarterResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllQuarterQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<QuarterResponse>> Handle(GetAllQuarterQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Quarters.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<QuarterResponse>>(entities);
        return result;
    }
}



