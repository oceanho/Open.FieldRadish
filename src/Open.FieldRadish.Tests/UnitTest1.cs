using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FieldRadish;

namespace Open.FieldRadish.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ITable table = new DefaultTable<object>(null);
        }
    }
}
