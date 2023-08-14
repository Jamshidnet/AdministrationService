using Application.UseCases.Categories.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;


public record CreateCategoryCommand(List<TranslateCategoryResponse> categories) : IRequest<Guid>;

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
        TranslateCategory Tcategory = new ();
        request.categories.ForEach(c => {
             Tcategory = _mapper.Map<TranslateCategory>(c);
            Tcategory.OwnerId = category.Id;
            Tcategory.Id = Guid.NewGuid();
            _dbContext.TranslateCategories
            .Add(Tcategory);
        });

        category.Id = Guid.NewGuid();
        await _dbContext.Categories.AddAsync(category);

        await _dbContext.SaveChangesAsync();
        return category.Id;
    }
}
