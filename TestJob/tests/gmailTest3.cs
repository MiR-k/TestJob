using System;
using NUnit.Framework;
using TestJob.core.helpers;
using TestJob.pages;

namespace TestJob
{
    public class gmailTest3 : BaseTest
    {
        #region TestData

        const string email = @"test.tatur2@gmail.com";
        const string password = @"02-034test";

        #endregion

        [Test]
        public void RunTest()
        {
            Logger.Step(1);
            LoginPage loginPage = new LoginPage();
            loginPage.Login(email, password);

            Logger.Step(2);
            EmailPage emailPage = new EmailPage();
            emailPage.DeleteAll();

            Logger.Step(3);
            emailPage.CheckNull();

            Logger.Step(4);
            MenuMailPage menuMail = new MenuMailPage();
            menuMail.Expand();
            menuMail.OpenFolder("Корзина");
            TrashMailPage trashMail = new TrashMailPage();
            trashMail.Resurrect();

            Logger.Step(5);
            menuMail.OpenFolder("Входящие");
            emailPage.NotNull();

            Logger.Step(6);
            emailPage.Loggout();

        }

    }
}
