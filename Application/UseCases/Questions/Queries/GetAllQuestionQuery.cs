using Application.UseCases.Questions.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.Queries;

public record GetAllQuestionQuery : IRequest<List<GetListQuestionResponse>>;

public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, List<GetListQuestionResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllQuestionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetListQuestionResponse>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Questions.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GetListQuestionResponse>>(entities);
        return result;
    }
}
