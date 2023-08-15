using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.Common.Models;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.Commands;


public record CreateQuestionCommand(List<CreateCommandTranslate> questions, Guid CategoryId, Guid QuestionTypeId) : IRequest<Guid>;

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

        Question question = _mapper.Map<Question>(request);
        question.CreatorUserId = user.Id;
        question.Id = Guid.NewGuid();

        TranslateQuestion Tquestions = new();

        request.questions.ForEach(c =>
        {
            Tquestions = _mapper.Map<TranslateQuestion>(c);
            Tquestions.OwnerId = question.Id;
            Tquestions.ColumnName = "QuestionsName";
            Tquestions.Id = Guid.NewGuid();
            _dbContext.TranslateQuestions
            .Add(Tquestions);
        });

        await _dbContext.Questions.AddAsync(question);
        await _dbContext.SaveChangesAsync();
        return question.Id;
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
