using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Business;
using Data;

namespace Presentation.Controllers
{
    public class CommentsController : Controller
    {
        private CommentBusiness dbComments = new CommentBusiness(new DBContext());

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PasteId,Content")] Comment comment)
        {
            comment.Author = User.Identity.Name;
            dbComments.Add(comment);
            return RedirectToAction("Details", "Home", new { id = comment.PasteId});
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            dbComments.Delete(id, User.Identity.Name);
        }
    }
}
