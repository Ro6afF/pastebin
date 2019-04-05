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
        private PasteBusiness db = new PasteBusiness();

        // GET: /
        public ActionResult Index()
        {
            return View(db.GetAll());
        }

        // GET: Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                Paste paste = db.Get(id);
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
                paste.AuthorID = User.Identity.GetUserId<int>();
                db.Add(paste);
                return RedirectToAction("Index");
            }

            return View(paste);
        }

        // GET: Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.userID = User.Identity.GetUserId<string>();
            try
            {
                Paste paste = db.Get(id);
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
                db.Update(paste);
                return RedirectToAction("Index");
            }
            return View(paste);
        }

        // GET: Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                Paste paste = db.Get(id);
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

        // POST: Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}