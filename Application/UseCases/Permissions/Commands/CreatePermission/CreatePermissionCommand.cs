using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Permissions.Commands.CreatePermission;

public record CreatePermissionCommand(List<CreateCommandTranslate> permissions) : IRequest<Guid>;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreatePermissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        Permission permission = _mapper.Map<Permission>(request);
        TranslatePermission Tpermission = new();

        permission.Id = Guid.NewGuid();

        request.permissions.ForEach(c =>
        {
            Tpermission = _mapper.Map<TranslatePermission>(c);
            Tpermission.OwnerId = permission.Id;
            Tpermission.ColumnName = "PermissionName";
            Tpermission.Id = Guid.NewGuid();
             _dbContext.TranslatePermissions
            .Add(Tpermission);
        });

         await _dbContext.Permissions.AddAsync(permission);

         await _dbContext.SaveChangesAsync();
        return permission.Id;
    }
}

