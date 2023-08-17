using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.Questions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.Commands;

public class UpdateQuestionCommand : IRequest<QuestionResponse>
{
    public Guid Id { get; set; }

    public string QuestionText { get; set; }

    public Guid? QuestionTypeId { get; set; }

    public Guid CategoryId { get; set; }
}

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, QuestionResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;
    public UpdateQuestionCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService userService)
    {
        _context = dbContext;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<QuestionResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var foundQuestion = await _context.Questions.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Question), request.Id);

        _mapper.Map(request, foundQuestion);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == _userService.Username)
             ?? throw new NotFoundException("There is no user with this id. ");

        var transQuestion = await _context.TranslateQuestions
             .FirstOrDefaultAsync(x => x.OwnerId == foundQuestion.Id
                                  && x.LanguageId == user.LanguageId);

        transQuestion.TranslateText = request.QuestionText;
        foundQuestion.CreatorUserId = user.Id;
        _context.Questions.Update(foundQuestion);
        _context.TranslateQuestions.Update(transQuestion);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QuestionResponse>(foundQuestion);
    }
}
