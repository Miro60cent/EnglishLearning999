using Blazored.LocalStorage;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Services
{
    public class ManageTopicsLocalStorageService : IManageTopicsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ITopicService TopicsService;
        const string key = "TopicCollection";

        public ManageTopicsLocalStorageService(ILocalStorageService localStorageService, ITopicService TopicsService)
        {
            this.localStorageService = localStorageService;
            this.TopicsService = TopicsService;
        }

        public async Task<List<TopicDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<TopicDto>>(key) ?? await AddCollection();
        }

        private async Task<List<TopicDto>> AddCollection()
        {
            var TopicCollection = await this.TopicsService.GetTopics();

            if (TopicCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, TopicCollection);
            }

            return TopicCollection;
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<TopicDto> TopicDto)
        {
            await this.localStorageService.SetItemAsync(key, TopicDto);
        }
    }
}