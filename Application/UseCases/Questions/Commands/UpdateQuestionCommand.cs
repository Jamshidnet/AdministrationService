using Application.Common.Exceptions;
using Application.UseCases.Questions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.Commands;


public class UpdateQuestionCommand : IRequest<QuestionResponse>
{
    public Guid Id { get; set; }

    public string QuestionText { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? CreatorUserId { get; set; }

    public Guid? QuestionTypeId { get; set; }
}
public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, QuestionResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateQuestionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<QuestionResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var foundQuestion = await _context.Questions.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Question), request.Id);
        _mapper.Map(request, foundQuestion);
        _context.Questions.Update(foundQuestion);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QuestionResponse>(foundQuestion);
    }
}
