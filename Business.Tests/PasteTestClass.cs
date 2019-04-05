﻿using Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tests
{
    public class PasteTestClass
    {
        public Mock<DbSet<Paste>> mockSet;
        public PasteBusiness db;
        public Mock<PasteContext> mockContext;
        public void getDbContext(List<Paste> dataList)
        {
            var data = dataList.AsQueryable();
            mockSet = new Mock<DbSet<Paste>>();

            mockSet.As<IQueryable<Paste>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Paste>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Paste>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Paste>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<PasteContext>();
            mockContext.Setup(x => x.Pastes).Returns(mockSet.Object);

            db = new PasteBusiness(mockContext.Object);
        }
    }
}