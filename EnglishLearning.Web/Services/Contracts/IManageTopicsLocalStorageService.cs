using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web.Services.Contracts
{
    public interface IManageTopicsLocalStorageService
    {
        Task<List<TopicDto>> GetCollection();
        Task SaveCollection(List<TopicDto> userDtos);
        Task RemoveCollection();
    }
}
