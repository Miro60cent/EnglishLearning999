using Microsoft.EntityFrameworkCore;
using EnglishLearning.Api.Data;
using EnglishLearning.Api.Entities;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories
{
    public class UserProfileActivitieRepository : IUserProfileActivitieRepository
    {
        private readonly EnglishLearningDbContext EnglishLearningDbContext;

        public UserProfileActivitieRepository(EnglishLearningDbContext EnglishLearningDbContext)
        {
            this.EnglishLearningDbContext = EnglishLearningDbContext;
        }

        private async Task<bool> CartItemExists(int UserProfileActivitieId, int ResourceId)
        {
            return await this.EnglishLearningDbContext.UserProfileActivitie_Items.AnyAsync(c => c.UserProfileActivitieId == UserProfileActivitieId && c.ResourceId == ResourceId);
        }

        public async Task<IEnumerable<UserProfileActivitie>> GetUserProfileActivities()
        {
            return EnglishLearningDbContext.UserProfileActivities;
        }

        public async Task<IEnumerable<UserProfileActivitie_Item>> GetItems(int userId, int UserProfileActivitieId)
        {
            return await (from UserProfileActivitie in this.EnglishLearningDbContext.UserProfileActivities
                          join UserProfileActivitie_Item in this.EnglishLearningDbContext.UserProfileActivitie_Items
                          on UserProfileActivitie.Id equals UserProfileActivitie_Item.UserProfileActivitieId
                          where UserProfileActivitie.UserId == userId && UserProfileActivitie.Id == UserProfileActivitieId
                          select new UserProfileActivitie_Item
                          {
                              Id = UserProfileActivitie_Item.Id,
                              ResourceId = UserProfileActivitie_Item.ResourceId,
                              Quantity = UserProfileActivitie_Item.Quantity,
                              UserProfileActivitieId = UserProfileActivitie_Item.UserProfileActivitieId
                          }).ToListAsync();
        }

        public async Task<IEnumerable<UserProfileActivitie_Item>> GetItems(int userId)
        {
            return await (from UserProfileActivitie in this.EnglishLearningDbContext.UserProfileActivities
                          join UserProfileActivitie_Item in this.EnglishLearningDbContext.UserProfileActivitie_Items
                          on UserProfileActivitie.Id equals UserProfileActivitie_Item.UserProfileActivitieId
                          where UserProfileActivitie.UserId == userId && UserProfileActivitie.Paid != true
                          select new UserProfileActivitie_Item
                          {
                              Id = UserProfileActivitie_Item.Id,
                              ResourceId = UserProfileActivitie_Item.ResourceId,
                              Quantity = UserProfileActivitie_Item.Quantity,
                              UserProfileActivitieId = UserProfileActivitie_Item.UserProfileActivitieId
                          }).ToListAsync();
        }

        public async Task<UserProfileActivitie_Item> GetItem(int id)
        {
            return await (from UserProfileActivitie in this.EnglishLearningDbContext.UserProfileActivities
                          join UserProfileActivitie_Item in this.EnglishLearningDbContext.UserProfileActivitie_Items
                          on UserProfileActivitie.Id equals UserProfileActivitie_Item.UserProfileActivitieId
                          where UserProfileActivitie_Item.Id == id
                          select new UserProfileActivitie_Item
                          {
                              Id = UserProfileActivitie_Item.Id,
                              ResourceId = UserProfileActivitie_Item.ResourceId,
                              Quantity = UserProfileActivitie_Item.Quantity,
                              UserProfileActivitieId = UserProfileActivitie_Item.UserProfileActivitieId
                          }).SingleOrDefaultAsync();
        }

        public async Task<UserProfileActivitie_Item> AddItem(UserProfileActivitie_ItemToAddDto UserProfileActivitie_ItemToAddDto)
        {
            if (await CartItemExists(UserProfileActivitie_ItemToAddDto.UserProfileActivitieId, UserProfileActivitie_ItemToAddDto.ResourceId) == false)
            {
                var item = await (from Resource in this.EnglishLearningDbContext.Resources
                                  where Resource.Id == UserProfileActivitie_ItemToAddDto.ResourceId
                                  select new UserProfileActivitie_Item
                                  {
                                      UserProfileActivitieId = UserProfileActivitie_ItemToAddDto.UserProfileActivitieId,
                                      ResourceId = Resource.Id,
                                      Quantity = UserProfileActivitie_ItemToAddDto.Quantity
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.EnglishLearningDbContext.UserProfileActivitie_Items.AddAsync(item);
                    await this.EnglishLearningDbContext.SaveChangesAsync();

                    return result.Entity;
                }
            }
            else
            {
                throw new Exception($"Этот товар уже есть в корзине");
            }

            return null;
        }

        public async Task<UserProfileActivitie_Item> UpdateQuantity(int id, UserProfileActivitie_ItemQuantityUpdateDto UserProfileActivitie_ItemQuantityUpdateDto)
        {
            var item = await this.EnglishLearningDbContext.UserProfileActivitie_Items.FindAsync(id);

            if (item != null)
            {
                item.Quantity = UserProfileActivitie_ItemQuantityUpdateDto.Quantity;
                await this.EnglishLearningDbContext.SaveChangesAsync();

                return item;
            }

            return null;
        }

        public async Task<UserProfileActivitie_Item> DeleteItem(int id)
        {
            var item = await this.EnglishLearningDbContext.UserProfileActivitie_Items.FindAsync(id);

            if (item != null)
            {
                this.EnglishLearningDbContext.UserProfileActivitie_Items.Remove(item);
                await this.EnglishLearningDbContext.SaveChangesAsync();
            }

            return item;
        }
    }
}