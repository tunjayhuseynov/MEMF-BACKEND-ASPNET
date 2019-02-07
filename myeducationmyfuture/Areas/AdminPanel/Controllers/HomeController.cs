using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myeducationmyfuture.Models;

namespace myeducationmyfuture.Areas.AdminPanel.Controllers
{

    public class HomeController : Controller
    {
        private MEMFEntities db = new MEMFEntities();

        // GET: AdminPanel/Home
        [Auth]
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }

        // GET: AdminPanel/Home/Details/5
        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: AdminPanel/Home/Create
        [Auth]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,text")] Blog blog, HttpPostedFileBase file )
        {
            if (ModelState.IsValid)
            {
                string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/public/img/uploaded"), Filename);
                file.SaveAs(myfile);
                blog.imagepath = Filename;
                blog.date = DateTime.Now.Date;
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: AdminPanel/Home/Edit/5
        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: AdminPanel/Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,text")] Blog blog, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                db.Entry(blog).State = EntityState.Modified;
                if (file != null)
                {
                    string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                    var myfile = System.IO.Path.Combine(Server.MapPath("~/public/img/uploaded"), Filename);
                    file.SaveAs(myfile);
                    blog.imagepath = Filename;
                    db.Entry(blog).Property(w => w.imagepath).IsModified = true;
                }
                else
                {
                    db.Entry(blog).Property(w => w.imagepath).IsModified = false;
                }
                db.Entry(blog).Property(w => w.date).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: AdminPanel/Home/Delete/5
        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: AdminPanel/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult LogPage()
        {
            return View();
        }

        public ActionResult LogIn(string pass, string user)
        {
            string password = "12345memf";
            string usr = "adminpanel";
            if(pass == password && user == usr)
            {
                Session["LogedIn"] = true;
                return RedirectToAction("Index");
            }

            Session["Error"] = true;
            return RedirectToAction("LogPage");
        }

        public ActionResult LogOut()
        {
            Session["LogedIn"] = null;
            return RedirectToAction("LogPage");
        }

    }
}
