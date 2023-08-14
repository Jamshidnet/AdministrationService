using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Languages.Commands
{

    public record CreateLanguageCommand(string LanguageName) : IRequest<Guid>;

    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, Guid>
    {
        private IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateLanguageCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            Language questionType = _mapper.Map<Language>(request);
            questionType.Id = Guid.NewGuid();
            await _dbContext.Languages.AddAsync(questionType);
            await _dbContext.SaveChangesAsync();
            return questionType.Id;
        }
    }
}
