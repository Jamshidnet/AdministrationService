using Application.Common.Exceptions;
using Application.UseCases.UserTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.UserTypes.Queries;


public record GetByIdUserTypeQuery(Guid Id) : IRequest<UserTypeResponse>;


public class GetByIdUserTypeQueryHandler : IRequestHandler<GetByIdUserTypeQuery, UserTypeResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdUserTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserTypeResponse> Handle(GetByIdUserTypeQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.UserTypes.FindAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(UserType), request.Id);

        var result = _mapper.Map<UserTypeResponse>(entity);
        return result;
    }
}
