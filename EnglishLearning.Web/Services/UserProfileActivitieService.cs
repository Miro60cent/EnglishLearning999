using Newtonsoft.Json;
using EnglishLearning.Models.Dtos;
using EnglishLearning.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text;

namespace EnglishLearning.Web.Services
{
    public class UserProfileActivitieService : IUserProfileActivitieService
    {
        private readonly HttpClient httpClient;
        public event Action<int> OnUserProfileActivitieChanged;

        public UserProfileActivitieService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<UserProfileActivitieDto>> GetUserProfileActivities()
        {
            try
            {
                var response = await httpClient.GetAsync($"api/UserProfileActivitie/GetUserProfileActivities");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<UserProfileActivitieDto>().ToList();
                    }

                    return await response.Content.ReadFromJsonAsync<List<UserProfileActivitieDto>>();
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

        public async Task<List<UserProfileActivitie_ItemDto>> GetItems(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/UserProfileActivitie/{userId}/GetItems");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<UserProfileActivitie_ItemDto>().ToList();
                    }

                    return await response.Content.ReadFromJsonAsync<List<UserProfileActivitie_ItemDto>>();
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

        public async Task<UserProfileActivitie_ItemDto> AddItem(UserProfileActivitie_ItemToAddDto UserProfileActivitie_ItemToAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<UserProfileActivitie_ItemToAddDto>("api/UserProfileActivitie", UserProfileActivitie_ItemToAddDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(UserProfileActivitie_ItemDto);
                    }

                    return await response.Content.ReadFromJsonAsync<UserProfileActivitie_ItemDto>();
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

        public async Task<UserProfileActivitie_ItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/UserProfileActivitie/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserProfileActivitie_ItemDto>();
                }

                return default(UserProfileActivitie_ItemDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<UserProfileActivitie_ItemDto> UpdateQuantity(UserProfileActivitie_ItemQuantityUpdateDto UserProfileActivitie_ItemQuantityUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(UserProfileActivitie_ItemQuantityUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
                var response = await httpClient.PatchAsync($"api/UserProfileActivitie/{UserProfileActivitie_ItemQuantityUpdateDto.UserProfileActivitie_ItemId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserProfileActivitie_ItemDto>();
                }

                return null;
            }
            catch (Exception e)
            {
                //Log exception
                throw;
            }
        }

        public void RaiseEventOnUserProfileActivitieChanged(int totalQuantity)
        {
            if (OnUserProfileActivitieChanged != null)
            {
                OnUserProfileActivitieChanged.Invoke(totalQuantity);
            }
        }
    }
}