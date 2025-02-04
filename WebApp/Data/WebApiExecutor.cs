using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApp.Data
{
    public class WebApiExecutor : IWebApiExecutor
    {
        private const string apiName = "ShirtsApi";
        private const string authorityApiName = "AuthorityApi";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebApiExecutor(IHttpClientFactory httpClientFactory,IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = _httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);
            await HandlePossibleErrors(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = _httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
            await HandlePossibleErrors(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = _httpClientFactory.CreateClient(apiName);
         //   await AddJwtToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            await HandlePossibleErrors(response);
        }
        public async Task InvokeDelete<T>(string relativeUrl)
        {
            var httpClient = _httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.DeleteAsync(relativeUrl);
            await HandlePossibleErrors(response);
        }
        public async Task HandlePossibleErrors(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var errorJson = await httpResponseMessage.Content.ReadAsStringAsync();
                throw new WebApiException(errorJson);
            }
        }
        private async Task AddJwtToHeader(HttpClient httpClient)
        {
            JwtToken? token = null;
            string? strToken = _httpContextAccessor.HttpContext.Session.GetString("access_token");
            if(!string.IsNullOrWhiteSpace(strToken))
            {
              token = JsonConvert.DeserializeObject<JwtToken>(strToken);
            }
            if(token == null)
            {
                var clientId = _configuration.GetValue<string>("ClientId");
                var secretKey = _configuration.GetValue<string>("Secret");
                var authoClient = _httpClientFactory.CreateClient(authorityApiName);
                var response = await authoClient.PostAsJsonAsync("auth", new AppCredential()
                {
                    ClientId = clientId,
                    Secret = secretKey
                });
                response.EnsureSuccessStatusCode();
                strToken = await response.Content.ReadAsStringAsync();

                token = JsonConvert.DeserializeObject<JwtToken>(strToken);
                _httpContextAccessor.HttpContext?.Session.SetString("access_token", strToken);
            }
           
          
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
    }
}