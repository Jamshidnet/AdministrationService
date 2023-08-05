using Application.Common.Exceptions;
using Application.UseCases.UserTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.UserTypes.Commands;


public class UpdateUserTypeCommand : IRequest<UserTypeResponse>
{
    public Guid Id { get; set; }

    public string TypeName { get; set; }

}
public class UpdateUserTypeCommandHandler : IRequestHandler<UpdateUserTypeCommand, UserTypeResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateUserTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<UserTypeResponse> Handle(UpdateUserTypeCommand request, CancellationToken cancellationToken)
    {
        var foundUserType = await _context.UserTypes.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(UserType), request.Id);

        // _mapper.Map(request, foundUserType);
        foundUserType.TypeName = request.TypeName;
        _context.UserTypes.Update(foundUserType);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserTypeResponse>(foundUserType);
    }

    private async Task FilterIfUserTypeExsists(Guid clientID)
    {
        if (await _context.UserTypes.FindAsync(clientID) is null)
            throw new NotFoundException("There is no client with given Id. ");
    }

}
