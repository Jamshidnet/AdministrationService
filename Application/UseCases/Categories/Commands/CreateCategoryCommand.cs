using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;


public record CreateCategoryCommand(List<CreateCommandTranslate> categories) : IRequest<Guid>;

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
        TranslateCategory Tcategory = new();

        category.Id = Guid.NewGuid();

        request.categories.ForEach(c =>
        {
            Tcategory = _mapper.Map<TranslateCategory>(c);
            Tcategory.OwnerId = category.Id;
            Tcategory.ColumnName = "CategoryName";
            Tcategory.Id = Guid.NewGuid();
            _dbContext.TranslateCategories
            .Add(Tcategory);
        });

        await _dbContext.Categories.AddAsync(category);

        await _dbContext.SaveChangesAsync();
        return category.Id;
    }
}
