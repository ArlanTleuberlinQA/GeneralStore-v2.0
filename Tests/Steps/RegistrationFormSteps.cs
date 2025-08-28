using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using GeneralStore.Configs;
using GeneralStore.Pages.MainPage;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace GeneralStore.Tests.RegistrationFormTests
{
    [Binding]
    public class RegistrationTestsSteps
    {
        private static AndroidDriver _driver;
        private static MainPage _mainPage;
        private WebDriverWait _wait;

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
            _mainPage = new MainPage(_driver);
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

        [BeforeScenario]
        public void Setup()
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Step definitions for feature files

        [Given(@"the user is on the registration page")]
        public void GivenTheUserIsOnTheRegistrationPage()
        {
            Assert.That(_mainPage.ToolbarTitleDisplayed, Is.True, "Registration page is not displayed");
        }

        [When(@"the user views the country selection text")]
        public void WhenTheUserViewsTheCountrySelectionText()
        {
            // This step is for observation only, no action needed
        }

        [Then(@"the country selection text should be ""(.*)""")]
        public void ThenTheCountrySelectionTextShouldBe(string expectedText)
        {
            Assert.Multiple(() =>
            {
                Assert.That(_mainPage.SelectCountryTextDisplayed, Is.True, "Text не отображается");
                Assert.That(_mainPage.SelectCountryTextEnabled, Is.True, " не Enabled");
                Assert.That(_mainPage.SelectCountryTextClickable, Is.EqualTo("false"), "не кликабелен");
                Assert.That(_mainPage.SelectCountryText, Is.EqualTo(expectedText), "Text не соответствует ожидаемому");
            });
        }

        [When(@"the user selects country ""(.*)""")]
        public void WhenTheUserSelectsCountry(string countryName)
        {
            Assert.Multiple(() =>
            {
                Assert.That(_mainPage.SpinnerCountryDisplayed, Is.True, "Select country is not displayed.");
                Assert.That(_mainPage.SpinnerCountryEnabled, Is.True, "Select country is not enabled.");
                Assert.That(_mainPage.SpinnerCountryClickable, Is.EqualTo("true"), "Select country is not clickable.");
            });
            
            _mainPage.SelectCountry(countryName);
        }

        [Then(@"the selected country should be ""(.*)""")]
        public void ThenTheSelectedCountryShouldBe(string expectedCountry)
        {
            Assert.Multiple(() =>
            {
                Assert.That(_mainPage.SelectedCountryText, Is.EqualTo(expectedCountry));
                Assert.That(_mainPage.SelectedCountryDisplayed, Is.True, "Selected country is not displayed.");
                Assert.That(_mainPage.SelectedCountryEnabled, Is.True, "Selected country is not enabled.");
            });
        }

        [Then(@"the background image should be visible")]
        public void ThenTheBackgroundImageShouldBeVisible()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_mainPage.BackGroundImageDisplayed, Is.True, "Background image is not displayed.");
                Assert.That(_mainPage.BackGroundImageEnabled, Is.True, "Background image is not enabled.");
            });
        }

        [Then(@"the toolbar title should be ""(.*)""")]
        public void ThenTheToolbarTitleShouldBe(string expectedTitle)
        {
            Assert.Multiple(() =>
            {
                Assert.That(_mainPage.ToolbarTitleDisplayed, Is.True, "Toolbar title is not displayed.");
                Assert.That(_mainPage.ToolbarTitleEnabled, Is.True, "Toolbar title is not enabled.");
                Assert.That(_mainPage.ToolbarTitleText, Is.EqualTo(expectedTitle), "Toolbar title text is not correct.");
            });
        }

        [When(@"the user selects gender ""(.*)""")]
        public void WhenTheUserSelectsGender(string gender)
        {
            if (gender.ToLower() == "female")
            {
                _mainPage.ClickRadioButtonFemale();
            }
            else
            {
                _mainPage.ClickRadioButtonMale();
            }
        }

        [Then(@"the ""(.*)"" radio button should be selected")]
        public void ThenTheRadioButtonShouldBeSelected(string gender)
        {
            if (gender.ToLower() == "female")
            {
                Assert.That(_mainPage.RadioButtonFemaleChecked, Is.EqualTo("true"), "Female radio button is not selected.");
            }
            else
            {
                Assert.That(_mainPage.RadioButtonMaleChecked, Is.EqualTo("true"), "Male radio button is not selected.");
            }
        }

        [When(@"the user enters name ""(.*)""")]
        public void WhenTheUserEntersName(string name)
        {
            Assert.Multiple(() =>
            {
                Assert.That(_mainPage.NameFieldDisplayed, Is.True, "Name field is not displayed.");
                Assert.That(_mainPage.NameFieldEnabled, Is.True, "Name field is not enabled.");
            });
            
            _mainPage.EnterName(name);
        }

        [When(@"the user clicks the Let's Shop button")]
        public void WhenTheUserClicksTheLetsShopButton()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_mainPage.LetsShopButtonDisplayed, Is.True, "Let's Shop button is not displayed.");
                Assert.That(_mainPage.LetsShopButtonEnabled, Is.True, "Let's Shop button is not enabled.");
            });
            
            _mainPage.ClickLetsShopButton();
        }

        [Then(@"an error toast message should appear saying ""(.*)""")]
        public void ThenAnErrorToastMessageShouldAppearSaying(string expectedMessage)
        {
            bool toastAppeared = _wait.Until(d => d.PageSource.Contains(expectedMessage));
            Assert.That(toastAppeared, Is.True, $"Toast message '{expectedMessage}' not found in page source.");
        }

        [Then(@"the user should be on the products page")]
        public void ThenTheUserShouldBeOnTheProductsPage()
        {
            Assert.That(_mainPage.ToolbarTitleText, Is.EqualTo("Products"), "Products page title is not correct.");
        }
    }
}