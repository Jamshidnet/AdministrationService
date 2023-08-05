using Application.UseCases.Users.Responses;
using AutoMapper;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Users.newfolder;


public record FilterUserByBirthDate : IRequest<List<UserResponse>>
{
    public DateOnly MinBirthDate { get; set; }
    public DateOnly MaxBirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool ValidYearRange => MaxBirthDate > MinBirthDate;

}
public class FilterUserByBirthDateHandler : IRequestHandler<FilterUserByBirthDate, List<UserResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public FilterUserByBirthDateHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<UserResponse>> Handle(FilterUserByBirthDate request, CancellationToken cancellationToken)
    {
        if (!request.ValidYearRange)
            throw new Exception(" Invalid year range input. ");

        var users = _context.Users
            .Where(user => user.Person.Birthdate >= request.MinBirthDate &&
        user.Person.Birthdate <= request.MaxBirthDate);

        return Task.FromResult(_mapper.Map<List<UserResponse>>(users));
    }
}
