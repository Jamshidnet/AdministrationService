using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Categories.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Commands;

public record UpdateCategoryCommand(Guid Id, string CategoryName) : IRequest<CategoryResponse>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
{

    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;
    public UpdateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService userService)
    {
        _context = dbContext;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await FilterIfCategoryExsists(request.Id);

        var transCategory = await _context.TranslateCategories
             .FirstOrDefaultAsync(x => x.OwnerId == category.Id
                                  && x.LanguageId.ToString() == _userService.LanguageId);

        transCategory.TranslateText = request.CategoryName;
        _context.TranslateCategories.Update(transCategory);
       await  _context.SaveChangesAsync(cancellationToken); 
        return _mapper.Map<CategoryResponse>(category);
    }

    private async Task<Category> FilterIfCategoryExsists(Guid categoryId)
    {
       return await _context.Categories.FindAsync(categoryId)
            ?? throw new NotFoundException("There is no category with given Id. ");
    }

}
