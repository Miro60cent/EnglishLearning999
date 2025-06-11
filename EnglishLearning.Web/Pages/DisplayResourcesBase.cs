using Microsoft.AspNetCore.Components;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web.Pages
{
    public class DisplayResourceBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ResourceDto> Resources { get; set; }
    }
}