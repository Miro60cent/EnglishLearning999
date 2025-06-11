using Blazored.LocalStorage;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Services
{
    public class ManageResourcesLocalStorageService : IManageResourcesLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IResourceService ResourceService;
        private const string key = "ResourceCollection";

        public ManageResourcesLocalStorageService(ILocalStorageService localStorageService, IResourceService ResourceService)
        {
            this.localStorageService = localStorageService;
            this.ResourceService = ResourceService;
        }

        public async Task<IEnumerable<ResourceDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<ResourceDto>>(key) ?? await AddCollection();
        }

        private async Task<IEnumerable<ResourceDto>> AddCollection()
        {
            var ResourceCollection = await this.ResourceService.GetResources();

            if (ResourceCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, ResourceCollection);
            }

            return ResourceCollection;
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<ResourceDto> ResourceDto)
        {
            await this.localStorageService.SetItemAsync(key, ResourceDto);
        }
    }
}