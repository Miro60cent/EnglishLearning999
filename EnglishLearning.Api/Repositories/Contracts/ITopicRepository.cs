using EnglishLearning.Api.Entities;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories.Contracts
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetTopics();
        Task<Topic> GetTopic(int id);
        Task<Topic> AddTopic(TopicAddDto TopicAddDto);
        Task<Topic> UpdateTopic(int id, TopicAddDto TopicAddDto);
        Task<Topic> DeleteTopic(int id);
    }
}