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
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Appium
{
    [AllureNUnit]
    [TestFixture]

    public class IOSLocalTest
    {
        private AppiumDriver<AppiumWebElement> driver;

        [SetUp]
        public void Setup()
        {
            var myJsonString = File.ReadAllText("test_settings.json"); 
            var myJObject = JObject.Parse(myJsonString);
            var appiumHost = myJObject.SelectToken("$.LOCAL.host").Value<string>();
            var iosApp = myJObject.SelectToken("$.LOCAL.ios.app").Value<string>();
            var iosDevice = myJObject.SelectToken("$.LOCAL.ios.deviceName").Value<string>();

            AppiumOptions caps = new AppiumOptions();
            caps.AddAdditionalCapability("appium:automationName", "XCUITest");
            caps.AddAdditionalCapability("appium:deviceName", iosDevice);
            caps.AddAdditionalCapability("appium:app", iosApp);
            caps.AddAdditionalCapability("appium:autoAcceptAlerts", true);
            caps.AddAdditionalCapability("appium:language", "en");
            caps.AddAdditionalCapability("appium:locale", "en");
            caps.AddAdditionalCapability("appium:fullReset", true);
            caps.AddAdditionalCapability("appium:noReset", false);
            driver = new IOSDriver<AppiumWebElement>(new Uri(appiumHost), caps);
            Console.WriteLine("iOS Local Test");
        }

        [Test]
        public void TestLocalIOS()
        {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(e => e.FindElement(By.XPath("(//*[contains(@label , '2')])[1]"))).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//XCUIElementTypeButton[@name='+']")).Click();
                driver.FindElement(By.XPath("//XCUIElementTypeButton[@name='5']")).Click();
                driver.FindElement(By.XPath("//XCUIElementTypeButton[@name='=']")).Click();
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
