using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myFirstWebApp.Models;


namespace myFirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string message, string subject)
        {
            SendContactEmail(name, message, subject);
            return Content("Thanks for contacting us! We'll be in contact with you soon!");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void SendContactEmail(string user, string message, string subject)
        {
            // Your hard-coded email values (where the email will be sent from), this could be
            // define in a config file, etc.
            var email = "sikorsk4@gmail.com";
            var password = "{Password}"; 

            // Your target (you may want to ensure that you have a property within your form so that you know
            // who to send the email to
            string address = "dicedlr99@gmail.com";

            // Builds a message and necessary credentials (example using Gmail)
            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            // This email will be sent from you
            // msg.From = new MailAddress(email);
            msg.From = new MailAddress(user);
            // Your target email address
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject + " from " + user;
            // Build the body of your email using the Body property of your message
            msg.Body = message;

            // Wires up and send the email
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
    }
}
