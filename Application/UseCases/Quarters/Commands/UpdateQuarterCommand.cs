using Application.Common.Exceptions;
using Application.UseCases.Quarters.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Quarters.Commands;

public class UpdateQuarterCommand : IRequest<QuarterResponse>
{
    public Guid Id { get; set; }

    public string QuarterName { get; set; }

    public Guid? DistrictId { get; set; }
}
public class UpdateQuarterCommandHandler : IRequestHandler<UpdateQuarterCommand, QuarterResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateQuarterCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<QuarterResponse> Handle(UpdateQuarterCommand request, CancellationToken cancellationToken)
    {
        var foundQuarter = await _context.Quarters.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Quarter), request.Id);
        _ = _mapper.Map(request, foundQuarter);
        _ = _context.Quarters.Update(foundQuarter);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<QuarterResponse>(foundQuarter);
    }
}
