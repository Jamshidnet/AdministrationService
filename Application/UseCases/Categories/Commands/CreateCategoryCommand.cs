using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;


public record CreateCategoryCommand(string CategoryName, string EnCategoryName) : IRequest<Guid>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = _mapper.Map<Category>(request);


        category.Id = Guid.NewGuid();
        await _dbContext.Categories.AddAsync(category);

        await _dbContext.SaveChangesAsync();
        return category.Id;
    }
}
