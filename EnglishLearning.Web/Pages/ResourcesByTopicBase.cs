using Microsoft.AspNetCore.Components;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Pages
{ 
    public class ResourcesByTopicBase : ComponentBase
    {
        [Parameter]
        public int TopicId { get; set; }

        [Inject]
        public IResourceService ResourceService { get; set; }

        [Inject]
        public IManageResourcesLocalStorageService ManageResourcesLocalStorageService { get; set; }

        public IEnumerable<ResourceDto> Resources { get; set; }

        public string TopicName { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Resources = await GetResourceCollectionByTopicId(TopicId);

                if (Resources != null && Resources.Count() > 0)
                {
                    var ResourceDto = Resources.FirstOrDefault(p => p.Topic_id == TopicId);

                    if (ResourceDto != null)
                    {
                        TopicName = ResourceDto.Topic_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task<IEnumerable<ResourceDto>> GetResourceCollectionByTopicId(int Topicid)
        {
            var ResourceCollection = await ManageResourcesLocalStorageService.GetCollection();

            if (ResourceCollection != null)
            {
                return ResourceCollection.Where(p => p.Topic_id == Topicid);
            }
            else
            {
                return await ResourceService.GetResourceByTopic(Topicid);
            }
        }
    }
}