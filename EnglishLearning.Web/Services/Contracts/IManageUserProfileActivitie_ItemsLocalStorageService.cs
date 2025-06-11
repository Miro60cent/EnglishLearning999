using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web.Services.Contracts
{
    public interface IManageUserProfileActivitie_ItemsLocalStorageService
    {
        Task<List<UserProfileActivitieDto>> GetUserProfileActivitiesCollection();
        Task<List<UserProfileActivitie_ItemDto>> GetCollection();
        Task SaveCollection(List<UserProfileActivitie_ItemDto> UserProfileActivitie_ItemDtos);
        Task RemoveCollection();
    }
}