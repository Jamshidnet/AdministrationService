using Application.Common.Exceptions;
using Application.UseCases.Users.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Users.Queries;

public record GetByIdUserQuery(Guid Id) : IRequest<UserResponse>;


public class GetByIdUserResponse : IRequestHandler<GetByIdUserQuery, UserResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdUserResponse(IApplicationDbContext context, IMapper mapper)
    {
        (_context, _mapper) = (context, mapper);
    }

    public async Task<UserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var foundUser = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
        if (foundUser is null)
            throw new NotFoundException(nameof(User), request.Id);


        var result = _mapper.Map<UserResponse>(foundUser);
        return result;
    }
}
