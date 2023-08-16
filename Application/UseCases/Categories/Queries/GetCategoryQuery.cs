using Application.Common.Exceptions;
using Application.UseCases.Categories.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Categories.Queries;


public record GetByIdCategoryQuery(Guid Id) : IRequest<CategoryResponse>;


public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryResponse> Handle(GetByIdCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Category), request.Id);



        var result = _mapper.Map<CategoryResponse>(entity);
        return result;
    }
}
