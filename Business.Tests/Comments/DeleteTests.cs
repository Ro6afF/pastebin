using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Data;

namespace Business.Tests.Comments
{
    [TestClass]
    public class DeleteTests : CommentTestClass
    {
        [TestMethod]
        public void NormalDelete()
        {
            Comment p = new Comment { Author = "pesho", Content = "content", Id = 1, PasteId = 1 };
            getDbContext(new List<Comment> { p });
            db.Delete(1, "pesho");
            mockSet.Verify(m => m.Remove(It.IsAny<Comment>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OtherUserDelete()
        {
            Comment p = new Comment { Author = "pesho", Content = "content", Id = 1, PasteId = 1 };
            getDbContext(new List<Comment> { p });
            db.Delete(1, "gosho");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotSingedInUpdate()
        {
            Comment p = new Comment { Author = "pesho", Content = "content", Id = 1, PasteId = 1 };
            getDbContext(new List<Comment> { p });
            db.Delete(1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousNotSingedInUpdate()
        {
            Comment p = new Comment { Author = null, Content = "content", Id = 1, PasteId = 1 };
            getDbContext(new List<Comment> { p });
            db.Delete(1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousSingedInUpdate()
        {
            Comment p = new Comment { Author = null, Content = "content", Id = 1, PasteId = 1 };
            getDbContext(new List<Comment> { p });
            db.Delete(1, "pesho");
        }
    }
}
