using Newtonsoft.Json;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text;

namespace EnglishLearning.Web.Services
{
    public class ResourceService : IResourceService
    {
        private readonly HttpClient httpClient;

        public ResourceService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ResourceDto>> GetResources()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Resource");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ResourceDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ResourceDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Код состояния HTTP: {response.StatusCode} сообщение: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<ResourceDto> GetResource(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Resource/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ResourceDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ResourceDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Код состояния HTTP: {response.StatusCode} сообщение: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<ResourceDto> AddResource(ResourceAddDto ResourceAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<ResourceAddDto>("api/Resource", ResourceAddDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ResourceDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ResourceDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Код состояния HTTP: {response.StatusCode} сообщение: {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResourceDto> UpdateResource(int id, ResourceAddDto ResourceAddDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(ResourceAddDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
                var response = await httpClient.PatchAsync($"api/Resource/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ResourceDto>();
                }

                return null;
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<ResourceDto> DeleteResource(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Resource/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ResourceDto>();
                }

                return default(ResourceDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<TopicDto>> GetTopics()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Resource/GetTopics");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<TopicDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<TopicDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Код состояния HTTP: {response.StatusCode} сообщение: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ResourceDto>> GetResourceByTopic(int TopicId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Resource/GetResourcesByTopic/{TopicId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ResourceDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ResourceDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Код состояния HTTP: {response.StatusCode} сообщение: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}