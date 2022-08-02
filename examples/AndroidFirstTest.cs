using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Appium
{
    [TestFixture]

    public class AndroidFirstTest
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
            string momentumApp = myJObject.SelectToken("CLOUD.android.momentumApp").Value<string>();
            JArray momentumDeviceList = (JArray)myJObject["CLOUD"]["android"]["momentumDeviceList"];
            JToken momentumDeviceId = momentumDeviceList[0];

            AppiumOptions caps = new AppiumOptions();
            Dictionary<string, object> momentumOptions = new Dictionary<string, object>();
            caps.AddAdditionalCapability("plarformName", "Android");
            caps.AddAdditionalCapability("appium:automationName", "UiAutomator2");
            caps.AddAdditionalCapability("appium:autoGrantPermissions", true);
            caps.AddAdditionalCapability("appium:language", "en");
            caps.AddAdditionalCapability("appium:locale", "en");
            caps.AddAdditionalCapability("appium:deviceName", "");
            caps.AddAdditionalCapability("appium:udid", "");
            caps.AddAdditionalCapability("appium:app", momentumApp);
            caps.AddAdditionalCapability("appium:fullReset", true);
            caps.AddAdditionalCapability("appium:noReset", false);
            momentumOptions.Add("user",momentumUser);
            momentumOptions.Add("token", momentumToken);
            momentumOptions.Add("gw", momentumDeviceId);
            caps.AddAdditionalCapability("momentum:options", momentumOptions);
            //Console.WriteLine(caps);
            driver = new AndroidDriver<AppiumWebElement>(new Uri(momentumHost), caps);
            Console.WriteLine("Android First Test");
        }

        [Test]
        public void TestFirstAndroidLogin()
        {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(e => e.FindElement(By.Id("username_txt"))).SendKeys("first@mobven.com");
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
