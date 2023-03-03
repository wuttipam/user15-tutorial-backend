using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;


namespace Tutorial.Api.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class MeController : ControllerBase
    {
        #region Property
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public MeController(ILogger<MeController> logger)
        {
            _logger = logger;
        }
        #endregion

        [HttpGet("/")]
        public async Task<ContentResult> Message(){
            return new ContentResult{
                ContentType = "text/html",
                Content = await HttpRequestAsync("") + " <b>Dotnet Tutorial Backend v0.0.1</b>"
            };
        }   

        public async Task<string> HttpRequestAsync(string url){
            if (string.IsNullOrEmpty(url)){
                return "";
            }

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content +"<br> Render by ";
            }
            return "";
        }
    }
}