using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private  readonly HttpClient _httpClient;
        public ChatGptController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: api/<ChatGptController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChatGptController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ChatGptController>
        [HttpPost]
        public async Task<string>  Post([FromBody] string value)
        {

            var apiKey = "";
            var model = "text-davinci-002";
            //var prompt = "What is the weather like today?";
           _httpClient.BaseAddress = new Uri( "https://api.openai.com/v1/completions");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            var content = new StringContent(
                   $"{{\"model\": \"{model}\", \"prompt\": \"{value}\"}}",
                   Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(
                   _httpClient.BaseAddress.ToString(), content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
           return responseString;

        }

        // PUT api/<ChatGptController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChatGptController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
