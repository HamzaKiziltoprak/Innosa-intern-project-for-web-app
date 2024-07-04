using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UserManagementMvc.Models;

namespace UserManagementMvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public UsersController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Login");
            }

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("/user/getusers");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(responseString);
                return View(users);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
