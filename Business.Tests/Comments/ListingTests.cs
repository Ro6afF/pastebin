using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests.Comments
{
    [TestClass]
    public class ListingTests : CommentTestClass
    {
        [TestMethod]
        public void Empty()
        {
            getDbContext(new List<Comment> { });
            Assert.AreEqual(0, db.GetAll(1).Count);
        }

        [TestMethod]
        public void TestListingCount()
        {
            getDbContext(new List<Comment> {
                new Comment { Author = null, Content = "content", PasteId = 1 },
                new Comment { Author = null, Content = "content", PasteId = 1 },
            });
            Assert.AreEqual(2, db.GetAll(1).Count);
        }

        [TestMethod]
        public void TestListingCount2()
        {
            getDbContext(new List<Comment> {
                new Comment { Author = null, Content = "content", PasteId = 1 },
                new Comment { Author = null, Content = "content", PasteId = 2 },
                new Comment { Author = null, Content = "content", PasteId = 1 },
                new Comment { Author = null, Content = "content", PasteId = 2 },
                new Comment { Author = null, Content = "content", PasteId = 1 }
            });
            Assert.AreEqual(2, db.GetAll(2).Count);
        }

        [TestMethod]
        public void TestListingCount3()
        {
            getDbContext(new List<Comment> {
                new Comment { Author = null, Content = "content", PasteId = 1 },
                new Comment { Author = null, Content = "content", PasteId = 1 },
            });
            Assert.AreEqual(0, db.GetAll(2).Count);
        }

        [TestMethod]
        public void TestValues()
        {
            getDbContext(new List<Comment> {
                new Comment { Author = "ivan", Content = "content", PasteId = 1 },
            });
            var res = db.GetAll(1);
            Assert.AreEqual("content", res[0].Content);
            Assert.AreEqual(1, res[0].PasteId);
            Assert.AreEqual("ivan", res[0].Author);
        }
    }
}
