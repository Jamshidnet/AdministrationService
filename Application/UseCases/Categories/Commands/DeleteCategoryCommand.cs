using Application.Common.Exceptions;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;


public record DeleteCategoryCommand(Guid CategoryId) : IRequest;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(request.CategoryId, cancellationToken)
            ?? throw new NotFoundException(nameof(Category), request.CategoryId);
        
        _ = _context.Categories.Remove(category);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
