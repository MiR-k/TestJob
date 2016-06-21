using System;
using NUnit.Framework;
using TestJob.core.helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestJob.pages
{
    class SentMailPage : EmailPage
    {
        public SentMailPage()
        {
            string strTitle = Browser.getDriver().Title;
            Assert.True(strTitle.Contains("Отправленные"));
        }

        public EmailPage Check(string toMail, string subject, string message)
        {
            var lnkFirstMailSent = Browser.getDriver().FindElement(By.XPath(@"//div[@role='main'] //tbody //tr[1]//td[6]"));
            lnkFirstMailSent.Click();
            Browser.WaitPageLoad();

            var lnkToMail = Browser.getDriver().FindElement(By.XPath(@"//span[contains(text(),""кому"")]//span"));
            var lnkSubject = Browser.getDriver().FindElement(By.XPath("//table //h2"));
            var txtMessage = Browser.getDriver().FindElement(By.XPath("//div[@class='a3s'] /div[@dir='ltr']"));

            Assert.True(lnkToMail.GetAttribute("email").Equals(toMail), "Проверка email");
            Logger.Info("Email проверен");

            Assert.AreEqual(subject, lnkSubject.Text, "Проверка темы");
            Logger.Info("Тема проверена");

            Assert.AreEqual(message, txtMessage.Text, "Проверка сообщения");
            Logger.Info("Сообщение проверено");
            return new EmailPage();
        }
    }
}
