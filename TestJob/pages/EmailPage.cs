using System;
using OpenQA.Selenium;
using NUnit.Framework;
using TestJob.core.helpers;

namespace TestJob.pages
{
    class EmailPage
    {
        public void CreateMail(string toEmail, string subjectMessage, string message)
        {
            var btnNewMail = Browser.getDriver().FindElement(By.XPath(@"//div[contains(text(),""НАПИСАТЬ"")]/.."));

            btnNewMail.Click();
            Browser.WaitPageLoad();

            var txtToMail = Browser.getDriver().FindElement(By.XPath("//textarea[@aria-label='Кому']"));
            var txtSubject = Browser.getDriver().FindElement(By.Name("subjectbox"));
            var txtMessage = Browser.getDriver().FindElement(By.XPath("//div[@role='textbox']"));

            txtToMail.SendKeys(toEmail);
            txtSubject.SendKeys(subjectMessage);
            txtMessage.SendKeys(message);
        }

        public void Send()
        {
            var btnSend = Browser.getDriver().FindElement(By.XPath(@"//div[contains(text(),""Отправить"")]/.."));
            btnSend.Click();
            Browser.WaitPageLoad();
            Logger.Info("Письмо отправлено");
        }

        public void Save()
        {
            var btnCloseMail = Browser.getDriver().FindElement(By.XPath("//img[@alt='Закрыть']"));
            btnCloseMail.Click();
        }


        public void CheckMail(string nameMail)
        {
            string strMessage = nameMail + "всего - ";
            var num = Browser.getDriver().FindElements(By.XPath(@"//font[contains(text(),"""+ nameMail + @""")]/../.."));

            Assert.AreEqual(0, num.Count, "Количество " + nameMail +"-"+ num.Count);
            Logger.Info(String.Concat(strMessage, Convert.ToString(num.Count)));
        }

        public void CheckNull()
        {
            var num = Browser.getDriver().FindElements(By.XPath(@"//div[@role='main'] //div[@class='Cp'] //tr"));
            Logger.Info(Convert.ToString(num.Count));
            Assert.AreEqual(0, num.Count / 2, "Количество писем: " + num.Count);
        }

        public void Loggout()
        {
            var btnProfile = Browser.getDriver().FindElement(By.XPath(@"//a[contains(@title,""Аккаунт Google"")]"));
            btnProfile.Click();
            Browser.WaitPageLoad();

            var btnLoggout = Browser.getDriver().FindElement(By.XPath(@"//a[contains(text(),""Выйти"")]"));
            btnLoggout.Click();
            Browser.AcceptAlert();
        }

        public void DeleteAll()
        {
            var chkAllItem = Browser.getDriver().FindElement(By.XPath("//div[@gh='tm'] //span[@role='checkbox']"));
            chkAllItem.Click();

            var btnDelete = Browser.getDriver().FindElement(By.XPath("//div[@gh='tm'] //div[@aria-label='Удалить']"));
            btnDelete.Click();

            var btnOK = Browser.getDriver().FindElement(By.XPath("//div[@role='alertdialog'] //button[@name='ok']"));
            if (btnOK.Displayed)
            {
                btnOK.Click();
            }

            CheckNull();

            string nameFolder = Browser.getDriver().Title;
            nameFolder = nameFolder.Substring(0, nameFolder.IndexOf(" "));
            Logger.Info("Папка " + nameFolder + " пуста");
        }

        public void NotNull()
        {
            var num = Browser.getDriver().FindElements(By.XPath(@"//div[@role='main'] //div[@class='Cp'] //tr"));
            
            Assert.AreNotEqual(0, num.Count / 2, "Количество писем: " + num.Count);
            Logger.Info("Количество писем: "+Convert.ToString(num.Count));
        }

    }
}
