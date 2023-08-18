using Application.Common.Exceptions;
using Application.UseCases.Questions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.Queries;


public record GetByIdQuestionQuery(Guid Id) : IRequest<QuestionResponse>;


public class GetByIdQuestionQueryHandler : IRequestHandler<GetByIdQuestionQuery, QuestionResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdQuestionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuestionResponse> Handle(GetByIdQuestionQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Questions.FindAsync(request.Id, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Question), request.Id);

        var result = _mapper.Map<QuestionResponse>(entity);
        return result;
    }
}
