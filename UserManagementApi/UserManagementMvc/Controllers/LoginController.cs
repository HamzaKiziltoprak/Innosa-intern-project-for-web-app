using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UserManagementMvc.Models;

namespace UserManagementMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public LoginController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Microsoft.AspNetCore.Identity.Data.LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(_configuration["ApiUrl"]);

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/auth/authenticate", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<TokenResponse>(responseString);

                    HttpContext.Session.SetString("JWToken", token.Token);
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }

            return View(model);
        }
    }
}
