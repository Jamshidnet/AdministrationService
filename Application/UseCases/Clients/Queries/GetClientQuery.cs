using Application.Common.Exceptions;
using Application.UseCases.Clients.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Clients.Queries;


public record GetByIdClientQuery(Guid Id) : IRequest<ClientResponse>;


public class GetByIdClientQueryHandler : IRequestHandler<GetByIdClientQuery, ClientResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdClientQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClientResponse> Handle(GetByIdClientQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Clients
            .FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Client), request.Id);

        ClientResponse clientResponse = _mapper.Map<ClientResponse>(entity);

        return clientResponse;
    }
}
