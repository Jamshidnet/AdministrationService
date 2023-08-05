using Application.Common.Exceptions;
using Application.UseCases.Permissions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Permissions.Queries
{
    public record GetByIdPermissionQuery(Guid PermissionId) : IRequest<PermissionResponse>;

    public class GetByIdPermissionQueryHandler : IRequestHandler<GetByIdPermissionQuery, PermissionResponse>
    {
        public readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetByIdPermissionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PermissionResponse> Handle(GetByIdPermissionQuery request, CancellationToken cancellationToken)
        {
            var permission = await _dbContext.Permissions.FindAsync(new object[] { request.PermissionId }, cancellationToken);
            if (permission == null)
            {
                throw new NotFoundException(nameof(Permission), request.PermissionId);
            }
            var result = _mapper.Map<PermissionResponse>(permission);
            return result;
        }
    }
}
