using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Commands;


public class UpdateDefaultAnswerCommand : IRequest<DefaultAnswerResponse>
{
    public Guid Id { get; set; }

    public string AnswerText { get; set; }

    public Guid QuestionId { get; set; }
}
public class UpdateDefaultAnswerCommandHandler : IRequestHandler<UpdateDefaultAnswerCommand, DefaultAnswerResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;
    public UpdateDefaultAnswerCommandHandler(
        IApplicationDbContext dbContext, 
        IMapper mapper,
        ICurrentUserService userService)
    {
        _context = dbContext;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<DefaultAnswerResponse> Handle(UpdateDefaultAnswerCommand request, CancellationToken cancellationToken)
    {
        var foundDefaultAnswer = await FilterIfDefaultAnswerExsists(request.Id);
        await FilterIfQuestionExsists(request.QuestionId);

        _mapper.Map(request, foundDefaultAnswer);
        _context.DefaultAnswers.Update(foundDefaultAnswer);


        var transDefaultAnswer = await _context.TranslateDefaultAnswers
             .FirstOrDefaultAsync(x => x.OwnerId == foundDefaultAnswer.Id
                                  && x.LanguageId.ToString() == _userService.LanguageId);

        transDefaultAnswer.TranslateText = request.AnswerText;
        _context.TranslateDefaultAnswers.Update(transDefaultAnswer);


        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<DefaultAnswerResponse>(foundDefaultAnswer);

    }
    private async Task FilterIfQuestionExsists(Guid question)
    {
        if (await _context.Questions.FindAsync(question) is null)
            throw new NotFoundException("There is no  quesiton  with given Id. ");
    }

    private async Task<DefaultAnswer> FilterIfDefaultAnswerExsists(Guid answerId)
    {
        return await _context.DefaultAnswers.FindAsync(answerId)
            ?? throw new NotFoundException("There is no answer with given Id. ");
    }
}
