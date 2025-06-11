using Newtonsoft.Json;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text;

namespace EnglishLearning.Web.Services
{
    public class TopicService : ITopicService
    {
        private readonly HttpClient httpClient;

        public TopicService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<TopicDto>> GetTopics()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Topic");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<TopicDto>().ToList();
                    }

                    return await response.Content.ReadFromJsonAsync<List<TopicDto>>();
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

        public async Task<TopicDto> GetTopic(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Topic/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(TopicDto);
                    }

                    return await response.Content.ReadFromJsonAsync<TopicDto>();
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

        public async Task<TopicDto> AddTopic(TopicAddDto TopicAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<TopicAddDto>("api/Topic", TopicAddDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(TopicDto);
                    }

                    return await response.Content.ReadFromJsonAsync<TopicDto>();
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

        public async Task<TopicDto> UpdateTopic(int id, TopicAddDto TopicAddDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(TopicAddDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
                var response = await httpClient.PatchAsync($"api/Topic/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TopicDto>();
                }

                return null;
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<TopicDto> DeleteTopic(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Topic/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TopicDto>();
                }

                return default(TopicDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
