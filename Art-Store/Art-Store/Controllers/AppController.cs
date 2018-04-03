using ArtStore.Services;
using ArtStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Controllers
{
    public class AppController:Controller
    {
        private readonly INullMailService _MailService;

        public AppController(INullMailService MailService)
        {
            _MailService = MailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _MailService.SendMessage("ciorteanucozmin@gmail.com",model.Email,model.Subject,model.Message);
                ViewBag.UserMessage = "Message sent";
                ModelState.Clear();
            }
            return View();
        }
    }
}
