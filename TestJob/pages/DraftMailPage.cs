using System;
using NUnit.Framework;
using TestJob.core.helpers;
using OpenQA.Selenium;

namespace TestJob.pages
{
    class DraftMailPage : EmailPage
    {
        public DraftMailPage()
        {
            string strTitle = Browser.getDriver().Title;
            Assert.True(strTitle.Contains("Черновики"));
        }

        public void Check(string toEmail, string subjectMessage, string message)
        {
            var lnkDraftFirst = Browser.getDriver().FindElement(By.XPath(@"//colgroup /following-sibling::tbody/tr[1] //font/../.."));
            lnkDraftFirst.Click();
            Browser.WaitPageLoad();

            var txtToMail = Browser.getDriver().FindElement(By.XPath(@"//textarea[@aria-label='Кому']/.. //div/input"));
            var txtSubject = Browser.getDriver().FindElement(By.XPath("//h2//div[2]"));
            var txtMessage = Browser.getDriver().FindElement(By.XPath("//div[@role='textbox']"));

            Assert.True(txtToMail.GetAttribute("value").Equals(toEmail), "Проверка email");
            Logger.Info("Успешна проверка: " + txtToMail.GetAttribute("value"));

            Assert.AreEqual(subjectMessage, txtSubject.Text, "Проверка темы");
            Logger.Info("Успешна проверка: " + txtSubject.Text);

            Assert.AreEqual(message, txtMessage.Text, "Проверка сообщения");
            Logger.Info("Сообщение проверено: " + txtMessage.Text);
        }
    }
}
