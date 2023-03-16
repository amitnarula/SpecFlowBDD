using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace CalculatorSelenium.Specs.Drivers
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public BrowserDriver()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateChromeWebDriver());
        }

        /// <summary>
        /// The Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateChromeWebDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            //We use the Chrome browser
            //var chromeDriverService = ChromeDriverService.CreateDefaultService();

            var chromeOptions = new ChromeOptions();

            //var chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
            var chromeDriver = new ChromeDriver();

            return chromeDriver;
        }

        private IWebDriver CreateMSEdgeWebDriver()
        {
            //We use the Chrome browser
            var edgeDriverService = EdgeDriverService.CreateDefaultService(".", "msedgedriver.exe");

            var edgeOptions = new EdgeOptions();

            

            var edgeDriver = new EdgeDriver(edgeDriverService, edgeOptions);

            return edgeDriver;
        }

        /// <summary>
        /// Disposes the Selenium web driver (closing the browser)
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}