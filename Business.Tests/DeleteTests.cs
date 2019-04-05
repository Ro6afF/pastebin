using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Data;

namespace Business.Tests
{
    [TestClass]
    public class DeleteTests : PasteTestClass
    {
        [TestMethod]
        public void NormalDelete()
        {
            Paste p = new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            db.Delete(0, "pesho");
            mockSet.Verify(m => m.Remove(It.IsAny<Paste>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OtherUserDelete()
        {
            Paste p = new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            db.Delete(0, "gosho");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotSingedInUpdate()
        {
            Paste p = new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            db.Delete(0, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousNotSingedInUpdate()
        {
            Paste p = new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            db.Delete(0, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousSingedInUpdate()
        {
            Paste p = new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            db.Delete(0, null);
        }
    }
}
