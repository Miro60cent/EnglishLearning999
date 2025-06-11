using Microsoft.EntityFrameworkCore;
using EnglishLearning.Api.Data;
using EnglishLearning.Api.Entities;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly EnglishLearningDbContext EnglishLearningDbContext;

        public ResourceRepository(EnglishLearningDbContext EnglishLearningDbContext)
        {
            this.EnglishLearningDbContext = EnglishLearningDbContext;
        }

        private async Task<bool> ResourceExists(string Model_Name)
        {
            return await this.EnglishLearningDbContext.Resources.AnyAsync(h => h.Model_Name == Model_Name);
        }

        public async Task<IEnumerable<Resource>> GetResources()
        {
            return await this.EnglishLearningDbContext.Resources.Include(p => p.Topic).ToListAsync();
        }

        public async Task<Resource> GetResource(int id)
        {
            return await EnglishLearningDbContext.Resources.Include(p => p.Topic).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Resource> AddResource(ResourceAddDto ResourceAddDto)
        {
            if (await ResourceExists(ResourceAddDto.Model_Name) == false)
            {
                var Resource = await (from Topic in this.EnglishLearningDbContext.Topics
                                     where Topic.Id == ResourceAddDto.Topic_id
                                     select new Resource
                                     {
                                         Model_Name = ResourceAddDto.Model_Name,
                                         Topic_id = ResourceAddDto.Topic_id,
                                         Color = ResourceAddDto.Color,
                                         Price = ResourceAddDto.Price,
                                         Image_Src = ResourceAddDto.Image_Src,
                                         Form_factor = ResourceAddDto.Form_factor,
                                         Wireless = ResourceAddDto.Wireless,
                                         Noice_Cancellation = ResourceAddDto.Noice_Cancellation,
                                         Waterproof = ResourceAddDto.Waterproof,
                                         Microphone = ResourceAddDto.Microphone,
                                         In_Stock = ResourceAddDto.In_Stock
                                     }).SingleOrDefaultAsync();

                var result = await this.EnglishLearningDbContext.Resources.AddAsync(Resource);
                await this.EnglishLearningDbContext.SaveChangesAsync();

                return result.Entity;
            }
            else
            {
                throw new Exception($"Такая модель уже существует");
            }

            return null;
        }

        public async Task<Resource> UpdateResource(int id, ResourceAddDto ResourceAddDto)
        {
            var Resource = await this.EnglishLearningDbContext.Resources.FindAsync(id);
            var TopicExists = await this.EnglishLearningDbContext.Resources.AnyAsync(h => h.Topic_id == ResourceAddDto.Topic_id);

            if (Resource != null && TopicExists)
            {
                Resource.Model_Name = ResourceAddDto.Model_Name;
                Resource.Topic_id = ResourceAddDto.Topic_id;
                Resource.Color = ResourceAddDto.Color;
                Resource.Price = ResourceAddDto.Price;
                Resource.Image_Src = ResourceAddDto.Image_Src;
                Resource.Form_factor = ResourceAddDto.Form_factor;
                Resource.Wireless = ResourceAddDto.Wireless;
                Resource.Noice_Cancellation = ResourceAddDto.Noice_Cancellation;
                Resource.Waterproof = ResourceAddDto.Waterproof;
                Resource.Microphone = ResourceAddDto.Microphone;
                Resource.In_Stock = ResourceAddDto.In_Stock;
                await this.EnglishLearningDbContext.SaveChangesAsync();

                return Resource;
            }
            else

            return null;
        }

        public async Task<Resource> DeleteResource(int ResourceId)
        {
            var Resource = await this.EnglishLearningDbContext.Resources.FindAsync(ResourceId);

            if (Resource != null)
            {
                this.EnglishLearningDbContext.Resources.Remove(Resource);
                await this.EnglishLearningDbContext.SaveChangesAsync();
            }

            return Resource;
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            return await this.EnglishLearningDbContext.Topics.ToListAsync();
        }

        public async Task<Topic> GetTopic(int id)
        {
            return await EnglishLearningDbContext.Topics.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Resource>> GetResourceByTopic(int id)
        {
            return await this.EnglishLearningDbContext.Resources.Include(p => p.Topic).Where(p => p.Topic_id == id).ToListAsync();
        }
    }
}