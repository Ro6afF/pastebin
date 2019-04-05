using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Collections.Generic;
using Moq;

namespace Business.Tests
{
    [TestClass]
    public class UpdateTests : PasteTestClass
    {
        

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotLoggedInUpdate()
        {
            Paste p = new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            p.Content = "updatedContent";
            db.Update(p, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OtherUserUpdate()
        {
            Paste p = new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { new Paste { AuthorID = "pesho", Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 } });
            p.Content = "updatedContent";
            db.Update(p, "gosho");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousNotSignedInUpdate()
        {
            Paste p = new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            p.Content = "updatedContent";
            db.Update(p, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousSignedInUpdate()
        {
            Paste p = new Paste { AuthorID = null, Content = "content", Description = "description", Expieres = DateTime.MaxValue, IsHidden = false, Title = "title", Id = 0 };
            getDbContext(new List<Paste> { p });
            p.Content = "updatedContent";
            db.Update(p, "pesho");
        }
    }
}
