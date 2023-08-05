using Application.UseCases.Docs.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Queries;


public record GetAllDocQuery : IRequest<List<DocResponse>>;


public class GetAllDocQueryHandler : IRequestHandler<GetAllDocQuery, List<DocResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllDocQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocResponse>> Handle(GetAllDocQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Docs.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<DocResponse>>(entities);
        return result;
    }
}
