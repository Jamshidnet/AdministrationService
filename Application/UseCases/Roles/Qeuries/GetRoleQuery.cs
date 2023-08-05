using Application.UseCases.Roles.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Roles.Qeuries;

public record GetAllRoleQuery : IRequest<List<RoleResponse>>;


public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, List<RoleResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllRoleQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RoleResponse>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Roles.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<RoleResponse>>(entities);
        return result;
    }
}
