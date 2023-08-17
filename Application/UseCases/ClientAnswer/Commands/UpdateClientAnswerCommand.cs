using Application.Common.Exceptions;

using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.ClientAnswers.Commands;


public class UpdateClientAnswerCommand : IRequest<ClientAnswerResponse>
{
    public Guid Id { get; set; }

    public string AnswerText { get; set; }

    public Guid? DefaultAnswerId { get; set; }

    public Guid DocId { get; set; }

    public Guid QuestionId { get; set; }
}
public class UpdateClientAnswerCommandHandler : IRequestHandler<UpdateClientAnswerCommand, ClientAnswerResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateClientAnswerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<ClientAnswerResponse> Handle(UpdateClientAnswerCommand request, CancellationToken cancellationToken)
    {
        await FilterIfClientAnswerExsists(request.Id);
        await FilterIfQuestionExsists(request.QuestionId);
        await FilterIfClienExsists(request.DocId);
        var foundClientAnswer = await _context.ClientAnswers.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(ClientAnswer), request.Id);
        _ = _mapper.Map(request, foundClientAnswer);
        _ = _context.ClientAnswers.Update(foundClientAnswer);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ClientAnswerResponse>(foundClientAnswer);
    }
    private async Task FilterIfClientAnswerExsists(Guid answerId)
    {
        if (await _context.ClientAnswers.FindAsync(answerId) is null)
            throw new NotFoundException("There is no answer with given Id. ");
    }

    private async Task FilterIfClienExsists(Guid docId)
    {
        if (await _context.Docs.FindAsync(docId) is null)
            throw new NotFoundException("There is no doc with given Id. ");
    }

    private async Task FilterIfQuestionExsists(Guid questionId)
    {
        if (await _context.Questions.FindAsync(questionId) is null)
            throw new NotFoundException("There is no question with given Id. ");
    }
}
