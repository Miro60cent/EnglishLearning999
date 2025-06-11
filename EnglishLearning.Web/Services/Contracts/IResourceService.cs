using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web.Services.Contracts
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceDto>> GetResources();
        Task<ResourceDto> GetResource(int id);
        Task<ResourceDto> AddResource(ResourceAddDto ResourceAddDto);
        Task<ResourceDto> UpdateResource(int id, ResourceAddDto ResourceAddDto);
        Task<ResourceDto> DeleteResource(int id);
        Task<IEnumerable<TopicDto>> GetTopics();
        Task<IEnumerable<ResourceDto>> GetResourceByTopic(int TopicId);
    }
}