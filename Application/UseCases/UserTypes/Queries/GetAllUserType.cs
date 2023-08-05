using Application.UseCases.UserTypes.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.UserTypes.Queries;


public record GetAllUserTypeQuery : IRequest<List<UserTypeResponse>>;


public class GetAllUserTypeQueryHandler : IRequestHandler<GetAllUserTypeQuery, List<UserTypeResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllUserTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserTypeResponse>> Handle(GetAllUserTypeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.UserTypes.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<UserTypeResponse>>(entities);
        return result;
    }
}

