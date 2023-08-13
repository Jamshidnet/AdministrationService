using Application.Common.Exceptions;
using Application.UseCases.ClientTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
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
    public UpdateClientTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<ClientTypeResponse> Handle(UpdateClientTypeCommand request, CancellationToken cancellationToken)
    {
        var foundClientType = await _context.ClientTypes.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(ClientType), request.Id);

        _mapper.Map(request, foundClientType);
        _context.ClientTypes.Update(foundClientType);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ClientTypeResponse>(foundClientType);
    }

    private async Task FilterIfClientTypeExsists(Guid clientID)
    {
        if (await _context.ClientTypes.FindAsync(clientID) is null)
            throw new NotFoundException("There is no client with given Id. ");
    }

}
