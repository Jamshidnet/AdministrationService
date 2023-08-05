using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.ClientTypes.Commands;

public record CreateClientTypeCommand(string TypeName) : IRequest<Guid>;

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
        ClientType questionType = _mapper.Map<ClientType>(request);
        questionType.Id = Guid.NewGuid();
        await _dbContext.ClientTypes.AddAsync(questionType);
        await _dbContext.SaveChangesAsync();
        return questionType.Id;
    }
}
