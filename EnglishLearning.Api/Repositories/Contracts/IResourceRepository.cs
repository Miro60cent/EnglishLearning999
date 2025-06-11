using EnglishLearning.Api.Entities;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories.Contracts
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetResources();
        Task<Resource> GetResource(int id);
        Task<Resource> AddResource(ResourceAddDto ResourceAddDto);
        Task<Resource> UpdateResource(int id, ResourceAddDto ResourceAddDto);
        Task<Resource> DeleteResource(int id);
        Task<IEnumerable<Topic>> GetTopics();
        Task<Topic> GetTopic(int id);
        Task<IEnumerable<Resource>> GetResourceByTopic(int id);
    }
}