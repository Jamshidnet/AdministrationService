using Application.Common.Exceptions;
using Application.UseCases.Roles.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Roles.Qeuries;

public record GetByIdRoleQuery(Guid Id) : IRequest<RoleResponse>;


public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, RoleResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdRoleQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoleResponse> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Roles.FindAsync(request.Id, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Role), request.Id);

        var result = _mapper.Map<RoleResponse>(entity);
        return result;
    }
}