using 
    .Models.Dtos;

namespace Topic.Web.Services.Contracts
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<UserDto> AddUser(UserAddDto userAddDto);
        Task<UserDto> UpdateUser(int id, UserUpdateDto userUpdateDto);
        Task<UserDto> DeleteUser(int id);
    }
}