using Newtonsoft.Json;
using System.Text;
using WebHouse.Web.Models;
using WebHouse.Web.Services.IServices;
using WebVilla.Utility;

namespace WebHouse.Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponseModel { get; set;}
        public IHttpClientFactory HttpClient { get; set;}

        public BaseService(IHttpClientFactory httpClient)
        {
            this.ResponseModel = new();
            this.HttpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("WebAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if(apiRequest.Data!= null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;                    
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResonse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResonse;
            }
            catch (Exception e)
            {

                var dto = new APIResponse
                {
                    ErrorMessages = new List<string>
                   {
                       Convert.ToString(e.Message)

                   },
                    IsSuccess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
