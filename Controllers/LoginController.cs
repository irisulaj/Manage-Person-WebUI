using ManagePersonWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace ManagePersonWebUI.Controllers
{
    public class LoginController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:44308/api/");

        private readonly HttpClient _httpClient;
        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }

        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginUser(LoginViewModel user)
        {
            user.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.Password))).Replace("-", "");

            var inputJson = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(baseUrl + "login", content).Result;
            if (response.IsSuccessStatusCode)
                       
            {
               string token = response.Content.ReadAsStringAsync().Result;

               if (token != null)
               {
                   HttpContext.Session.SetString("newToken", token);


                    return RedirectToAction("Index","Home");
               }
                            
                   
            }
             return  RedirectToAction("Index","Home");

        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("newToken");
            return RedirectToAction("Index","Login");
        }
    }
}
