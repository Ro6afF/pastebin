using Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tests.Ratings
{ 
    /// <summary>
    /// A class which mocks the DB in order to test the business layer of the application
    /// </summary>
    public class RatingTestClass
    {
        public Mock<DbSet<Rating>> mockSet;
        public Mock<DbSet<Paste>> mockSetP;
        public RatingBusiness db;
        public Mock<DBContext> mockContext;

        /// <summary>
        /// Mocks the DB context that it contains the entries given as paramether.
        /// </summary>
        /// <param name="dataList">The list of all the entries that should be in the database</param>
        /// <param name="pasteList">The list of all the entries of pastes that should be in the database</param>
        /// <exception cref="NotImplementedException">Thrown then context.Entry function is called</exception>
        public void getDbContext(List<Rating> dataList, List<Paste> pasteList)
        {
            var data = dataList.AsQueryable();
            var dataP = pasteList.AsQueryable();

            mockSet = new Mock<DbSet<Rating>>();
            mockSetP = new Mock<DbSet<Paste>>();

            mockSet.As<IQueryable<Rating>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Rating>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Rating>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Rating>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockSetP.As<IQueryable<Paste>>().Setup(m => m.Provider).Returns(dataP.Provider);
            mockSetP.As<IQueryable<Paste>>().Setup(m => m.Expression).Returns(dataP.Expression);
            mockSetP.As<IQueryable<Paste>>().Setup(m => m.ElementType).Returns(dataP.ElementType);
            mockSetP.As<IQueryable<Paste>>().Setup(m => m.GetEnumerator()).Returns(dataP.GetEnumerator());

            mockContext = new Mock<DBContext>();
            mockContext.Setup(x => x.Ratings).Returns(mockSet.Object);
            mockContext.Setup(x => x.Pastes).Returns(mockSetP.Object);
            mockContext.Setup(x => x.Entry(It.IsAny<Rating>())).Throws(new NotImplementedException());

            db = new RatingBusiness(mockContext.Object);
        }
    }
}
