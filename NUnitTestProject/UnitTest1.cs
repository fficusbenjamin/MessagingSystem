using NUnit.Framework;
using BusinessLayer;
using DB;
using System;

namespace NUnitTestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HeaderTest()
        {
            MessageFactory factory = null;
            factory = new EmailFactory("E123456789");

            Message message = factory.GetMessageType();
            Assert.Throws<Exception>(() => message.Sender = "");
        }
    }
}