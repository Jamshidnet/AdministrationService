using Application.UseCases.Quarters.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Quarters.Queries;

public record GetAllQuarterQuery : IRequest<List<GetListQuarterResponse>>;


public class GetAllQuarterQueryHandler : IRequestHandler<GetAllQuarterQuery, List<GetListQuarterResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllQuarterQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetListQuarterResponse>> Handle(GetAllQuarterQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Quarters.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GetListQuarterResponse>>(entities);
        return result;
    }
}



