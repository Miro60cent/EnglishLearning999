using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Web.Services.Contracts
{
    public interface IManageResourcesLocalStorageService
    {
        Task<IEnumerable<ResourceDto>> GetCollection();
        Task SaveCollection(List<ResourceDto> ResourceDto);
        Task RemoveCollection();
    }
}