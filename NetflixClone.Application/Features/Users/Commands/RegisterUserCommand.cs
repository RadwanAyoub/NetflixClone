using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Features.Users.Commands
{
    public class RegisterUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}