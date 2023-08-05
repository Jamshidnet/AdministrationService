using Application.Common.Exceptions;
using Application.UseCases.ClientTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.ClientTypes.Queries;


public record GetByIdClientTypeQuery(Guid Id) : IRequest<ClientTypeResponse>;


public class GetByIdClientTypeQueryHandler : IRequestHandler<GetByIdClientTypeQuery, ClientTypeResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdClientTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClientTypeResponse> Handle(GetByIdClientTypeQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.ClientTypes.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(ClientType), request.Id);

        var result = _mapper.Map<ClientTypeResponse>(entity);
        return result;
    }
}
