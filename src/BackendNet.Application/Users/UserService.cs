using BackendNet.Application.Auth.Register;
using BackendNet.Application.Users.Dto;
using BackendNet.Application.Common.Pagination;
using BackendNet.Application.Users.Command;
using BackendNet.Domain.Users;
public class UserService
{
    public readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResponse<UserResponse>> GetAllAsync(int page, int pageSize)
    {
        var users = await _userRepository.GetAllAsync(page, pageSize);

        var totalItems = await _userRepository.CountAsync();

        var totalPages = (int)Math.Ceiling(
            totalItems / (double)pageSize
        );

        var userResponses = users
            .Select(user => new UserResponse(user.Id, user.FirstName, user.LastName, user.Email))
            .ToList();

        return new PagedResponse<UserResponse>(userResponses, page, pageSize, totalItems, totalPages);
    }

    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new Exception("User not existent.");
        }

        return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email);

    }

    public async Task<UserResponse> UpdateUserAsync(UserUpdateCommand command, Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            throw new Exception("User not existent.");
        }
        user.Update(command.FirstName, command.LastName, command.Email);

        await _userRepository.UpdateUserAsync(user);

        return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email);
    }

    public async Task<string> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            throw new Exception("User not existent.");
        }

        user.Delete();

        await _userRepository.DeleteUserAsync();

        return new string("user desactivated.");
    }
}