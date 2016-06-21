using System;
using NUnit.Framework;
using TestJob.core.helpers;
using TestJob.pages;

namespace TestJob
{
    public class gmailTest : BaseTest
    {
        #region TestData

        const string email = @"test.tatur1@gmail.com";
        const string toEmail = @"test.tatur2@gmail.com";
        const string password = @"01-023test";
        const string subject = "hello";
        const string textMail = "привет";

        #endregion

        [Test]
        public void RunTest()
        {
            LoginPage lgnPage = new LoginPage();
            Logger.Step(1,2);
            lgnPage.Login(email, password);

            Logger.Step(3, 5);
            EmailPage emailPage = new EmailPage();
            emailPage.CreateMail(toEmail, subject, textMail);
            emailPage.Save();

            Logger.Step(6);
            MenuMailPage menuMail = new MenuMailPage();
            menuMail.OpenFolder("Черновики");

            Logger.Step(7);
            DraftMailPage draftMail = new DraftMailPage();
            draftMail.Check(toEmail, subject, textMail);

            Logger.Step(8);
            emailPage.Send();
                       
            Logger.Step(9);
            menuMail.OpenFolder("Черновики");
            emailPage.CheckMail("Черновик");
               
            Logger.Step(10,11);
            menuMail.OpenFolder("Отправленные");
            SentMailPage sentMail = new SentMailPage();
            sentMail.Check(toEmail, subject, textMail)
                .Loggout();

        }

    }
}
