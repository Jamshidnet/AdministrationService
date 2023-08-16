using Application.UseCases.Permissions.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Permissions.Queries;

public record GetAllPermissionQuery : IRequest<List<PermissionResponse>>;

public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, List<PermissionResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetAllPermissionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async  Task<List<PermissionResponse>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _dbContext.Permissions.ToListAsync();

        var permissionResponses = _mapper.Map<List<PermissionResponse>>(permissions);

        return permissionResponses;
    }
}

