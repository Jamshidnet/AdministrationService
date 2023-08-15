using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Categories.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;

public record UpdateCategoryCommand(List<UpdateCommandTranslate> categories ) : IRequest<CategoryResponse>;

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
        var en = request.categories.First();
        var category = _context.Categories.Find(en.OwnerId)
            ?? throw new NotFoundException(" there is no category with this owner id. ");

        category.CategoryName = en.TranslateText;
        _mapper.Map(request.categories, category.TranslateCategories);
        _context.Categories.Update(category);
       await  _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CategoryResponse>(category);
    }

    private async Task FilterIfCategoryExsists(Guid categoryId)
    {
        if (await _context.Categories.FindAsync(categoryId) is null)
            throw new NotFoundException("There is no category with given Id. ");
    }

}
