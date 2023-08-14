using Application.UseCases.Languages.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Languages.Queries;


public record GetAllLanguageQuery : IRequest<List<LanguageResponse>>;


public class GetAllLanguageQueryHandler : IRequestHandler<GetAllLanguageQuery, List<LanguageResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllLanguageQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LanguageResponse>> Handle(GetAllLanguageQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Languages.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<LanguageResponse>>(entities);
        return result;
    }
}
