using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.Permissions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Permissions.Commands.UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<PermissionResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, PermissionResponse>
    {
        public readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;
        public UpdatePermissionCommandHandler(IApplicationDbContext dbcontext, IMapper mapper, ICurrentUserService userService)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PermissionResponse> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await FilterIfPermissionExsists(request.Id);

            var transPermission = await _dbContext.TranslatePermissions
                 .FirstOrDefaultAsync(x => x.OwnerId == permission.Id
                                      && x.LanguageId.ToString() == _userService.LanguageId);

            transPermission.TranslateText = request.Name;
            _ = _dbContext.TranslatePermissions.Update(transPermission);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PermissionResponse>(permission);
        }
        private async Task<Permission> FilterIfPermissionExsists(Guid clientID)
        {
            return await _dbContext.Permissions.FindAsync(clientID)
                ?? throw new NotFoundException("There is no client with given Id. ");
        }
    }
}
