using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.ClientTypes.Commands;

public record CreateClientTypeCommand(List<CreateCommandTranslate> clientTypes) : IRequest<Guid>;

public class CreateClientTypeCommandHandler : IRequestHandler<CreateClientTypeCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateClientTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateClientTypeCommand request, CancellationToken cancellationToken)
    {
        ClientType clientType = _mapper.Map<ClientType>(request);
        TranslateClientType TclientType = new();

        clientType.Id = Guid.NewGuid();

        request.clientTypes.ForEach(c =>
        {
            TclientType = _mapper.Map<TranslateClientType>(c);
            TclientType.OwnerId = clientType.Id;
            TclientType.ColumnName = "ClientTypeName";
            TclientType.Id = Guid.NewGuid();
            _dbContext.TranslateClientTypes
            .Add(TclientType);
        });

        await _dbContext.ClientTypes.AddAsync(clientType);

        await _dbContext.SaveChangesAsync();
        return clientType.Id;
    }
}
