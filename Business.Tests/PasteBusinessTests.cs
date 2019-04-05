using System;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
    [TestClass]
    public class PasteBusinessTests
    {
        PasteBusiness db = new PasteBusiness();

        private void clearDB()
        {
            db.GetAll();
            foreach (var item in db.GetAll())
            {
                db.Delete(item.Id, item.AuthorID);
            }
        }

        [TestMethod]
        public void TestEmptyListing()
        {
            clearDB();
            Assert.AreEqual(0, db.GetAll().Count);
        }

        [TestMethod]
        public void AddPublicPaste()
        {
            clearDB();
            db.Add(new Paste { Title = "test", Description = "test", Content = "test\ntest", Expieres = DateTime.MaxValue, IsHidden = false, AuthorID = null });
            var res = db.GetAll();
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("test", res[0].Title);
            Assert.AreEqual("test", res[0].Description);
            Assert.AreEqual("test\ntest", res[0].Content);
            Assert.AreEqual(DateTime.MaxValue, res[0].Expieres);
            Assert.IsNull(res[0].AuthorID);
        }
        [TestMethod]
        public void AddPrivatePaste()
        {
            clearDB();
            db.Add(new Paste { Title = "test", Description = "test", Content = "test\ntest", Expieres = DateTime.MaxValue, IsHidden = true, AuthorID = null });
            var res = db.GetAll();
            Assert.AreEqual(0, res.Count);
        }
    }
}
