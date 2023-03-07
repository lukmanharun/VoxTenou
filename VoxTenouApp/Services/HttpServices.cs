using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using VoxTenouApp.Interfaces;
using VoxTenouApp.Models;
using VoxTenouApp.Models.Organizer;
using VoxTenouApp.Models.SportEvent;
using VoxTenouApp.Models.User;

namespace VoxTenouApp.Services
{
    public class HttpServices : IHttpServices
    {
        private readonly HttpClient httpClient;
        public HttpServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        #region User 
        public async Task<(bool success, string message)> CreateUser(CreateUserDTO userModel)
        {
            var pyload = new StringContent(JsonSerializer.Serialize(userModel, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/users");
            request.Content = pyload;
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return (true, "Success");
            }
            return (false, response.ReasonPhrase);
        }
        public async Task<(bool success, string message, ResponseAuth? auth)> SignIn(SignInDTO userModel)
        {
            var pyload = new StringContent(JsonSerializer.Serialize(userModel, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/users/login");
            request.Content = pyload;
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var res = JsonSerializer.Deserialize<ResponseAuth>(responseJson);
                return (true, "Success", res);
            }
            return (false, response.ReasonPhrase, null);
        }
        #endregion
        #region Organizer
        public async Task<ResponseApiPagination<GridOrganizer>> GetListOrganizer(int page,int pageNumber, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/organizers?page={page}&perPage={pageNumber}");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                (
                    scheme:"Bearer",
                    parameter:token
                );
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var res = JsonSerializer.Deserialize<ResponseApiPagination<GridOrganizer>>(responseJson) ?? new ResponseApiPagination<GridOrganizer>();
                return res;
            }
            return new ResponseApiPagination<GridOrganizer>() { data = new List<GridOrganizer>() };
        }
        public async Task<ResponseApiPagination<GridOrganizer>> GetListOrganizer(int page, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/organizers?page={page}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var res = JsonSerializer.Deserialize<ResponseApiPagination<GridOrganizer>>(responseJson) ?? new ResponseApiPagination<GridOrganizer>();
                return res;
            }
            return new ResponseApiPagination<GridOrganizer>() { data = new List<GridOrganizer>() };
        }
        public async Task<OrganizerFormSubmitDto> OrganizerById(string token, long Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/organizers/{Id}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<OrganizerFormSubmitDto>(responseJson);
            }
            return null;
        }
        public async Task<bool> AddNewOrgnizer(string token, OrganizerFormDto form)
        {
            var pyload = new StringContent(JsonSerializer.Serialize(form, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/organizers");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            request.Content = pyload;
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        public async Task<bool> EditOrgnizer(string token, OrganizerFormDto form, long Id)
        {
            var pyload = new StringContent(JsonSerializer.Serialize(form, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/v1/organizers/{Id}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            request.Content = pyload;
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        public async Task<bool> DeleteOrgnizer(string token, long Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/v1/organizers/{Id}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        #endregion

        #region Sport Event
        public async Task<ResponseApiPagination<GridSportEvent>> GetListSportEvent(int page, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/sport-events?page={page}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var res = JsonSerializer.Deserialize<ResponseApiPagination<GridSportEvent>>(responseJson) ?? 
                    new ResponseApiPagination<GridSportEvent>();
                return res;
            }
            return new ResponseApiPagination<GridSportEvent>() { data = new List<GridSportEvent>() };
        }
        public async Task<GridSportEvent> SportEventById(string token, long Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/sport-events/{Id}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<GridSportEvent>(responseJson);
            }
            return null;
        }
        public async Task<bool> AddNewSportEvent(string token, SubmitSportEventDto form)
        {
            var pyload = new StringContent(JsonSerializer.Serialize(form, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/sport-events");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            request.Content = pyload;
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        public async Task<bool> EditSportEvent(string token, SubmitSportEventDto form, long Id)
        {
            var pyload = new StringContent(JsonSerializer.Serialize(form, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/v1/sport-events/{Id}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            request.Content = pyload;
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        public async Task<bool> DeleteSportEvent(string token, long Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/v1/sport-events/{Id}");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        #endregion
    }
}
