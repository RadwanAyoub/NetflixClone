using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Features.Users.Commands
{
    public class LoginUserCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = null!;
    }
}