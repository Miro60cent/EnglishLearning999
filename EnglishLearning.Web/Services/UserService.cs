using Newtonsoft.Json;
using EnglishLearning.Models.Dtos;
using Topic.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text;

namespace Topic.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/User");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<UserDto>().ToList();
                    }

                    return await response.Content.ReadFromJsonAsync<List<UserDto>>();
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

        public async Task<UserDto> GetUser(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/User/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(UserDto);
                    }

                    return await response.Content.ReadFromJsonAsync<UserDto>();
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

        public async Task<UserDto> AddUser(UserAddDto userAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<UserAddDto>("api/User", userAddDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(UserDto);
                    }

                    return await response.Content.ReadFromJsonAsync<UserDto>();
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

        public async Task<UserDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(userUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
                var response = await httpClient.PatchAsync($"api/User/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }

                return null;
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/User/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }

                return default(UserDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
