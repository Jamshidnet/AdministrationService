using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.UseCases.Users.Commands.LoginUser;
using Application.UseCases.Users.Responses;
using AutoMapper;
using Domein.Common.Identity;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;
using NewProject.JWT.Interfaces;

namespace Application.Common.JWT.Service
{
    public class RefreshToken : IUserRefreshToken
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RefreshToken(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async ValueTask<UserRefreshToken> AddOrUpdateRefreshToken(UserRefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == refreshToken.UserName, cancellationToken);

            user.RefreshToken = refreshToken.RefreshToken;
            user.ExpiresDate = refreshToken.ExpiresTime;
            // _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return refreshToken;

        }

        public async ValueTask<UserResponse> AuthenAsync(LoginUserCommand user)
        {
            var foundUserByUsername = await _context.Users.SingleOrDefaultAsync(u => u.Username == user.Username)
                ?? throw new NotFoundException(" There is no user with this username ");

            Guid salt = foundUserByUsername.SaltId;

            string hashPassword = (user.Password + salt).GetHashedString();
            var founUser = await _context.Users.FirstOrDefaultAsync(x => x.Password == hashPassword)
                    ?? throw new NotFoundException(" Invalid password . ");

            var userResponse = _mapper.Map<UserResponse>(founUser);


            return userResponse;
        }

        public async ValueTask<bool> DeleteUserRefreshTokens(string username, string refreshToken, CancellationToken cancellationToken = default)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            foundUser.RefreshToken = null;
            foundUser.ExpiresDate = null;
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }

        public async ValueTask<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            return new UserRefreshToken()
            {
                RefreshToken = user.RefreshToken,
                //  ExpiresTime=user.ExpiresDate,
                UserName = user.Username
            };
        }
    }
}
