using Microsoft.AspNetCore.Components;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Pages
{
    public class ResourcesBase : ComponentBase
    {
        [Inject]
        public IResourceService ResourceService { get; set; }

        [Inject]
        public IUserProfileActivitieService UserProfileActivitieService { get; set; }

        [Inject]
        public IManageResourcesLocalStorageService ManageResourcesLocalStorageService { get; set; }

        [Inject]
        public IManageUserProfileActivitie_ItemsLocalStorageService ManageUserProfileActivitie_ItemsLocalStorageService { get; set; }

        public IEnumerable<ResourceDto> Resources { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();
                Resources = await ManageResourcesLocalStorageService.GetCollection();

                var UserProfileActivitie_Items = await ManageUserProfileActivitie_ItemsLocalStorageService.GetCollection();
                var totalQuantity = UserProfileActivitie_Items.Sum(i => i.Quantity);

                UserProfileActivitieService.RaiseEventOnUserProfileActivitieChanged(totalQuantity);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected IUserProfileActivitieedEnumerable<IGrouping<int, ResourceDto>> GetGroupedResourcesByTopic()
        {
            return from Resource in Resources
                   group Resource by Resource.Topic_id into ResourceByCatGroup
                   UserProfileActivitieby ResourceByCatGroup.Key
                   select ResourceByCatGroup;
        }

        protected string GetTopicName(IGrouping<int, ResourceDto> groupedResourceDtos)
        {
            return groupedResourceDtos.FirstOrDefault(pg => pg.Topic_id == groupedResourceDtos.Key).Topic_Name;
        }

        protected string GetTopicAvatar_Src(IGrouping<int, ResourceDto> groupedResourceDtos)
        {
            return groupedResourceDtos.FirstOrDefault(pg => pg.Topic_id == groupedResourceDtos.Key).Topic_Avatar_Src;
        }

        private async Task ClearLocalStorage()
        {
            await ManageResourcesLocalStorageService.RemoveCollection();
            await ManageUserProfileActivitie_ItemsLocalStorageService.RemoveCollection();
        }
    }
}