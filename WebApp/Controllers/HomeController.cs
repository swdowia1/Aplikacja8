using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Mail;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MailSetting _MailSetting;

        public HomeController(IMemoryCache memoryCache, MailSetting mailSetting)
        {
            _memoryCache = memoryCache;
            _MailSetting = mailSetting;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Currency()
        {
            string memoryList = "list_currency";
            var cacheData = _memoryCache.Get<List<Root>>(memoryList);
            if (cacheData != null)
            {
                return View(cacheData[0]);
            }

            var httpClient = new HttpClient();
            var baseAdress = "http://api.nbp.pl/api/exchangerates/tables/c/?format=json";
            httpClient.BaseAddress = new Uri(baseAdress);

            var response = httpClient.GetAsync(httpClient.BaseAddress).Result;

            var contentJson = response.Content.ReadAsStringAsync().Result;
            cacheData = JsonConvert.DeserializeObject<List<Root>>(contentJson);
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);

            _memoryCache.Set(memoryList, cacheData, expirationTime);


            return View(cacheData[0]);
        }
        public IActionResult Mail()
        {

            return View("Mail", "Nie wype³niony to adres docelowy :" + _MailSetting.MailTo);
        }
        [HttpPost]

        public JsonResult SendMail([FromBody] MailSend _mail)
        {
            string mailto = string.IsNullOrEmpty(_mail.MailTo) ? _MailSetting.MailTo : _mail.MailTo;
            try
            {
                string mailCredential = _MailSetting.MailCred;
                MailMessage msg = new MailMessage();

                msg.To.Add(new MailAddress(mailto));
                msg.From = new MailAddress(mailCredential, User.Identity.Name);
                msg.Subject = _mail.Title;
                msg.Body = _mail.Body;
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(mailCredential, _MailSetting.PassWord);
                client.Port = int.Parse(_MailSetting.Port);
                client.Host = _MailSetting.Host;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                client.Send(msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { result = ex.Message });
            }

            return Json(new { result = "Wys³ano do " + mailto });

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
