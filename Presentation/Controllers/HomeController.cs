using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Business;
using Microsoft.AspNet.Identity;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private PasteBusiness dbPastes = new PasteBusiness(new DBContext());
        private CommentBusiness dbComments = new CommentBusiness(new DBContext());
        private RatingBusiness dbRatings = new RatingBusiness(new DBContext());

        // GET: /
        public ActionResult Index()
        {
            var res = dbPastes.GetAll();
            res.Reverse();
            return View(res);
        }

        public ActionResult MyPastes()
        {
            if (User.Identity.Name != null)
            {
                return View(dbPastes.GetAllByAuthor(User.Identity.Name));
            }
            return View(new List<Paste>());
        }

        [HttpPost]
        public void Rate(int rate, int id)
        {
            dbRatings.Rate(id, rate, User.Identity.Name);
        }

        // GET: Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                Paste paste = dbPastes.Get(id);
                ViewBag.Comments = dbComments.GetAll(id);
                ViewBag.Comments.Reverse();
                ViewBag.Rating = dbRatings.Get(id);
                return View(paste);
            }
            catch (ArgumentException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (KeyNotFoundException)
            {
                return HttpNotFound();
            }
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Description,IsHidden,Expieres")] Paste paste)
        {
            if (ModelState.IsValid)
            {
                paste.AuthorID = User.Identity.Name;
                dbPastes.Add(paste);
                return RedirectToAction("Index");
            }

            return View(paste);
        }

        // GET: Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                Paste paste = dbPastes.Get(id);
                return View(paste);
            }
            catch (ArgumentException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (KeyNotFoundException)
            {
                return HttpNotFound();
            }
        }

        // POST: Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Description,IsHidden,Expieres")] Paste paste)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbPastes.Update(paste, User.Identity.Name);
                }
                catch (InvalidOperationException)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
                return RedirectToAction("MyPastes");
            }
            return View(paste);
        }

        // GET: Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                Paste paste = dbPastes.Get(id);
                return View(paste);
            }
            catch (InvalidOperationException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            catch (KeyNotFoundException)
            {
                return HttpNotFound();
            }
        }

        // POST: Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                dbPastes.Delete(id, User.Identity.Name);
            }
            catch (InvalidOperationException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return RedirectToAction("MyPastes");
        }
    }
}