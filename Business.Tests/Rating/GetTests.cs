using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests.Ratings
{
    [TestClass]
    public class GetTests : RatingTestClass
    {
        [TestMethod]
        public void Empty()
        {
            getDbContext(new List<Rating> { }, new List<Paste> { });
            Assert.AreEqual(0, db.Get(1));
        }

        [TestMethod]
        public void TestEmpty2()
        {
            getDbContext(new List<Rating> {
                new Rating { Author = "pesho", Rate = 5, PasteId = 1 },
                new Rating { Author = "ivan", Rate = 4, PasteId = 1 },
            }, new List<Paste> { });
            Assert.AreEqual(0, db.Get(2));
        }

        [TestMethod]
        public void TestRating()
        {
            getDbContext(new List<Rating> {
                new Rating { Author = "pesho", Rate = 5, PasteId = 1 },
                new Rating { Author = "ivan", Rate = 4, PasteId = 1 },
            }, new List<Paste> { });
            Assert.AreEqual(4.5, db.Get(1));
        }

        [TestMethod]
        public void TestRating2()
        {
            getDbContext(new List<Rating> {
                new Rating { Author = "pesho", Rate = 5, PasteId = 1 },
                new Rating { Author = "ivan", Rate = 4, PasteId = 1 },
                new Rating { Author = "pesho", Rate = 3, PasteId = 2 },
                new Rating { Author = "ivan", Rate = 4, PasteId = 2 },
            }, new List<Paste> { });
            Assert.AreEqual(4.5, db.Get(1));
            Assert.AreEqual(3.5, db.Get(2));
        }
    }
}
