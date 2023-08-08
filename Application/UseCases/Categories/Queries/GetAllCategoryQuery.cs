using Application.UseCases.Categories.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Queries;

public record GetAllCategoryQuery   : IRequest<List<GetListCategoryResponse>>;


public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<GetListCategoryResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetListCategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Categories.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GetListCategoryResponse>>(entities);
        return result;
    }
}
