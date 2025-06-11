using Microsoft.AspNetCore.Components;
using Topic.Models.Dtos;
using Topic.Web.Services.Contracts;

namespace Topic.Web.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject] 
        public IMyLocalStorageService MyLocalStorageService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IManageUsersLocalStorageService ManageUsersLocalStorageService { get; set; }

        [Inject]
        public IResourceProfileService ResourceProfileService { get; set; }

        [Inject]
        public IManageResourceProfile_ItemsLocalStorageService ManageResourceProfile_ItemsLocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<UserDto> Users { get; set; }

        public UserDto User { get; set; }

        public List<ResourceProfileDto> ResourceProfiles { get; set; }

        public string ErrorMessage { get; set; }

        public LoginBase()
        {
            LoginData = new LoginDto();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = await ManageUsersLocalStorageService.GetCollection();
                ResourceProfiles = await ManageResourceProfile_ItemsLocalStorageService.GetResourceProfilesCollection();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public LoginDto LoginData { get; set; }

        protected async Task LoginAsync()
        {
            User = Users.Find(u => u.Email == LoginData.UserName && u.Password == LoginData.Password);

            if (User != null)
            {
                var token = new SecurityTokenDto
                {
                    AccessToken = LoginData.Password,
                    UserName = LoginData.UserName,
                    ExpiredAt = DateTime.UtcNow.AddDays(1)
                };

                HardCoded.UserId = User.Id;
                var ResourceProfileDto = ResourceProfiles.Find(o => o.UserId == User.Id && o.Paid == false);
                HardCoded.ResourceProfileId = ResourceProfileDto.Id;



                var t = HardCoded.UserId;
                var y = HardCoded.ResourceProfileId;

                await MyLocalStorageService.SetAsync(nameof(SecurityTokenDto), token);

                NavigationManager.NavigateTo("/", true);
            }
            else
            {
                NavigationManager.NavigateTo("/login", true);
            }
        }
    }
}