using Application.UseCases.Docs.Responses;
using AutoMapper;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Filters;

public record FilterByTakenDate : IRequest<List<DocResponse>>
{
    public DateOnly MinTakenDate { get; set; }
    public DateOnly MaxTakenDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool ValidYearRange => MaxTakenDate > MinTakenDate;
}

public class FilterByTakenDateHandler : IRequestHandler<FilterByTakenDate, List<DocResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public FilterByTakenDateHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public  Task<List<DocResponse>> Handle(FilterByTakenDate request, CancellationToken cancellationToken)
    {
        if (!request.ValidYearRange) throw new Exception(" Invalid year range input. ");
       
        var docs = _context.Docs.Where(o => o.TakenDate >= request.MinTakenDate &&
                                    o.TakenDate <= request.MaxTakenDate);

        return Task.FromResult(_mapper.Map<List<DocResponse>>(docs));  
    }
}
