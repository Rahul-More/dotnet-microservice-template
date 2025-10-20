using Domain.Entities;
using Domain.Services;

namespace Application;

public class UserServiceFacade
{
    private readonly UserService _userService;

    public UserServiceFacade(UserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDto?> GetUserDtoAsync(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return null;
        return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
    }
}