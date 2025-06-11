using Blazored.LocalStorage;
using 
    .Models.Dtos;
using Topic.Web.Services.Contracts;

namespace Topic.Web.Services
{
    public class ManageUsersLocalStorageService : IManageUsersLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IUserService usersService;
        const string key = "UserCollection";

        public ManageUsersLocalStorageService(ILocalStorageService localStorageService, IUserService usersService)
        {
            this.localStorageService = localStorageService;
            this.usersService = usersService;
        }

        public async Task<List<UserDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<UserDto>>(key) ?? await AddCollection();
        }

        private async Task<List<UserDto>> AddCollection()
        {
            var userCollection = await this.usersService.GetUsers();

            if (userCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, userCollection);
            }

            return userCollection;
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<UserDto> userDto)
        {
            await this.localStorageService.SetItemAsync(key, userDto);
        }
    }
}