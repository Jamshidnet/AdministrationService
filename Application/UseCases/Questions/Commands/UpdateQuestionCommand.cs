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

    public Guid? QuestionId { get; set; }

    public Guid? CreatorUserId { get; set; }

    public Guid? QuestionTypeId { get; set; }
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

        var transQuestion = await _context.TranslateQuestions
             .FirstOrDefaultAsync(x => x.OwnerId == foundQuestion.Id
                                  && x.LanguageId.ToString() == _userService.LanguageId);

        transQuestion.TranslateText = request.QuestionText;

        _context.Questions.Update(foundQuestion);
        _context.TranslateQuestions.Update(transQuestion);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QuestionResponse>(foundQuestion);
    }
}
