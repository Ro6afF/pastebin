using System;
using System.Collections.Generic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests
{
    [TestClass]
    public class AddTests : PasteTestClass
    {
        [TestMethod]
        public void NormalAdd()
        {
            getDbContext(new List<Paste> { });
            db.Add(new Paste { AuthorID = "ivancho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 });

            mockSet.Verify(m => m.Add(It.IsAny<Paste>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
