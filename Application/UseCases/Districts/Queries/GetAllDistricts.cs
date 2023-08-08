using Application.UseCases.Districts.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Districts.Queries;

public record GetAllDistrictQuery : IRequest<List<GetListDIstrictResponse>>;


public class GetAllDistrictQueryHandler : IRequestHandler<GetAllDistrictQuery, List<GetListDIstrictResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllDistrictQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetListDIstrictResponse>> Handle(GetAllDistrictQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Districts.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GetListDIstrictResponse>>(entities);
        return result;
    }
}
