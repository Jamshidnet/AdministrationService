using Application.Common.Abstraction;
using Application.Common.Exceptions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.Commands;


public record CreateQuestionCommand(string QuestionText, Guid CategoryId, Guid QuestionTypeId) : IRequest<Guid>;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private ICurrentUserService _currentUser;
    public CreateQuestionCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        await FilterIfCategoryExsists(request.CategoryId);
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == _currentUser.Username)
           ?? throw new NotFoundException(
               " a user with the username that exists within the incoming claim couldn't found in database. ");


        Question region = _mapper.Map<Question>(request);

        region.Id = Guid.NewGuid();
        region.CreatorUserId = user.Id;


        await _dbContext.Questions.AddAsync(region);
        await _dbContext.SaveChangesAsync();
        return region.Id;
    }
    private async Task FilterIfCategoryExsists(Guid categoryId)
    {
        if (await _dbContext.Categories.FindAsync(categoryId) is null)
            throw new NotFoundException("There is no category with given Id. ");
    }

    private async Task FilterIfUserExsists(Guid userId)
    {
        if (await _dbContext.Users.FindAsync(userId) is null)
            throw new NotFoundException("There is no user with given Id. ");
    }


}
