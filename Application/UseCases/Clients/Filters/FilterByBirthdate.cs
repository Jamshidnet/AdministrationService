using Application.UseCases.Clients.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Clients.Filters;

public record FilterByBirthDate : IRequest<List<GetListClientResponse>>
{
    public DateOnly MinBirthDate { get; set; }
    public DateOnly? MaxBirthDate { get; set; }
    public bool ValidYearRange => MaxBirthDate > MinBirthDate;


    public FilterByBirthDate(DateOnly MinBirthDate, DateOnly? MaxBirthDate = null)
    {
        this.MinBirthDate = MinBirthDate;
        this.MaxBirthDate = MaxBirthDate != null ? MaxBirthDate : DateOnly.FromDateTime(DateTime.Now);
    }

}
public class FilterByBirthDateHandler : IRequestHandler<FilterByBirthDate, List<GetListClientResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public FilterByBirthDateHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetListClientResponse>> Handle(FilterByBirthDate request, CancellationToken cancellationToken)
    {
        if (!request.ValidYearRange)
            throw new Exception(" Invalid year range input. ");

        var clients = await _context.Clients
            .Where(client => client.Person.Birthdate >= request.MinBirthDate &&
        client.Person.Birthdate <= request.MaxBirthDate).ToListAsync();

        return _mapper.Map<List<GetListClientResponse>>(clients);
    }
}
