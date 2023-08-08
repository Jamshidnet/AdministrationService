using Application.UseCases.Regions.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Regions.Queries;

public record GetAllRegionQuery: IRequest<List<GetListRegionResponse>>;


public class GetAllRegionQueryHandler : IRequestHandler<GetAllRegionQuery, List<GetListRegionResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllRegionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetListRegionResponse>> Handle(GetAllRegionQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Regions.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GetListRegionResponse>>(entities);
        return result;
    } 
}