# CSharp Dotnet Appium Nunit MomentumSuite

[NUnit](https://nunit.org/) Integration with local or Momentum Suite real mobile farm devices


## Supports
  * Selenium v4 and Appium 2.0 (W3C)
  * Native or Hybrid Android and iOS apps (APK, AAB, IPA)
  * Local testing or using Momentum Suite's 150+ Android or iOS devices
  * [Allure](https://docs.qameta.io/allure/) test report integration
  
  ## Setup

**Requirements:**

* .NET Core SDK 3.1  If you don't have it installed, download it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/3.1).
* Install the [Allure command-line tool](https://www.npmjs.com/package/allure-commandline) (required to process the results directory after test run)

**Install the dependencies:**

Run the following command in project's base directory :
```
cd examples
dotnet build
```

## Getting Started
Getting Started with Appium-CSharp tests on Momentum Suite couldn't be easier!
With a Momentum Suite account, You need 4 things to start without any Appium or Android SDK dependencies.
  * **momentum:user** Usually it could be your email address
  * **momentum:token** Your unique access token learned from momentumsuite.com
  * **momentum:gw** Comma seperated Momentum Suite mobile device ID list (4 digit number) to run the test. First number will be your default phone for all except parallel-testing.
  * **appium:app** Your uploaded IPA, APK or AAB app file from Momentum Suite Application Library. Example format is ms://<hashed-app-id> Optionally you can use a public accessible web URL.
 
 Do not forget to set these 4 Appium capability values and check hostname, port, path and protocol values on your **testSettings.json** file.

**Start with Android device:**
 Open for editing your test_settings.json file under [root directory](https://github.com/momentumsuite/csharp-dotnet-appium-nunit-momentumsuite/blob/main/test_settings.json).
 
 Set momentumUser, momentumToken, momentumDeviceList, momentumApp on test_settings.json file.
 
 Test script is available in examples directory
 
 Run the following command in project's base directory :
```
 cd examples
 dotnet test --filter AndroidFirstTest
```


**Start with iOS device:**
Same with Android, but need to change testSettings.json file.
 
Run the following command in project's base directory :
```
 cd examples
 dotnet test --filter IOSFirstTest
```

**Start with local testing:**
Use Local testing that access resources hosted in your development or testing environments. You need to install Appium and it's all dependencies like Android SDK, Xcode, Command Line tools. At the same sime you will need to run a real device or simulator/emulator.  Do not forget to check hostname, port, path and protocol values on your testSettings.json file with your own Appium server.
 
Run the following command in project's base directory :
```
 cd examples
 dotnet test --filter AndroidLocalTest
```
 
 **All available commands to start mobile testing:**
 ```
dotnet test --filter AndroidLocalTest
dotnet test --filter AndroidFirstTest
dotnet test --filter IOSLocalTest
dotnet test --filter IOSFirstTest
```

### Allure Reporting
 
 Run the following command in project's base directory after test run has been completed. This command will open a browser window with HTML test results.
```
cd examples
allure serve bin/Debug/netcoreapp3.1/allure-results
```

## Getting Help
If you are running into any issues or have any queries, please check [Momentum Suite Contact page](https://www.momentumsuite.com/contact/) or get in touch with us.
 
Our Technical Documentation space is [here](https://www.momentumsuite.com/docs/).
