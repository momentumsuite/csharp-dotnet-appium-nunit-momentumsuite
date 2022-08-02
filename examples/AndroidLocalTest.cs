using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using NUnit.Framework;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.Commons;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Appium
{
    [AllureNUnit]
    [TestFixture]

    public class AndroidLocalTest
    {
        private AppiumDriver<AppiumWebElement> driver;

        [SetUp]
        public void Setup()
        {
            var myJsonString = File.ReadAllText("test_settings.json"); 
            var myJObject = JObject.Parse(myJsonString);
            var appiumHost = myJObject.SelectToken("$.LOCAL.host").Value<string>();
            var androidApp = myJObject.SelectToken("$.LOCAL.android.app").Value<string>();
            var androidDevice = myJObject.SelectToken("$.LOCAL.android.deviceName").Value<string>();

            AppiumOptions caps = new AppiumOptions();
            caps.AddAdditionalCapability("appium:automationName", "UiAutomator2");
            caps.AddAdditionalCapability("appium:deviceName", androidDevice);
            caps.AddAdditionalCapability("appium:app", androidApp);
            caps.AddAdditionalCapability("appium:autoGrantPermissions", true);
            caps.AddAdditionalCapability("appium:language", "en");
            caps.AddAdditionalCapability("appium:locale", "en");
            caps.AddAdditionalCapability("appium:fullReset", true);
            caps.AddAdditionalCapability("appium:noReset", false);
            driver = new AndroidDriver<AppiumWebElement>(new Uri(appiumHost), caps);
            Console.WriteLine("Android Local Test");
        }

        [Test]
        public void TestLocalAndroidLogin()
        {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(e => e.FindElement(By.Id("username_txt"))).SendKeys("local@mobven.com");
                driver.FindElement(By.Id("password_txt")).SendKeys("Pass321*");
                driver.FindElement(By.Id("login_btn")).Click();
                Thread.Sleep(1000);
                wait.Until(e => e.FindElement(By.Id("account_layout")).Displayed);
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
