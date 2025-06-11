using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    .Models.Dtos;
using Topic.Web.Services.Contracts;

namespace Topic.Web.Pages
{
    public class CheckoutBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<ResourceProfile_ItemDto> ResourceProfile_Items { get; set; }

        protected int TotalQuantity { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }

        [Inject]
        public IResourceProfileService ResourceProfileService { get; set; }

        [Inject]
        public IManageResourceProfile_ItemsLocalStorageService ManageResourceProfile_ItemsLocalStorageService { get; set; }

        protected string DisplayButtons { get; set; } = "block";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ResourceProfile_Items = await ManageResourceProfile_ItemsLocalStorageService.GetCollection();

                if (ResourceProfile_Items != null && ResourceProfile_Items.Count() > 0)
                {
                    Guid ResourceProfileGuid = Guid.NewGuid();

                    PaymentAmount = ResourceProfile_Items.Sum(p => p.TotalPrice);
                    TotalQuantity = ResourceProfile_Items.Sum(p => p.Quantity);
                    PaymentDescription = $"O_{HardCoded.UserId}_{ResourceProfileGuid}";
                }
                else
                {
                    DisplayButtons = "none";
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}