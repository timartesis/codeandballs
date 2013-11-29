using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTesting
{
    public class UnitTest1
    {
        [Test]
        public void TestMultiplication()
        {
            Assert.AreEqual(4, 2 * 2, "Multiplication");
            Assert.That(2 * 2, Is.EqualTo(4), "Multiplication constraint-based");
        }
    }
}
