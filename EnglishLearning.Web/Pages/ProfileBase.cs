using Microsoft.AspNetCore.Components;
using 
    .Models.Dtos;
using Topic.Web.Services.Contracts;

namespace Topic.Web.Pages
{
    public class ProfileBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IManageUsersLocalStorageService ManageUsersLocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<UserDto> Users { get; set; }

        public UserDto User { get; set; }

        public string ErrorMessage { get; set; }

        public string NewPassword1 { get; set; }

        public string NewPassword2 { get; set; }

        public string OldPassword { get; set; }

        public string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = await ManageUsersLocalStorageService.GetCollection();
                User = Users.Find(u => u.Id == HardCoded.UserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task UpdateUser_Click()
        {
            try
            {
                if (NewPassword1 == null && NewPassword2 == null && OldPassword == null)
                {
                    var userUpdateDto = new UserUpdateDto
                    {
                        Avatar_Src = User.Avatar_Src,
                        FullName = User.FullName,
                        Password = User.Password,
                        Telephone = User.Telephone,
                        Discount_Percent = User.Discount_Percent,
                        Is_Admin = User.Is_Admin
                    };

                    var returnedUpdateItemDto = await this.UserService.UpdateUser(User.Id, userUpdateDto);
                    await ManageUsersLocalStorageService.SaveCollection(Users);
                    Message = "Изменения успешно внесены";
                }
                else
                {
                    if (OldPassword == User.Password && NewPassword1 == NewPassword2)
                    {
                        var userUpdateDto = new UserUpdateDto
                        {
                            Avatar_Src = User.Avatar_Src,
                            FullName = User.FullName,
                            Password = NewPassword1,
                            Telephone = User.Telephone,
                            Discount_Percent = User.Discount_Percent,
                            Is_Admin = User.Is_Admin
                        };

                        var returnedUpdateItemDto = await this.UserService.UpdateUser(User.Id, userUpdateDto);
                        await ManageUsersLocalStorageService.SaveCollection(Users);
                        Message = "Изменения успешно внесены";

                        NewPassword1 = null;
                        NewPassword2 = null;
                        OldPassword = null;
                    }
                    else Message = "Неверно введён прежний пароль или не совпадают новые пароли"; ;
                }
            }
            catch (Exception e)
            {
                Message = "Не получилось внести изменения " + e;
            }
        }

        protected async Task UpdateAvatar_Click()
        {
            try
            {
                var userUpdateDto = new UserUpdateDto
                {
                    Avatar_Src = User.Avatar_Src,
                    FullName = User.FullName,
                    Password = User.Password,
                    Telephone = User.Telephone,
                    Discount_Percent = User.Discount_Percent,
                    Is_Admin = User.Is_Admin
                };

                var returnedUpdateItemDto = await this.UserService.UpdateUser(User.Id, userUpdateDto);
                await ManageUsersLocalStorageService.SaveCollection(Users);
                Message = "Изменения успешно внесены";
            }
            catch (Exception e)
            {
                Message = "Не получилось внести изменения " + e;
            }
        }
    }
}