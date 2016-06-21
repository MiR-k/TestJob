using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TestJob.Properties;

namespace TestJob
{
    [TestFixture]
    public abstract class BaseTest
    {
        public TestContext TestContext { get; set; }

        [SetUp]
        public void TestInitialize()
        {
            Browser.Start();
            Browser.getDriver().Navigate().GoToUrl(Settings.Default.StartPage);
        }

        [TearDown]
        public void TestExit()
        {
            Browser.Quit();
        }
    }
}
