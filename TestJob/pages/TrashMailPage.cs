using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TestJob.core.helpers;

namespace TestJob.pages
{
    class TrashMailPage : EmailPage
    {
        public TrashMailPage()
        {
            string strTitle = Browser.getDriver().Title;
            Assert.True(strTitle.Contains("Корзина"));
        }

        public void Clean()
        {
            var chkAllItem = Browser.getDriver().FindElement(By.XPath("//div[@gh='tm'] //span[@role='checkbox']"));
            chkAllItem.Click();

            var lnkClean = Browser.getDriver().FindElement(By.XPath(@"//div[contains(text(),""Удалить навсегда"")]"));
            lnkClean.Click();
            Logger.Info("Удалены все письма");

            CheckNull();
            Logger.Info("Папка корзина пуста");
        }

        public void Resurrect()
        {
            var chkAllItem = Browser.getDriver().FindElement(By.XPath("//div[@gh='tm'] //span[@role='checkbox']"));
            chkAllItem.Click();
            Browser.WaitPageLoad();

            var btnGroupFolder = Browser.getDriver().FindElement(By.XPath(@"//div[@gh='tm']//div[@aria-label='Переместить в']"));
            btnGroupFolder.Click();
            Browser.WaitPageLoad();

            var fldInbox = Browser.getDriver().FindElement(By.XPath(@"//div[@role='menuitem']/div[contains(text(),""Входящие"")]"));
            fldInbox.Click();
        }
    }
}
