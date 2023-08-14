using Application.UseCases.Logs.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Logs.Queries;


public record GetAllUserActionQuery : IRequest<List<UserActionResponse>>;


public class GetAllQuestionTypeQueryHandler : IRequestHandler<GetAllUserActionQuery, List<UserActionResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllQuestionTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserActionResponse>> Handle(GetAllUserActionQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.UserActions.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<UserActionResponse>>(entities);
        return result;
    }
}
