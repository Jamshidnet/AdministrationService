using Application.Common.Exceptions;
using Application.UseCases.QuestionTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.QuestionTypes.Queries;


public record GetByIdQuestionTypeQuery(Guid Id) : IRequest<QuestionTypeResponse>;


public class GetByIdQuestionTypeQueryHandler : IRequestHandler<GetByIdQuestionTypeQuery, QuestionTypeResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdQuestionTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuestionTypeResponse> Handle(GetByIdQuestionTypeQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.QuestionTypes.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(QuestionType), request.Id);

        var result = _mapper.Map<QuestionTypeResponse>(entity);
        return result;
    }
}
