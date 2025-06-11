using EnglishLearning.Api.Entities;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories.Contracts
{
    public interface IUserRepository
    { 
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> AddUser(UserAddDto userAddDto);
        Task<User> UpdateUser(int id, UserUpdateDto userUpdateDto);
        Task<User> DeleteUser(int id);
        Task<ResourceProfile> CloseUserResourceProfile(int id);
    }
}