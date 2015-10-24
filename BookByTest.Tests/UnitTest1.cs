using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookByText;

namespace BookByTest.Tests
{
    [TestClass]
    public class BookServiceTests
    {
        private IBookService service;

        [TestMethod]
        public void TestMethod1()
        {
            service = new BookService();
        }
    }
}
