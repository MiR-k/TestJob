using System;
using NUnit.Framework;
using TestJob.core.helpers;
using TestJob.pages;

namespace TestJob
{
    public class gmailTest2 : BaseTest
    {
        #region TestData

        const string email = @"test.tatur1@gmail.com";
        const string password = @"01-023test";

        #endregion

        [Test]
        public void RunTest()
        {
            Logger.Step(1);
            LoginPage loginPage = new LoginPage();
            loginPage.Login(email, password);

            Logger.Step(2);
            MenuMailPage menuMail = new MenuMailPage();
            menuMail.OpenFolder("Отправленные");

            Logger.Step(3);
            SentMailPage sentMail = new SentMailPage();
            sentMail.DeleteAll();

            Logger.Step(4);
            menuMail.Expand();
            menuMail.OpenFolder("Корзина");

            Logger.Step(5);
            TrashMailPage trashMail = new TrashMailPage();
            trashMail.Clean();

            Logger.Step(6);
            EmailPage emailPage = new EmailPage();
            emailPage.Loggout();

        }

    }
}
