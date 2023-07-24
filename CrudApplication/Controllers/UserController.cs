using CRUDAPI.Models;
using CrudApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CrudApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        Uri baseAddress = new Uri("https://localhost:7025/api");
        private readonly HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public ActionResult Profile() {
            return View();

        }

        [HttpPost]
        public ActionResult Profile(UserProfile user) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync(client.BaseAddress + "/Profile/Post", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    ViewData["successMessage"] = "product created";
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ViewData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult Index(string searchBy, string search)
        {
            List<UserView> users = new List<UserView>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/User/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<UserView>>(data);
            }
            if (searchBy == "ID")
            {
                return View(users.Where(x => x.Id == Int32.Parse(search) || search == null).ToList());
            }
            return View(users.Where(x => x.Username == search || search == null).ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserView user)
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync(client.BaseAddress + "/User/Post", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    ViewData["successMessage"] = "product created";
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ViewData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserView user = new UserView();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/User/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserView>(data);
            }

            return View(user);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            UserProfile user = new UserProfile();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Profile/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserProfile>(data);
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserView user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage res = client.PutAsync(client.BaseAddress + "/User/Put", content).Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserView user = new UserView();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/User/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserView>(data);
            }

            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConf(int id)
        {
            HttpResponseMessage res = client.DeleteAsync(client.BaseAddress + "/User/Delete/"+ id).Result;
            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
     

    }
}