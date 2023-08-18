using Application.Common.Abstraction;
using Domein.Entities;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.Common.Logging
{
    public class LoggingService : IDocChangeLogger
    {

        IApplicationDbContext _context;
        ICurrentUserService _currentUserService;

        public LoggingService(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task Log(Guid DocId, string action)
        {

            var table = await _context.SysTables.SingleOrDefaultAsync(x => x.TableName == "Doc");
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == _currentUserService.Username);
            DocChangeLog docChangeLog = new()
            {
                Id = Guid.NewGuid(),
                ActionName = action,
                DateAt = DateOnly.FromDateTime(DateTime.Now),
                UserId = user?.Id,
                TableId = table.Id,
                DocId = DocId
            };

             await _context.DocChangeLogs.AddAsync(docChangeLog);
             await _context.SaveChangesAsync();

        }
    }
}
