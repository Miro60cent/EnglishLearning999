using Blazored.LocalStorage;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Services
{
    public class ManageUserProfileActivitie_ItemsLocalStorageService : IManageUserProfileActivitie_ItemsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IUserProfileActivitieService UserProfileActivitieService;
        const string key = "UserProfileActivitieItemCollection";
        const string key2 = "UserProfileActivitieCollection";

        public ManageUserProfileActivitie_ItemsLocalStorageService(ILocalStorageService localStorageService, IUserProfileActivitieService UserProfileActivitieService)
        {
            this.localStorageService = localStorageService;
            this.UserProfileActivitieService = UserProfileActivitieService;
        }

        public async Task<List<UserProfileActivitieDto>> GetUserProfileActivitiesCollection()
        {
            return await this.UserProfileActivitieService.GetUserProfileActivities();
        }

        public async Task<List<UserProfileActivitie_ItemDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<UserProfileActivitie_ItemDto>>(key) ?? await AddCollection();
        }

        private async Task<List<UserProfileActivitie_ItemDto>> AddCollection()
        {
            var UserProfileActivitieCollection = await this.UserProfileActivitieService.GetItems(HardCoded.UserId);

            if (UserProfileActivitieCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, UserProfileActivitieCollection);
            }

            return UserProfileActivitieCollection;
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<UserProfileActivitie_ItemDto> UserProfileActivitieItemDtos)
        {
            await this.localStorageService.SetItemAsync(key, UserProfileActivitieItemDtos);
        }
    }
}