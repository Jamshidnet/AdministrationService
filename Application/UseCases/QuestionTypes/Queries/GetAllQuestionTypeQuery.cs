using Application.UseCases.QuestionTypes.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.QuestionTypes.Queries;


public record GetAllQuestionTypeQuery : IRequest<List<GetLIstQuestionTypeResponse>>;


public class GetAllQuestionTypeQueryHandler : IRequestHandler<GetAllQuestionTypeQuery, List<GetLIstQuestionTypeResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllQuestionTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetLIstQuestionTypeResponse>> Handle(GetAllQuestionTypeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.QuestionTypes.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GetLIstQuestionTypeResponse>>(entities);
        return result;
    }
}