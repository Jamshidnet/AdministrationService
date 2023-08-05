using Application.Common.Exceptions;
using Application.UseCases.Docs.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Queries;

public record GetByIdDocQuery(Guid Id) : IRequest<DocResponse>;


public class GetByIdDocQueryHandler : IRequestHandler<GetByIdDocQuery, DocResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdDocQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DocResponse> Handle(GetByIdDocQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Docs.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Doc), request.Id);

        var result = _mapper.Map<DocResponse>(entity);
        return result;
    }
}
