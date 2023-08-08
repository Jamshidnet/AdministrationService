using Application.UseCases.Docs.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Queries;


public record GetAllDocQuery : IRequest<List<GetListDocResponse>>;


public class GetAllDocQueryHandler : IRequestHandler<GetAllDocQuery, List<GetListDocResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllDocQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetListDocResponse>> Handle(GetAllDocQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Docs.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GetListDocResponse>>(entities);
        return result;
    }
}
