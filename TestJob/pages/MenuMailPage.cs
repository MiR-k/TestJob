using System;
using OpenQA.Selenium;
using TestJob.core.helpers;

namespace TestJob.pages
{
    public class MenuMailPage
    {
        public void OpenFolder(string nameFolder)
        {
            var lnkFolder = Browser.getDriver().FindElement(By.XPath(@"//a[contains(@title,""" + nameFolder + @""")]"));
            lnkFolder.Click();
            Browser.AcceptAlert();
            Logger.Info("Открыта " + nameFolder);
        }

        public void Expand()
        {
            var lnkExand = Browser.getDriver().FindElement(By.XPath(@"//div[@role='navigation']//span[contains(text(),""Ещё"")]"));
            if (lnkExand.Displayed)
            {
                lnkExand.Click();
            }
        }
    }
}
