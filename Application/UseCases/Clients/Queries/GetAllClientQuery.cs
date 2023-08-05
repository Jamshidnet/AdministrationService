using Application.UseCases.Clients.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Clients.Queries;

public record GetAllClientQuery : IRequest<List<ClientResponse>>;


public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, List<ClientResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllClientQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClientResponse>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Clients.ToListAsync(cancellationToken);

        List<ClientResponse> responses = _mapper.Map<List<ClientResponse>>(entities);
        return responses;
    }
}
