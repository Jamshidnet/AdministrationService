using Application.Common.Exceptions;
using Application.UseCases.Quarters.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Quarters.Queries;


public record GetByIdQuarterQuery(Guid Id) : IRequest<QuarterResponse>;


public class GetByIdQuarterQueryHandler : IRequestHandler<GetByIdQuarterQuery, QuarterResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdQuarterQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuarterResponse> Handle(GetByIdQuarterQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Quarters.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Quarter), request.Id);

        var result = _mapper.Map<QuarterResponse>(entity);
        return result;
    }
}
