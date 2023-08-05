using Application.Common.Exceptions;
using Application.UseCases.DefaultAnswers.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.DefaultAnswers.Queries;


public record GetByIdDefaultAnswerQuery(Guid Id) : IRequest<DefaultAnswerResponse>;


public class GetByIdDefaultAnswerQueryHandler : IRequestHandler<GetByIdDefaultAnswerQuery, DefaultAnswerResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdDefaultAnswerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DefaultAnswerResponse> Handle(GetByIdDefaultAnswerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.DefaultAnswers.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(DefaultAnswer), request.Id);

        var result = _mapper.Map<DefaultAnswerResponse>(entity);
        return result;
    }
}
