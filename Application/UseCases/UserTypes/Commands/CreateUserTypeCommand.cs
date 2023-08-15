using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.UserTypes.Commands;


public record CreateUserTypeCommand(List<CreateCommandTranslate> userTypes) : IRequest<Guid>;

public class CreateUserTypeCommandHandler : IRequestHandler<CreateUserTypeCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateUserTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        UserType userType = _mapper.Map<UserType>(request);
        TranslateUserType TuserType = new();

        userType.Id = Guid.NewGuid();

        request.userTypes.ForEach(c =>
        {
            TuserType = _mapper.Map<TranslateUserType>(c);
            TuserType.OwnerId = userType.Id;
            TuserType.ColumnName = "UserTypeName";
            TuserType.Id = Guid.NewGuid();
            _dbContext.TranslateUserTypes
            .Add(TuserType);
        });

        await _dbContext.UserTypes.AddAsync(userType);

        await _dbContext.SaveChangesAsync();
        return userType.Id;
    }
}


