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
using System.Collections;
using System.Collections.Generic;

namespace Appium
{
    [AllureNUnit]
    [TestFixture]

    public class IOSFirstTest
    {
        private AppiumDriver<AppiumWebElement> driver;

        [SetUp]
        public void Setup()
        {
            var myJsonString = File.ReadAllText("test_settings.json"); 
            var myJObject = JObject.Parse(myJsonString);
        
            
            string momentumUser = myJObject.SelectToken("CLOUD.momentumUser").Value<string>();
            string momentumToken = myJObject.SelectToken("CLOUD.momentumToken").Value<string>();
            string momentumHost = myJObject.SelectToken("CLOUD.momentumHost").Value<string>();
            string momentumApp = myJObject.SelectToken("CLOUD.ios.momentumApp").Value<string>();
            JArray momentumDeviceList = (JArray)myJObject["CLOUD"]["ios"]["momentumDeviceList"];
            JToken momentumDeviceId = momentumDeviceList[0];

            int remoteDebugProxy_ = momentumDeviceId.Value<Int32>(); 

            var remoteDebugProxy =(remoteDebugProxy_ + 2000).ToString(); 

            AppiumOptions caps = new AppiumOptions();
            Dictionary<string, object> momentumOptions = new Dictionary<string, object>();
            caps.AddAdditionalCapability("appium:automationName", "XCUITest");
            caps.AddAdditionalCapability("appium:autoAcceptAlerts", true);
            caps.AddAdditionalCapability("appium:language", "en");
            caps.AddAdditionalCapability("appium:locale", "en");
            caps.AddAdditionalCapability("appium:deviceName", "");
            caps.AddAdditionalCapability("appium:udid", "");
            caps.AddAdditionalCapability("appium:app", momentumApp);
            caps.AddAdditionalCapability("appium:fullReset", true);
            caps.AddAdditionalCapability("appium:noReset", false);
            caps.AddAdditionalCapability("appium:remoteDebugProxy", remoteDebugProxy);
            momentumOptions.Add("user",momentumUser);
            momentumOptions.Add("token", momentumToken);
            momentumOptions.Add("gw", momentumDeviceId);
            caps.AddAdditionalCapability("momentum:options", momentumOptions);
            driver = new IOSDriver<AppiumWebElement>(new Uri(momentumHost), caps);
            Console.WriteLine("iOS First Test");
        }

        [Test]
        public void TestFirstIOS()
        {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(e => e.FindElement(By.XPath("(//*[contains(@label , '2') or contains(@name , '2') or contains(@value , '2')])[1]"))).Click();
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
