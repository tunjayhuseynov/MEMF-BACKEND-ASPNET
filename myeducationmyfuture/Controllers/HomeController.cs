using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using myeducationmyfuture.Models;

namespace myeducationmyfuture.Controllers
{
    public class HomeController : Controller
    {
        MEMFEntities db = new MEMFEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Blog(int id=1)
        {
            ModelDB model = new ModelDB();
            model.blogList = db.Blogs.ToList();
            model.Index = id;

            return View(model);
        }

        public ActionResult News(int id=1)
        {
            ModelDB model = new ModelDB();
            model.news = db.Blogs.Where(w=> w.id == id).FirstOrDefault();
            model.Index = id;
            model.blogList = db.Blogs.ToList();
            return View(model);
        }

        public ActionResult SendEmail(string email, string name, string msg)
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "tuncayhuseynov@gmail.com";
            string password = "5591980supertun";
            string emailTo = "Tabib.baku@gmail.com";


            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = "MEMF Support Desk";
                mail.Body = "Name: " + name + "\nEmail: " + email + "\nMessage: " + msg;
                mail.IsBodyHtml = false;
                // Can set to false, if you are sending pure text.



                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }

            return RedirectToAction("Index");
        }

    }
}