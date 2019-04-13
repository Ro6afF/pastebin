using System;
using System.Collections.Generic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests.Ratings
{
    [TestClass]
    public class RateTests : RatingTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotExistingAdd()
        {
            getDbContext(new List<Rating> { }, new List<Paste> { });
            db.Rate(1, 2, "pesho");
        }
        [TestMethod]
        public void NormalAdd()
        {
            getDbContext(new List<Rating> { }, new List<Paste> { new Paste { AuthorID = "", Content = "", Description = "", Expieres = DateTime.MaxValue, IsHidden = false, Title = "", Id = 1 } });
            db.Rate(1, 2, "pesho");
            mockSet.Verify(m => m.Add(It.IsAny<Rating>()), Times.Exactly(1));
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        }
        [TestMethod]
        public void NormalAdd2()
        {
            getDbContext(new List<Rating> { new Rating { Author = "pesho", Id = 1, PasteId = 1, Rate = 4 } }, new List<Paste> { new Paste { AuthorID = "", Content = "", Description = "", Expieres = DateTime.MaxValue, IsHidden = false, Title = "", Id = 1 } });
            db.Rate(1, 2, "ivan");
            mockSet.Verify(m => m.Add(It.IsAny<Rating>()), Times.Exactly(1));
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongValue()
        {
            getDbContext(new List<Rating> { }, new List<Paste> { new Paste { AuthorID = "", Content = "", Description = "", Expieres = DateTime.MaxValue, IsHidden = false, Title = "", Id = 1 } });
            db.Rate(1, 6, "pesho");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongValue2()
        {
            getDbContext(new List<Rating> { }, new List<Paste> { new Paste { AuthorID = "", Content = "", Description = "", Expieres = DateTime.MaxValue, IsHidden = false, Title = "", Id = 1 } });
            db.Rate(1, 0, "pesho");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotLogedIn()
        {
            getDbContext(new List<Rating> { }, new List<Paste> { new Paste { AuthorID = "", Content = "", Description = "", Expieres = DateTime.MaxValue, IsHidden = false, Title = "", Id = 1 } });
            db.Rate(1, 0, "");
        }

        [TestMethod]
        public void UpdateAdd()
        {
            getDbContext(new List<Rating> { new Rating { Author = "pesho", PasteId = 1, Rate = 2 } }, new List<Paste> { new Paste { AuthorID = "", Content = "", Description = "", Expieres = DateTime.MaxValue, IsHidden = false, Title = "", Id = 1 } });
            db.Rate(1, 2, "pesho");
            mockSet.Verify(m => m.Add(It.IsAny<Rating>()), Times.Exactly(1));
            mockSet.Verify(m => m.Remove(It.IsAny<Rating>()), Times.Exactly(1));
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        }
    }
}
