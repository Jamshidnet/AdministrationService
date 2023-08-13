using Application.Common.Exceptions;
using Application.UseCases.Categories.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;

public class UpdateCategoryCommand : IRequest<CategoryResponse>
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; }

}
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
        var foundCategory = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Category), request.Id);
        _mapper.Map(request, foundCategory);
        _context.Categories.Update(foundCategory);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CategoryResponse>(foundCategory);
    }

    private async Task FilterIfCategoryExsists(Guid categoryId)
    {
        if (await _context.Categories.FindAsync(categoryId) is null)
            throw new NotFoundException("There is no category with given Id. ");
    }

}
