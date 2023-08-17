using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.QuestionTypes.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.QuestionTypes.Commands;


public class UpdateQuestionTypeCommand : IRequest<QuestionTypeResponse>
{
    public Guid Id { get; set; }

    public string QuestionTypeName { get; set; }

}
public class UpdateQuestionTypeCommandHandler : IRequestHandler<UpdateQuestionTypeCommand, QuestionTypeResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;
    public UpdateQuestionTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService userService)
    {
        _context = dbContext;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<QuestionTypeResponse> Handle(UpdateQuestionTypeCommand request, CancellationToken cancellationToken)
    {
        var questionType = await FilterIfQuestionTypeExsists(request.Id);

        var transQuestionType = await _context.TranslateQuestionTypes
             .FirstOrDefaultAsync(x => x.OwnerId == questionType.Id
                                  && x.LanguageId.ToString() == _userService.LanguageId);

        transQuestionType.TranslateText = request.QuestionTypeName;
        _ = _context.TranslateQuestionTypes.Update(transQuestionType);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<QuestionTypeResponse>(questionType);
    }

    private async Task<QuestionType> FilterIfQuestionTypeExsists(Guid categoryId)
    {
        return await _context.QuestionTypes.FindAsync(categoryId)
            ?? throw new NotFoundException("There is no category with given Id. ");
    }

}
