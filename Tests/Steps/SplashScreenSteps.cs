using GeneralStore.Configs;
using GeneralStore.Pages.MainPage;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using System;
using TechTalk.SpecFlow;
 
namespace GeneralStore.Tests.Steps
{
    [Binding]
    public class SplashScreenSteps
    {
        private static AndroidDriver _driver;
        private static MainPage _splash;
 
        [BeforeTestRun]
        public static void InitApp()
        {
            var serverUrl = new Uri(
                Environment.GetEnvironmentVariable("APPIUM_SERVER_URL")
                ?? "http://localhost:4723/wd/hub");
 
            _driver = new AndroidDriver(
                serverUrl,
                AppiumConfig.BuildAndroidOptions(),
                TimeSpan.FromSeconds(180));
 
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _splash = new MainPage(_driver);
        }
 
        [AfterTestRun]
        public static void CloseApp()
        {
            try
            {
                _driver?.RemoveApp("com.androidsample.generalstore");
            }
            finally
            {
                _driver?.Quit();
                _driver?.Dispose();
            }
        }
 
        [When(@"application is launched")]
        public void WhenAppStarted()
        {
            Assert.That(_splash.IsDisplayed, Is.True, "App not started");
        }
 
        [Then(@"Splash screen is displayed")]
        public void ThenSplashDisplayed()
        {
            Assert.That(_splash.IsDisplayed, Is.True, "Splash not displayed");
        }
 
        [Then(@"Splash screen is enabled")]
        public void ThenSplashEnabled()
        {
            Assert.That(_splash.IsEnabled, Is.True, "Splash not Enabled");
        }
 
        [Then(@"On the Splash screen there is no text")]
        public void ThenNoTextOnSplash()
        {
            Assert.That(_splash.HasAnyText, Is.False, "На Splash есть текст");
        }
    }
}