using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web.Services.Contracts
{
    public interface ITopicService
    {
        Task<List<TopicDto>> GetTopics();
        Task<TopicDto> GetTopic(int id);
        Task<TopicDto> AddTopic(TopicAddDto TopicAddDto);
        Task<TopicDto> UpdateTopic(int id, TopicAddDto TopicAddDto);
        Task<TopicDto> DeleteTopic(int id);
    }
}
