using Application.UseCases.Users.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Users.Queries;

public record GetAllUserQuery : IRequest<List<UserResponse>>;
public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllUserQueryHandler(IApplicationDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

    public async Task<List<UserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var allUser = await _context.Users.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<UserResponse>>(allUser);
        return result;
    }
}