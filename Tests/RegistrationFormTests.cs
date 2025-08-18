using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using GeneralStore.Configs;
using GeneralStore.Pages.MainPage;


namespace GeneralStore.Tests.RegistrationFormTests

{
    [TestFixture]
    public class RegistrationTests
    {
        private AndroidDriver driver;
        private MainPage _registrationForm;
        private WebDriverWait? wait;

        [SetUp]
        public void SetUp()
        {
            var serverUrl = new Uri(
                Environment.GetEnvironmentVariable("APPIUM_SERVER_URL")
                ?? "http://localhost:4723/wd/hub");

            driver = new AndroidDriver(
                serverUrl,
                AppiumConfig.BuildAndroidOptions(),
                TimeSpan.FromSeconds(180));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _registrationForm = new MainPage(driver);
        }

        [Test]

        public void VerifySelectCountryText()

        {

            string expectedResultText = "Select the country where you want to shop";
            Assert.Multiple(() =>
            {
                Assert.That(_registrationForm.SelectCountryTextDisplayed, Is.True, "Text не отображается");
                Assert.That(_registrationForm.SelectCountryTextEnabled, Is.True, " не Enabled");
                Assert.That(_registrationForm.SelectCountryTextClickable, Is.EqualTo("false"), "не кликабелен");
                Assert.That(_registrationForm.SelectCountryText, Is.EqualTo(expectedResultText), "Text не соответствует ожидаемому");
            });
        }


        [Test]
        public void Country_Afghanistan_HasCorrectProperties()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var country = wait.Until(drv => drv.FindElement(By.XPath("//android.widget.TextView[@resource-id='android:id/text1']")));

            MainPage.VerifyCountryProperties(country);
        }


        [Test]
        public void ChooseUkraineTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_registrationForm.SpinnerCountryDisplayed, Is.True, "Select country is not displayed.");
                Assert.That(_registrationForm.SpinnerCountryEnabled, Is.True, "Select country is not enabled.");
                Assert.That(_registrationForm.SpinnerCountryClickable, Is.EqualTo("true"), "Select country is not clickable.");
                TestContext.Out.WriteLine("Select country is displayed, enabled, and clickable.");
                _registrationForm.SelectCountry("France");
                Assert.That(_registrationForm.SelectedCountryText, Is.EqualTo("France"));
                Assert.That(_registrationForm.SelectedCountryDisplayed, Is.True, "Selected country is not displayed.");
                Assert.That(_registrationForm.SelectedCountryEnabled, Is.True, "Selected country is not enabled.");
            });
           
        }
        [Test]
        public void BackgoundImageTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_registrationForm.BackGroundImageDisplayed, Is.True, "Background image is not displayed.");
                Assert.That(_registrationForm.BackGroundImageEnabled, Is.True, "Background image is not enabled.");
                TestContext.Out.WriteLine("Background image is displayed and enabled.");
            });
        }
        [Test]
        public void ToolBarTitleTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_registrationForm.ToolbarTitleDisplayed, Is.True, "Toolbar title is not displayed.");
                Assert.That(_registrationForm.ToolbarTitleEnabled, Is.True, "Toolbar title is not enabled.");
                Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("General Store"), "Toolbar title text is not correct.");
                TestContext.Out.WriteLine("Toolbar title is displayed, enabled, and has correct text.");
            });
        }
        [Test]
        public void RadioButtonsTest()
        {
            Assert.That(_registrationForm.RadioButtonMaleDisplayed, Is.True, "Radio button is not displayed.");
            Assert.That(_registrationForm.RadioButtonMaleEnabled, Is.True, "Radio button is not enabled.");
            Assert.That(_registrationForm.RadioButtonMaleChecked, Is.EqualTo("true"), "Radio button is selected by default.");

            Assert.That(_registrationForm.RadioButtonFemaleDisplayed, Is.True, "Radio button is not displayed.");
            Assert.That(_registrationForm.RadioButtonFemaleEnabled, Is.True, "Radio button is not enabled.");
            Assert.That(_registrationForm.RadioButtonFemaleChecked, Is.EqualTo("false"), "Radio button is not selected by default.");
            _registrationForm.ClickRadioButtonFemale();
            Assert.That(_registrationForm.RadioButtonFemaleChecked, Is.EqualTo("true"), "Radio button is not selected after clicking.");
            Assert.That(_registrationForm.RadioButtonMaleChecked, Is.EqualTo("false"), "Radio button is not selected after clicking the other one.");
            _registrationForm.ClickRadioButtonMale();
            Assert.That(_registrationForm.RadioButtonMaleChecked, Is.EqualTo("true"), "Radio button is selected after clicking.");
            Assert.That(_registrationForm.RadioButtonFemaleChecked, Is.EqualTo("false"), "Radio button is not selected after clicking the other one.");
            TestContext.Out.WriteLine("Radio button is displayed, enabled, and selected.");
        }
        [Test]
        public void LogInWithEmptyName()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_registrationForm.NameFieldDisplayed, Is.True, "Name field is not displayed.");
                Assert.That(_registrationForm.NameFieldEnabled, Is.True, "Name field is not enabled.");
                Assert.That(_registrationForm.LetsShopButtonDisplayed, Is.True, "Let's Shop button is not displayed.");
                Assert.That(_registrationForm.LetsShopButtonEnabled, Is.True, "Let's Shop button is not enabled.");
                _registrationForm.ClickLetsShopButton();
                TestContext.Out.WriteLine("Let's Shop button is clicked.");
                TestContext.Out.WriteLine("Name field is displayed and enabled.");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                bool toastAppeared = wait.Until(d => d.PageSource.Contains("Please enter your name"));
                Assert.That(toastAppeared, Is.True, "Toast message not found in page source.");
                TestContext.Out.WriteLine("Error message for empty name field is displayed.");
                Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("General Store"), "Products page title is not correct after clicking Let's Shop button.");
            });
           
        }
        [Test]
        public void LoginWithFemaleGender()
        {

            _registrationForm.EnterName("Test User");
            _registrationForm.ClickRadioButtonFemale();
            _registrationForm.ClickLetsShopButton();

            Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("Products"), "Products page title is not correct after clicking Let's Shop button.");
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                driver?.RemoveApp("com.androidsample.generalstore");
            }
            catch (Exception e)
            {
                TestContext.Progress.WriteLine($"Не удалось удалить приложение: {e.Message}");
            }
            finally
            {
                driver?.Quit();
                driver?.Dispose();
            }

        }
    }

}
