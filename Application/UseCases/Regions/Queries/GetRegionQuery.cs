using Application.Common.Exceptions;
using Application.UseCases.Regions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Regions.Queries;

public record GetByIdRegionQuery(Guid Id) : IRequest<RegionResponse>;


public class GetByIdRegionQueryHandler : IRequestHandler<GetByIdRegionQuery, RegionResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdRegionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RegionResponse> Handle(GetByIdRegionQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Regions.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Region), request.Id);

        var result = _mapper.Map<RegionResponse>(entity);
        return result;
    }
}
