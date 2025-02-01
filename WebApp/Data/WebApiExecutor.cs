namespace WebApp.Data
{
    public class WebApiExecutor : IWebApiExecutor
    {
        private const string apiName = "ShirtApi";
        private readonly IHttpClientFactory _httpClientFactory;
        public WebApiExecutor(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = _httpClientFactory.CreateClient(apiName);
            return await httpClient.GetFromJsonAsync<T>(relativeUrl);
        }
    }
}
