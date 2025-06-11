using Microsoft.AspNetCore.Components;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Pages
{
    public class ResourceProfileCustomizationBase : ComponentBase
    {
        [Inject]
        public IUserService ResourceService { get; set; }

        [Inject]
        public IUserService TopicService { get; set; }

        [Inject]
        public IManageResourcesLocalStorageService ManageResourcesLocalStorageService { get; set; }

        [Inject]
        public IManageTopicsLocalStorageService ManageTopicsLocalStorageService { get; set; }

        public IEnumerable<ResourceDto> Resources { get; set; }

        public List<TopicDto> Topics { get; set; }

        public ResourceDto Resource { get; set; }

        public TopicDto Topic { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        public string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                int hh = 1;
                int mm = 1;

                Resources = await ManageResourcesLocalStorageService.GetCollection();
                Topics = await ManageTopicsLocalStorageService.GetCollection();
                Resource = Resources.First<ResourceDto>(h => h.Id == hh);
                Topic = Topics.Find(m => m.Id == mm);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}