using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_WebApp.ViewModels;
using System.Text;

namespace Silicon_WebApp.Controllers
{
    public class DefaultController(HttpClient httpClient) : Controller

    {
        private readonly HttpClient _httpClient = httpClient;

        public IActionResult Home()
        {

           
            return View();
       
        
        
        }
        [HttpPut]
        public async Task<IActionResult> Subscribe(SubscribeViewModel model)
        {
            if (ModelState.IsValid)
            {
               var content= new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response=await _httpClient.PostAsync("https://localhost:7121/api/subscribe", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["StatusMessage"]="You have successfully subscribed";
                }
                else if (response.StatusCode== System.Net.HttpStatusCode.Conflict)
                {
                    TempData["StatusMessage"]="You have already subscribed";
                }
                else
                {
                    TempData["StatusMessage"]="An error occurred while processing your request";
                }
            }
            return RedirectToAction("Home", "Default", "subscribe");
        }   

    }



}
