using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.Categories.Responses;
using Application.UseCases.ClientTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.ClientTypes.Commands;


public class UpdateClientTypeCommand : IRequest<ClientTypeResponse>
{
    public Guid Id { get; set; }

    public string TypeName { get; set; }

}
public class UpdateClientTypeCommandHandler : IRequestHandler<UpdateClientTypeCommand, ClientTypeResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;
    public UpdateClientTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService userService)
    {
        _context = dbContext;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<ClientTypeResponse> Handle(UpdateClientTypeCommand request, CancellationToken cancellationToken)
    {
        var clientType = await FilterIfClientTypeExsists(request.Id);

        var transClientType = await _context.TranslateClientTypes
             .FirstOrDefaultAsync(x => x.OwnerId == clientType.Id
                                  && x.LanguageId.ToString() == _userService.LanguageId);

        transClientType.TranslateText = request.TypeName;
        _context.TranslateClientTypes.Update(transClientType);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ClientTypeResponse>(clientType);
    }

    private async Task<ClientType> FilterIfClientTypeExsists(Guid clientTypeId)
    {
        return await _context.ClientTypes.FindAsync(clientTypeId)
            ?? throw new NotFoundException("There is no client type with given Id. ");
    }

}
