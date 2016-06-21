using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing.Imaging;
using System.IO;
using TestJob.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TestJob
{
    [Serializable]
    public enum Browsers
    {

        [Description("Mozilla Firefox")]
        Firefox,

        [Description("Google Chrome")]
        Chrome
    }

    public static class Browser
    {
        #region Properties

        public static Browsers SelectedBrowser
        {
            get { return Settings.Default.Browser; }
        }

        public static Uri Url
        {
            get { WaitAjax(); return new Uri(WebDriver.Url); }
        }
        
        #endregion

        #region Private

        private static IWebDriver _webDriver;
        private static string _mainWindowHandler;

        private static IWebDriver WebDriver
        {
            get { return _webDriver ?? DriverInstance(); }
        }

        private static IWebDriver DriverInstance()
        {

            Contract.Ensures(Contract.Result<IWebDriver>() != null);

            if (_webDriver != null) return _webDriver;

            switch (SelectedBrowser)
            {
                case Browsers.Firefox:
                    _webDriver = StartFirefox();
                    break;
                case Browsers.Chrome:
                    _webDriver = StartChrome();
                    break;
                default:
                    throw new Exception(string.Format("Unknown browser: {0}.", SelectedBrowser));
            }

            return WebDriver;
        }
        
        private static FirefoxDriver StartFirefox()
        {
            var firefoxProfile = new FirefoxProfile
            {
                AcceptUntrustedCertificates = true,
                EnableNativeEvents = true
            };

            return new FirefoxDriver(firefoxProfile);
        }

        private static ChromeDriver StartChrome()
        {
            var chromeOptions = new ChromeOptions();
            var defaultDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\..\Local\Google\Chrome\User Data\Default";
            
            return new ChromeDriver(chromeOptions);
        }
        #endregion

        #region Public

        public static void Start()
        {
            _webDriver = DriverInstance();
            _webDriver.Manage().Window.Maximize();
            _mainWindowHandler = _webDriver.CurrentWindowHandle;
        }

        public static void Quit()
        {
            if (_webDriver == null) return;

            _webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.Quit();
            _webDriver = null;
        }

        public static void Navigate(Uri url)
        {
            Contract.Requires(url != null);

            WebDriver.Navigate().GoToUrl(url);
        }

        public static RemoteWebDriver getDriver()
        {
            return (RemoteWebDriver)_webDriver;
        }

        public static void WaitPageLoad()
        {
            Contract.Assume(WebDriver != null);

            var ready = new Func<bool>(() => (bool)ExecuteJavaScript("return document.readyState == 'complete'"));
           
            Contract.Assert(Wait.SpinWait(ready, TimeSpan.FromSeconds(60), TimeSpan.FromMilliseconds(100)));

        }

        public static void WaitAjax()
        {
            Contract.Assume(WebDriver != null);

            var ready = new Func<bool>(() => (bool)ExecuteJavaScript("return (typeof($) === 'undefined') ? true : !$.active;"));

            Contract.Assert(Wait.SpinWait(ready, TimeSpan.FromSeconds(60), TimeSpan.FromMilliseconds(100)));
        }

        public static void SwitchToFrame(IWebElement inlineFrame)
        {
            WebDriver.SwitchTo().Frame(inlineFrame);
        }

        public static void AcceptAlert()
        {
            var accept = Wait.MakeTry(() => WebDriver.SwitchTo().Alert().Accept());

            Wait.SpinWait(accept, TimeSpan.FromSeconds(5));
        }

        public static IEnumerable<IWebElement> FindElements(By selector)
        {
            Contract.Assume(WebDriver != null);

            return WebDriver.FindElements(selector);
        }

        public static Screenshot GetScreenshot()
        {
            WaitPageLoad();

            return ((ITakesScreenshot)WebDriver).GetScreenshot();
        }

        public static void SaveScreenshot(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));

            GetScreenshot().SaveAsFile(path, ImageFormat.Jpeg);
        }

        public static object ExecuteJavaScript(string javaScript, params object[] args)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;

            return javaScriptExecutor.ExecuteScript(javaScript, args);
        }

        public static void NavigateBack()
        {
            WebDriver.Navigate().Back();
        }

        public static void Refresh()
        {
            WebDriver.Navigate().Refresh();

        }

        #endregion
    }
}
