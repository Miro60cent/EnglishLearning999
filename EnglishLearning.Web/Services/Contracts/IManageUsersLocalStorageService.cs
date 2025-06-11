using Topic.Models.Dtos;

namespace Topic.Web.Services.Contracts
{
    public interface IManageUsersLocalStorageService
    {
        Task<List<UserDto>> GetCollection();
        Task SaveCollection(List<UserDto> userDtos);
        Task RemoveCollection();
    }
}