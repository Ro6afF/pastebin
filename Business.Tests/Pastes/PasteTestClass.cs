using Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tests.Pastes
{ 
    /// <summary>
    /// A class which mocks the DB in order to test the business layer of the application
    /// </summary>
    public class PasteTestClass
    {
        public Mock<DbSet<Paste>> mockSet;
        public PasteBusiness db;
        public Mock<DBContext> mockContext;

        /// <summary>
        /// Mocks the DB context that it contains the entries given as paramether.
        /// </summary>
        /// <param name="dataList">The list of all the entries that should be in the database</param>
        /// <exception cref="NotImplementedException">Thrown then context.Entry function is called</exception>
        public void getDbContext(List<Paste> dataList)
        {
            var data = dataList.AsQueryable();
            mockSet = new Mock<DbSet<Paste>>();

            mockSet.As<IQueryable<Paste>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Paste>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Paste>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Paste>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<DBContext>();
            mockContext.Setup(x => x.Pastes).Returns(mockSet.Object);
            mockContext.Setup(x => x.Entry(It.IsAny<Paste>())).Throws(new NotImplementedException());

            db = new PasteBusiness(mockContext.Object);
        }
    }
}
