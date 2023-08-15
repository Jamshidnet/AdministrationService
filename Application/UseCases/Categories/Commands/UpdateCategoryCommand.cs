using Application.Common.Exceptions;
using Application.UseCases.Categories.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;

public record UpdateCategoryCommand(List<UpdateCategoryTranslateResponse> categories ) : IRequest<CategoryResponse>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
{

    private IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var foundCatagory = await _context.Categories.FindAsync(request.categories.First().Id);

        _mapper.Map(request.categories.First(), foundCatagory);

        foundCatagory.TranslateCategories = _mapper.Map<TranslateCategory[]>(request.categories); 

        return _mapper.Map<CategoryResponse>(foundCatagory);
    }

    private async Task FilterIfCategoryExsists(Guid categoryId)
    {
        if (await _context.Categories.FindAsync(categoryId) is null)
            throw new NotFoundException("There is no category with given Id. ");
    }

}
