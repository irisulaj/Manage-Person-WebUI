using ManagePersonWebUI.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ManagePersonWebUI.Controllers
{
    public class PersonController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:44308/api/");
   
        private readonly HttpClient _httpClient;

        public PersonController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Person()
        {
            List<PersonViewModel> persons = new List<PersonViewModel>();

            var userToken = HttpContext.Session.GetString("newToken");

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + userToken);
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl+"person").Result;

            if (response.IsSuccessStatusCode)
            {
                string getData = response.Content.ReadAsStringAsync().Result;
                persons = JsonConvert.DeserializeObject<List<PersonViewModel>>(getData);
                return View(persons);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userToken = HttpContext.Session.GetString("newToken");
            if (userToken != null) 
            { 
             return View();
            }
            else 
            { return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]

        public IActionResult AddPerson(PersonViewModel person) 
        {
            try 
            { 
                string data = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var userToken = HttpContext.Session.GetString("newToken");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + userToken);
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "person", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Person");
                }
                else 
                {
                    return RedirectToAction("Index", "Login");
                }
            
            }
            catch(Exception ex)
            {
                throw ex;

            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]

        public IActionResult Edit(int id) 
        { 
            PersonViewModel person = new PersonViewModel();
            var userToken = HttpContext.Session.GetString("newToken");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + userToken);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "person/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                person = JsonConvert.DeserializeObject<PersonViewModel>(data);
                return View(person);

            }
            else 
            {
                return RedirectToAction("Index", "Login");
            }


        }

        [HttpPost]
        public IActionResult EditPerson(PersonViewModel person)
        {
            try
            {
                string data = JsonConvert.SerializeObject(person);
                
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var userToken = HttpContext.Session.GetString("newToken");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + userToken);
                HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "person/"+person.IdPerson, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Person");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {

            PersonViewModel person = new PersonViewModel();
            var userToken = HttpContext.Session.GetString("newToken");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + userToken);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "person/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                person = JsonConvert.DeserializeObject<PersonViewModel>(data);
                return View(person);

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            var userToken = HttpContext.Session.GetString("newToken");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + userToken);
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "person/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Person");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


    }
}
