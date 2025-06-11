using EnglishLearning.Api.Entities;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<TopicDto> ConvertToDto(this IEnumerable<Topic> Topics)
        {
            return (from Topic in Topics
                    select new TopicDto
                    {
                        Id = Topic.Id,
                        Model_Name = Topic.Model_Name,
                        UserProfileActivities_id = Topic.UserProfileActivities.Id,
                        Color = Topic.Color,
                        Price = Topic.Price,
                        Image_Src = Topic.Image_Src,
                        Form_factor = Topic.Form_factor,
                        Wireless = Topic.Wireless,
                        Noice_Cancellation = Topic.Noice_Cancellation,
                        Waterproof = Topic.Waterproof,
                        Microphone = Topic.Microphone,
                        In_Stock = Topic.In_Stock,
                        UserProfileActivities_Name = Topic.UserProfileActivities.FullName,
                        UserProfileActivities_Avatar_Src = Topic.UserProfileActivities.Avatar_Src
                    }).ToList();
        }

        public static TopicDto ConvertToDto(this Topic Topic)
        {
            return new TopicDto
            {
                Id = Topic.Id,
                Model_Name = Topic.Model_Name,
                UserProfileActivities_id = Topic.UserProfileActivities.Id,
                Color = Topic.Color,
                Price = Topic.Price,
                Image_Src = Topic.Image_Src,
                Form_factor = Topic.Form_factor,
                Wireless = Topic.Wireless,
                Noice_Cancellation = Topic.Noice_Cancellation,
                Waterproof = Topic.Waterproof,
                Microphone = Topic.Microphone,
                In_Stock = Topic.In_Stock,
                UserProfileActivities_Name = Topic.UserProfileActivities.FullName,
                UserProfileActivities_Avatar_Src = Topic.UserProfileActivities.Avatar_Src
            };
        }

        public static TopicDto ConvertToDto(this Topic Topic, UserProfileActivities UserProfileActivities)
        {
            return new TopicDto
            {
                Id = Topic.Id,
                Model_Name = Topic.Model_Name,
                UserProfileActivities_id = Topic.UserProfileActivities_id,
                Color = Topic.Color,
                Price = Topic.Price,
                Image_Src = Topic.Image_Src,
                Form_factor = Topic.Form_factor,
                Wireless = Topic.Wireless,
                Noice_Cancellation = Topic.Noice_Cancellation,
                Waterproof = Topic.Waterproof,
                Microphone = Topic.Microphone,
                In_Stock = Topic.In_Stock,
                UserProfileActivities_Name = UserProfileActivities.FullName,
                UserProfileActivities_Avatar_Src = UserProfileActivities.Avatar_Src
            };
        }

        public static IEnumerable<UserProfileActivitiesDto> ConvertToDto(this IEnumerable<UserProfileActivities> UserProfileActivitiess)
        {
            return (from UserProfileActivities in UserProfileActivitiess
                    select new UserProfileActivitiesDto
                    {
                        Id = UserProfileActivities.Id,
                        FullName = UserProfileActivities.FullName,
                        Avatar_Src = UserProfileActivities.Avatar_Src
                    }).ToList();
        }

        public static UserProfileActivitiesDto ConvertToDto(this UserProfileActivities UserProfileActivitiess)
        {
            return  new UserProfileActivitiesDto
                    {
                        Id = UserProfileActivitiess.Id,
                        FullName = UserProfileActivitiess.FullName,
                        Avatar_Src = UserProfileActivitiess.Avatar_Src
                    };
        }

        public static IEnumerable<ResourceProfile_ItemDto> ConvertToDto(this IEnumerable<ResourceProfile_Item> ResourceProfile_Items, IEnumerable<Topic> Topics)
        {
            return (from ResourceProfile_Item in ResourceProfile_Items
                    join Topic in Topics
                    on ResourceProfile_Item.TopicId equals Topic.Id
                    select new ResourceProfile_ItemDto
                    {
                        Id = ResourceProfile_Item.Id,
                        ResourceProfileId = ResourceProfile_Item.ResourceProfileId,
                        TopicId = Topic.Id,
                        Model_Name = Topic.Model_Name,
                        Color = Topic.Color,
                        Image_Src = Topic.Image_Src,
                        UserProfileActivities_id = Topic.UserProfileActivities_id,
                        UserProfileActivities_Name = Topic.UserProfileActivities.FullName,
                        UserProfileActivities_Avatar_Src = Topic.UserProfileActivities.Avatar_Src,
                        In_Stock = Topic.In_Stock,
                        Price = Topic.Price,
                        TotalPrice = Topic.Price * ResourceProfile_Item.Quantity,
                        Quantity = ResourceProfile_Item.Quantity
                    }).ToList();
        }

        public static ResourceProfile_ItemDto ConvertToDto(this ResourceProfile_Item ResourceProfile_Item, Topic Topic)
        {
            return new ResourceProfile_ItemDto
            {
                Id = ResourceProfile_Item.Id,
                ResourceProfileId = ResourceProfile_Item.ResourceProfileId,
                TopicId = Topic.Id,
                Model_Name = Topic.Model_Name,
                Color = Topic.Color,
                Image_Src = Topic.Image_Src,
                UserProfileActivities_id = Topic.UserProfileActivities_id,
                UserProfileActivities_Name = Topic.UserProfileActivities.FullName,
                UserProfileActivities_Avatar_Src = Topic.UserProfileActivities.Avatar_Src,
                In_Stock = Topic.In_Stock,
                Price = Topic.Price,
                TotalPrice = Topic.Price * ResourceProfile_Item.Quantity,
                Quantity = ResourceProfile_Item.Quantity
            };
        }

        public static IEnumerable<UserDto> ConvertToDto(this IEnumerable<User> users)
        {
            return (from user in users
                    select new UserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Password = user.Password,
                        Telephone = user.Telephone,
                        Avatar_Src = user.Avatar_Src,
                        Discount_Percent = user.Discount_Percent,
                        Is_Admin = user.Is_Admin
                    }).ToList();
        }

        public static UserDto ConvertToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Telephone = user.Telephone,
                Avatar_Src = user.Avatar_Src,
                Discount_Percent = user.Discount_Percent,
                Is_Admin = user.Is_Admin
            };
        }

        public static IEnumerable<ResourceProfileDto> ConvertToDto(this IEnumerable<ResourceProfile> ResourceProfiles)
        {
            return (from ResourceProfile in ResourceProfiles
                    select new ResourceProfileDto
                    {
                        Id = ResourceProfile.Id,
                        UserId = ResourceProfile.UserId,
                        Paid = ResourceProfile.Paid
                    }).ToList();
        }

        public static ResourceProfileDto ConvertToDto(this ResourceProfile ResourceProfile)
        {
            return new ResourceProfileDto
            {
                Id = ResourceProfile.Id,
                UserId = ResourceProfile.UserId,
                Paid = ResourceProfile.Paid
            };
        }
    }
}