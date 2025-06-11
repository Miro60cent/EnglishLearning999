using Microsoft.AspNetCore.Components;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Pages
{
    public class ResourceDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IResourceService ResourceService { get; set; }

        [Inject]
        public IUserProfileActivitieService UserProfileActivitieService { get; set; }

        [Inject]
        public IManageResourcesLocalStorageService ManageResourcesLocalStorageService { get; set; }

        [Inject]
        public IManageUserProfileActivitie_ItemsLocalStorageService ManageUserProfileActivitie_ItemsLocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ResourceDto Resource { get; set; }

        public string ErrorMessage { get; set; }

        private List<UserProfileActivitie_ItemDto> UserProfileActivitie_Items { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                UserProfileActivitie_Items = await ManageUserProfileActivitie_ItemsLocalStorageService.GetCollection();
                Resource = await GetResourceById(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToUserProfileActivitie_Click(UserProfileActivitie_ItemToAddDto UserProfileActivitie_ItemToAddDto)
        {
            try
            {
                var UserProfileActivitie_ItemDto = await UserProfileActivitieService.AddItem(UserProfileActivitie_ItemToAddDto);

                if (UserProfileActivitie_ItemDto != null)
                {
                    UserProfileActivitie_Items.Add(UserProfileActivitie_ItemDto);
                    await ManageUserProfileActivitie_ItemsLocalStorageService.SaveCollection(UserProfileActivitie_Items);
                }

                NavigationManager.NavigateTo("/UserProfileActivitie");
            }
            catch (Exception)
            {
                //Log Exception
            }
        }

        private async Task<ResourceDto> GetResourceById(int id)
        {
            var ResourceDtos = await ManageResourcesLocalStorageService.GetCollection();

            if (ResourceDtos != null)
            {
                return ResourceDtos.SingleOrDefault(p => p.Id == id);
            }

            return null;
        }
    }
}