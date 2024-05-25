using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Недвижимость;

namespace UnitTest
{
    [TestClass]
    public class DB
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange and Act
            Class1 db = new Class1();
            db.openConnection();
            // Assert
            Assert.IsTrue(db.sqlConnection.State == System.Data.ConnectionState.Open);
            db.closeConnection();
        }
    }
}
