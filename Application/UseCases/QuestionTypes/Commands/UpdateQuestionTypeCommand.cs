using Application.Common.Exceptions;
using Application.UseCases.QuestionTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.QuestionTypes.Commands;


public class UpdateQuestionTypeCommand : IRequest<QuestionTypeResponse>
{
    public Guid Id { get; set; }

    public string QuestionTypeName { get; set; }

}
public class UpdateQuestionTypeCommandHandler : IRequestHandler<UpdateQuestionTypeCommand, QuestionTypeResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateQuestionTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<QuestionTypeResponse> Handle(UpdateQuestionTypeCommand request, CancellationToken cancellationToken)
    {
        var foundQuestionType = await _context.QuestionTypes.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(QuestionType), request.Id);
        _mapper.Map(request, foundQuestionType);
        _context.QuestionTypes.Update(foundQuestionType);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QuestionTypeResponse>(foundQuestionType);
    }

    private async Task FilterIfQuestionTypeExsists(Guid categoryId)
    {
        if (await _context.QuestionTypes.FindAsync(categoryId) is null)
            throw new NotFoundException("There is no category with given Id. ");
    }

}
