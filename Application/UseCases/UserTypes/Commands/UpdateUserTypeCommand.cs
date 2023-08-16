using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.UserTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    private readonly ICurrentUserService _userService;
    public UpdateUserTypeCommandHandler(
        IApplicationDbContext dbContext, 
        IMapper mapper, 
        ICurrentUserService userService)
    {
        _context = dbContext;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<UserTypeResponse> Handle(UpdateUserTypeCommand request, CancellationToken cancellationToken)
    {
        var userType = await FilterIfUserTypeExsists(request.Id);

        var transUserType = await _context.TranslateUserTypes
             .FirstOrDefaultAsync(x => x.OwnerId == userType.Id
                                  && x.LanguageId.ToString() == _userService.LanguageId);

        transUserType.TranslateText = request.TypeName;
        _context.TranslateUserTypes.Update(transUserType);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserTypeResponse>(userType);
    }

    private async Task<UserType> FilterIfUserTypeExsists(Guid clientID)
    {
        return await _context.UserTypes.FindAsync(clientID) 
            ??throw new NotFoundException("There is no client with given Id. ");
    }

}
