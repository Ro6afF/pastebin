using System;
using System.Collections.Generic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests.Comments
{
    [TestClass]
    public class AddTests : CommentTestClass
    {
        [TestMethod]
        public void NormalAdd()
        {
            getDbContext(new List<Comment> { });
            db.Add(new Comment { Author = "ivancho", Content = "content", Id = 1, PasteId = 1 });

            mockSet.Verify(m => m.Add(It.IsAny<Comment>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
