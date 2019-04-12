using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests.Pastes
{
    [TestClass]
    public class ListingTests : PasteTestClass
    {
        [TestMethod]
        public void Empty()
        {
            getDbContext(new List<Paste> { });
            Assert.AreEqual(0, db.GetAll().Count);
        }

        [TestMethod]
        public void TestListingCount()
        {
            getDbContext(new List<Paste> {
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title"},
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = true, Title = "title" },
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = false, Title = "title" },
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = true, Title = "title" },
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title"}
            });
            Assert.AreEqual(2, db.GetAll().Count);
        }

        [TestMethod]
        public void TestListingValues()
        {
            getDbContext(new List<Paste> {
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title"},
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = true, Title = "title" },
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = false, Title = "title" },
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = true, Title = "title" },
            });
            var res = db.GetAll();
            Assert.AreEqual("content", res[0].Content);
            Assert.AreEqual("description", res[0].Description);
            Assert.AreEqual("title", res[0].Title);
        }

        [TestMethod]
        public void TestListByAuthor()
        {
            getDbContext(new List<Paste> {
                new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title"},
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = true, Title = "title" },
                new Paste { AuthorID = "gosho", Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = false, Title = "title" },
                new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = true, Title = "title" },
            });
            var res = db.GetAllByAuthor("pesho");
            Assert.AreEqual(2, res.Count);
            res = db.GetAllByAuthor("gosho");
            Assert.AreEqual(1, res.Count);
            res = db.GetAllByAuthor(null);
            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void TestGetById()
        {
            getDbContext(new List<Paste> {
                new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0},
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = true, Title = "title", Id = 1 },
                new Paste { AuthorID = "gosho", Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = false, Title = "title", Id = 2 },
                new Paste { AuthorID = "pesho", Content = "content1", Description = "description1", Expieres = DateTime.MinValue, IsHidden = true, Title = "title1", Id = 3 },
            });
            var res = db.Get(0);
            Assert.AreEqual("content", res.Content);
            Assert.AreEqual("description", res.Description);
            Assert.AreEqual("title", res.Title);
            res = db.Get(3);
            Assert.AreEqual("content1", res.Content);
            Assert.AreEqual("description1", res.Description);
            Assert.AreEqual("title1", res.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetByIdNullParam()
        {
            getDbContext(new List<Paste> {
                new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0},
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = true, Title = "title", Id = 1 },
                new Paste { AuthorID = "gosho", Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = false, Title = "title", Id = 2 },
                new Paste { AuthorID = "pesho", Content = "content1", Description = "description1", Expieres = DateTime.MinValue, IsHidden = true, Title = "title1", Id = 3 },
            });
            var res = db.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestGetByIdWrongId()
        {
            getDbContext(new List<Paste> {
                new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0},
                new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = true, Title = "title", Id = 1 },
                new Paste { AuthorID = "gosho", Content = "content", Description = "description", Expieres = DateTime.MinValue, IsHidden = false, Title = "title", Id = 2 },
                new Paste { AuthorID = "pesho", Content = "content1", Description = "description1", Expieres = DateTime.MinValue, IsHidden = true, Title = "title1", Id = 3 },
            });
            var res = db.Get(123);
        }
    }
}
