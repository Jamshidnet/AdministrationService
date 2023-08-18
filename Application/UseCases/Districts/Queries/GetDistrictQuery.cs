using Application.Common.Exceptions;
using Application.UseCases.Districts.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Districts.Queries;


public record GetByIdDistrictQuery(Guid Id) : IRequest<DistrictResponse>;


public class GetByIdDistrictQueryHandler : IRequestHandler<GetByIdDistrictQuery, DistrictResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdDistrictQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DistrictResponse> Handle(GetByIdDistrictQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Districts.FindAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(District), request.Id);

        var result = _mapper.Map<DistrictResponse>(entity);
        return result;
    }
}