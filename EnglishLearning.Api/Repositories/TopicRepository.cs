using Microsoft.EntityFrameworkCore;
using EnglishLearning.Api.Data;
using EnglishLearning.Api.Entities;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly EnglishLearningDbContext EnglishLearningDbContext;

        public TopicRepository(EnglishLearningDbContext EnglishLearningDbContext)
        {
            this.EnglishLearningDbContext = EnglishLearningDbContext;
        }

        private async Task<bool> TopicExists(string fullName)
        {
            return await this.EnglishLearningDbContext.Topics.AnyAsync(m => m.FullName == fullName);
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            return await this.EnglishLearningDbContext.Topics.ToListAsync();
        }

        public async Task<Topic> GetTopic(int id)
        {
            return await EnglishLearningDbContext.Topics.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Topic> AddTopic(TopicAddDto TopicAddDto)
        {
            if (await TopicExists(TopicAddDto.FullName) == false)
            {
                var Topic = new Topic
                {
                    FullName = TopicAddDto.FullName,
                    Avatar_Src = TopicAddDto.Avatar_Src
                };

                var result = await this.EnglishLearningDbContext.Topics.AddAsync(Topic);
                await this.EnglishLearningDbContext.SaveChangesAsync();

                return result.Entity;
            }
            else
            {
                throw new Exception($"Такой производитель уже существует");
            }

            return null;
        }

        public async Task<Topic> UpdateTopic(int id, TopicAddDto TopicAddDto)
        {
            var Topic = await this.EnglishLearningDbContext.Topics.FindAsync(id);

            if (Topic != null)
            {
                Topic.FullName = TopicAddDto.FullName;
                Topic.Avatar_Src = TopicAddDto.Avatar_Src;
                await this.EnglishLearningDbContext.SaveChangesAsync();

                return Topic;
            }

            return null;
        }

        public async Task<Topic> DeleteTopic(int id)
        {
            var Topic = await this.EnglishLearningDbContext.Topics.FindAsync(id);

            if (Topic != null)
            {
                this.EnglishLearningDbContext.Topics.Remove(Topic);
                await this.EnglishLearningDbContext.SaveChangesAsync();
            }

            return Topic;
        }
    }
}