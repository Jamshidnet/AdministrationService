using Application.Common.Exceptions;
using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
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
    public UpdateDefaultAnswerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<DefaultAnswerResponse> Handle(UpdateDefaultAnswerCommand request, CancellationToken cancellationToken)
    {
        await FilterIfDefaultAnswerExsists(request.Id);
        await FilterIfQuestionExsists(request.QuestionId);

        var foundDefaultAnswer = await _context.DefaultAnswers.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(DefaultAnswer), request.Id);
        _mapper.Map(request, foundDefaultAnswer);
        _context.DefaultAnswers.Update(foundDefaultAnswer);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<DefaultAnswerResponse>(foundDefaultAnswer);
    }
    private async Task FilterIfDefaultAnswerExsists(Guid answerId)
    {
        if (await _context.DefaultAnswers.FindAsync(answerId) is null)
            throw new NotFoundException("There is no  default answer with given Id. ");
    }

    private async Task FilterIfQuestionExsists(Guid questionId)
    {
        if (await _context.Questions.FindAsync(questionId) is null)
            throw new NotFoundException("There is no question with given Id. ");
    }
}
