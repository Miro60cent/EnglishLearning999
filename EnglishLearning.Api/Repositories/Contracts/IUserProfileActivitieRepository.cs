using EnglishLearning.Api.Entities;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories.Contracts
{
    public interface IUserProfileActivitieRepository
    {
        Task<IEnumerable<UserProfileActivitie>> GetUserProfileActivities();
        Task<IEnumerable<UserProfileActivitie_Item>> GetItems(int userId);
        Task<UserProfileActivitie_Item> GetItem(int id);
        Task<UserProfileActivitie_Item> AddItem(UserProfileActivitie_ItemToAddDto UserProfileActivitie_ItemToAddDto);
        Task<UserProfileActivitie_Item> UpdateQuantity(int id, UserProfileActivitie_ItemQuantityUpdateDto UserProfileActivitie_ItemQuantityUpdateDto);
        Task<UserProfileActivitie_Item> DeleteItem(int id);
    }
}