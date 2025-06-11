using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web.Services.Contracts
{
    public interface IUserProfileActivitieService
    {
        Task<List<UserProfileActivitieDto>> GetUserProfileActivities();
        Task<List<UserProfileActivitie_ItemDto>> GetItems(int userId);
        Task<UserProfileActivitie_ItemDto> AddItem(UserProfileActivitie_ItemToAddDto UserProfileActivitie_ItemToAddDto);
        Task<UserProfileActivitie_ItemDto> UpdateQuantity(UserProfileActivitie_ItemQuantityUpdateDto UserProfileActivitie_ItemQuantityUpdateDto);
        Task<UserProfileActivitie_ItemDto> DeleteItem(int id);

        event Action<int> OnUserProfileActivitieChanged;
        void RaiseEventOnUserProfileActivitieChanged(int totalQuantity);
    }
}