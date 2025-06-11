using Microsoft.AspNetCore.Components;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;

namespace EnglishLearning.Web.Shared
{
    public class TopicsNavMenuBase : ComponentBase
    {
        [Inject]
        public IEnglishLearning EnglishLearning { get; set; }

        public IEnumerable<TopicDto> TopicDtos { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                TopicDtos = await EnglishLearningService.GetTopics();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}