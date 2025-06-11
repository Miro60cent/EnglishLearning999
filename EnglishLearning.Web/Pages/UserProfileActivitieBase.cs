using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;
using System.Globalization;

namespace EnglishLearning.Web.Pages
{
    public class UserProfileActivitieBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public IUserProfileActivitieService UserProfileActivitieService { get; set; }

        [Inject]
        public IManageUserProfileActivitie_ItemsLocalStorageService ManageUserProfileActivitie_ItemsLocalStorageService { get; set; }

        public List<UserProfileActivitie_ItemDto> UserProfileActivitieItems { get; set; }

        public string ErrorMessage { get; set; }

        protected string TotalPrice { get; set; }

        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                UserProfileActivitieItems = await ManageUserProfileActivitie_ItemsLocalStorageService.GetCollection();
                UserProfileActivitieChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteUserProfileActivitie_Item_Click(int id)
        {
            var UserProfileActivitieItemDto = await UserProfileActivitieService.DeleteItem(id);
            await RemoveUserProfileActivitie_Item(id);
            UserProfileActivitieChanged();
        }

        protected async Task UpdateQuantityUserProfileActivitie_Item_Click(int id, int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    var updateItemDto = new UserProfileActivitie_ItemQuantityUpdateDto
                    {
                        UserProfileActivitie_ItemId = id,
                        Quantity = quantity
                    };

                    var returnedUpdateItemDto = await this.UserProfileActivitieService.UpdateQuantity(updateItemDto);
                    await UpdateUserProfileActivitieTotalPrice(returnedUpdateItemDto);
                    UserProfileActivitieChanged();
                }
                else
                {
                    var item = this.UserProfileActivitieItems.FirstOrDefault(i => i.Id == id);

                    if (item != null)
                    {
                        item.Quantity = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateUserProfileActivitieTotalPrice(UserProfileActivitie_ItemDto UserProfileActivitieItemDto)
        {
            var item = GetUserProfileActivitie_Item(UserProfileActivitieItemDto.Id);

            if (item != null)
            {
                item.TotalPrice = UserProfileActivitieItemDto.Price * UserProfileActivitieItemDto.Quantity;
            }

            await ManageUserProfileActivitie_ItemsLocalStorageService.SaveCollection(UserProfileActivitieItems);
        }

        private void CalculateUserProfileActivitieSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = this.UserProfileActivitieItems.Sum(p => p.TotalPrice).ToString("C2", new System.Globalization.CultureInfo("uk-UA"));
        }

        private void SetTotalQuantity()
        {
            TotalQuantity = this.UserProfileActivitieItems.Sum(p => p.Quantity);
        }

        private UserProfileActivitie_ItemDto GetUserProfileActivitie_Item(int id)
        {
            return UserProfileActivitieItems.FirstOrDefault(i => i.Id == id);
        }

        private async Task RemoveUserProfileActivitie_Item(int id)
        {
            var UserProfileActivitieItemDto = GetUserProfileActivitie_Item(id);
            UserProfileActivitieItems.Remove(UserProfileActivitieItemDto);
            await ManageUserProfileActivitie_ItemsLocalStorageService.SaveCollection(UserProfileActivitieItems);
        }

        private void UserProfileActivitieChanged()
        {
            CalculateUserProfileActivitieSummaryTotals();
            UserProfileActivitieService.RaiseEventOnUserProfileActivitieChanged(TotalQuantity);
        }
    }
}