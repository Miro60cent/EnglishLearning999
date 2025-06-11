using Microsoft.EntityFrameworkCore;
using EnglishLearning.Api.Data;
using EnglishLearning.Api.Entities;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TopicDbContext TopicDbContext;

        public UserRepository(TopicDbContext TopicDbContext)
        {
            this.TopicDbContext = TopicDbContext;
        }

        private async Task<bool> UserExists(string email)
        {
            return await this.TopicDbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this.TopicDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await TopicDbContext.Users.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User> AddUser(UserAddDto userAddDto)
        {
            if (await UserExists(userAddDto.Email) == false)
            {
                var user = new User {
                    FullName = userAddDto.FullName,
                    Email = userAddDto.Email,
                    Password = userAddDto.Password,
                    Telephone = "",
                    Avatar_Src = "/Images/Users/User0.png",
                    Discount_Percent = 0,
                    Is_Admin = false
                };

                var result = await this.TopicDbContext.Users.AddAsync(user);
                await this.TopicDbContext.SaveChangesAsync();

                var ResourceProfile = new ResourceProfile
                {
                    UserId = user.Id,
                    Paid = false
                };
                await this.TopicDbContext.ResourceProfiles.AddAsync(ResourceProfile);
                await this.TopicDbContext.SaveChangesAsync();

                return result.Entity;
            }
            else
            {
                throw new Exception($"Пользователь с таким Email уже существует");
            }

            return null;
        }

        public async Task<User> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var user = await this.TopicDbContext.Users.FindAsync(id);

            if (user != null)
            {
                user.FullName = userUpdateDto.FullName;
                user.Password = userUpdateDto.Password;
                user.Telephone = userUpdateDto.Telephone;
                user.Avatar_Src = userUpdateDto.Avatar_Src;
                user.Discount_Percent = userUpdateDto.Discount_Percent;
                user.Is_Admin = userUpdateDto.Is_Admin;
                await this.TopicDbContext.SaveChangesAsync();

                return user;
            }

            return null;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await this.TopicDbContext.Users.FindAsync(id);

            if (user != null)
            {
                this.TopicDbContext.Users.Remove(user);
                await this.TopicDbContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task<ResourceProfile> CloseUserResourceProfile(int userId)
        {
            var user = await this.TopicDbContext.Users.FindAsync(userId);

            if (user != null)
            {
                var ResourceProfile = await TopicDbContext.ResourceProfiles.SingleOrDefaultAsync(o => o.UserId == userId && o.Paid == false);

                if (ResourceProfile != null)
                {
                    ResourceProfile.Paid = true;

                    var newResourceProfile = new ResourceProfile
                    {
                        UserId = user.Id,
                        Paid = false,
                        User = user
                    };
                    await this.TopicDbContext.ResourceProfiles.AddAsync(newResourceProfile);
                    await this.TopicDbContext.SaveChangesAsync();

                    return newResourceProfile;
                }
                else
                {
                    throw new Exception($"Ошибка при закрытии заказа пользователя с Id = {userId}");
                }
            }
            else
            {
                throw new Exception($"Пользователь с Id = {userId} не найден");
            }

            return null;
        }
    }   
}