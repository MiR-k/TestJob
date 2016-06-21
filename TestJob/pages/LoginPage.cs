using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using TestJob.core.helpers;

namespace TestJob.pages
{
    public class LoginPage
    {
        public void Login(string emailUser,string password)
        {
            TypeUserSubmit(emailUser);
            PasswordSubmit(password);
            Browser.WaitPageLoad();
            CheckMail(emailUser);
        }
        
        public void TypeUserSubmit(string email)
        {
            var txtEmail = Browser.getDriver().FindElement(By.Id("Email"));
            var btnNext = Browser.getDriver().FindElement(By.Id("next"));
            txtEmail.SendKeys(email);
            btnNext.Click();
        }

        public void PasswordSubmit(string password)
        {
            Browser.WaitPageLoad();
            var btnSubmit = Browser.getDriver().FindElement(By.Id("signIn"));
            var txtPassword = Browser.getDriver().FindElement(By.Id("Passwd"));
            txtPassword.SendKeys(password);
            btnSubmit.Click();
        }

        private void CheckMail(string mail)
        {
            Browser.WaitPageLoad();
            var btnProfile = Browser.getDriver().FindElement(By.XPath(@"//a[contains(@title,""Аккаунт Google"")]"));
            btnProfile.Click();
            Browser.WaitPageLoad();
            var txtUser = Browser.getDriver().FindElement(By.XPath(@"//a[contains(text(),""Мой аккаунт"")]/.. /div[2]"));
            Assert.AreEqual(mail,txtUser.Text, "Проверка логина");
            Logger.Info("Логин успешен");
        }
    }
}
