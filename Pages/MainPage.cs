using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using GeneralStore.Configs;
using OpenQA.Selenium.Appium;

namespace GeneralStore.Pages.MainPage
{
    public class MainPage : DriverFactory
    {
        private readonly By _splash = By.Id("com.androidsample.generalstore:id/splashscreen");
        private readonly By _navBg = By.Id("android:id/navigationBarBackground");
        private readonly By _letsShop = By.Id("com.androidsample.generalstore:id/btnLetsShop");
        private readonly By _nameField = By.Id("com.androidsample.generalstore:id/nameField");
        private readonly By _selectCountryText = By.XPath("//android.widget.TextView[@text='Select the country where you want to shop']");
        private readonly By _spinnerCountry = By.Id("com.androidsample.generalstore:id/spinnerCountry");
        private readonly By _selectedCountry = By.Id("android:id/text1");
        private readonly By _backGroundImage = By.ClassName("android.widget.ImageView");
        private readonly By _toolbarTitle = By.Id("com.androidsample.generalstore:id/toolbar_title");
        
        private readonly By _radioButtonFemale = By.Id("com.androidsample.generalstore:id/radioFemale");
        private readonly By _radioButtonMale = By.Id("com.androidsample.generalstore:id/radioMale");

        public MainPage(AndroidDriver driver) : base(driver) { }


        public bool IsDisplayed => WaitFor(_splash).Displayed;
        public bool IsEnabled => WaitFor(_splash).Enabled;
        public bool HasAnyText => !string.IsNullOrEmpty(WaitFor(_splash).Text);


        public bool NavBarDisplayed => WaitFor(_navBg).Displayed;
        public bool NavBarEnabled => WaitFor(_navBg).Enabled;
        public bool LetsShopButtonDisplayed => WaitFor(_letsShop).Displayed;
        public bool LetsShopButtonEnabled => WaitFor(_letsShop).Enabled;
        public bool NameFieldDisplayed => WaitFor(_nameField).Displayed;
        public bool NameFieldEnabled => WaitFor(_nameField).Enabled;
        public bool SelectCountryTextDisplayed => WaitFor(_selectCountryText).Displayed;
        public bool SelectCountryTextEnabled => WaitFor(_selectCountryText).Enabled;
        public string SelectCountryText => WaitFor(_selectCountryText).Text;
        public string? SelectCountryTextClickable
            => WaitFor(_selectCountryText) != null ? WaitFor(_selectCountryText).GetAttribute("clickable") : null;
        public bool SpinnerCountryDisplayed => WaitFor(_spinnerCountry).Displayed;
        public bool SpinnerCountryEnabled => WaitFor(_spinnerCountry).Enabled;
        public string? SpinnerCountryClickable => WaitFor(_spinnerCountry) != null ? WaitFor(_spinnerCountry).GetAttribute("clickable") : null;
        public bool SelectedCountryDisplayed => WaitFor(_selectedCountry).Displayed;
        public bool SelectedCountryEnabled => WaitFor(_selectedCountry).Enabled;
        public string SelectedCountryText => WaitFor(_selectedCountry).Text;
        public bool BackGroundImageDisplayed => WaitFor(_backGroundImage).Displayed;
        public bool BackGroundImageEnabled => WaitFor(_backGroundImage).Enabled;
        public bool ToolbarTitleDisplayed => WaitFor(_toolbarTitle).Displayed;
        public bool ToolbarTitleEnabled => WaitFor(_toolbarTitle).Enabled;
        public string ToolbarTitleText => WaitFor(_toolbarTitle).Text;
        
        public bool RadioButtonFemaleDisplayed => WaitFor(_radioButtonFemale).Displayed;
        public bool RadioButtonFemaleEnabled => WaitFor(_radioButtonFemale).Enabled;
        public string? RadioButtonFemaleChecked => WaitFor(_radioButtonFemale) != null ? WaitFor(_radioButtonFemale).GetAttribute("checked") : null;
        public string? RadioButtonMaleChecked => WaitFor(_radioButtonMale) != null ? WaitFor(_radioButtonMale).GetAttribute("checked") : null;
        public bool RadioButtonMaleDisplayed => WaitFor(_radioButtonMale).Displayed;
        public bool RadioButtonMaleEnabled => WaitFor(_radioButtonMale).Enabled;
        public void ClickLetsShopButton()
        {
            WaitFor(_letsShop).Click();
        }
        public void EnterName(string name)
        {
            var nameField = WaitFor(_nameField);
            nameField.SendKeys(name);
        }

        public void SelectCountry(string country)
{

    var spinner = WaitFor(_spinnerCountry);
    spinner.Click();

    var countryElement = WaitFor(
        MobileBy.AndroidUIAutomator(
            $"new UiScrollable(new UiSelector().className(\"android.widget.ListView\")).scrollIntoView(new UiSelector().text(\"{country}\"))"
        ));
        countryElement.Click();
    }
        public void ClickRadioButtonFemale()
        {
            var femaleChecker = WaitFor(_radioButtonFemale);
            femaleChecker.Click();
        }
        public void ClickRadioButtonMale()
        {
            var maleCgecker = WaitFor(_radioButtonMale);
            maleCgecker.Click();
        }
        public static void VerifyCountryProperties(IWebElement country)
        {
            string expectedCountryText = "Afghanistan";
            string actualCountryText = country.Text;


            Assert.That(expectedCountryText, Is.EqualTo(actualCountryText), "Country name is incorrect.");
            TestContext.Out.WriteLine("Country text is correct.");


            Assert.That(country.Displayed, "Country is not displayed.");
            TestContext.Out.WriteLine("Country is displayed.");


            Assert.That(country.Enabled, "Country is not enabled.");
            TestContext.Out.WriteLine("Country is enabled.");


            var clickable = country.GetAttribute("clickable");
            Assert.That(clickable, Is.EqualTo("false"), "Country is clickable but should not be.");
            TestContext.Out.WriteLine("Country is not clickable.");


            var focusable = country.GetAttribute("focusable");
            Assert.That(focusable, Is.EqualTo("false"), "Country is focusable but should not be.");
            TestContext.Out.WriteLine("Country is not focusable.");


            var focused = country.GetAttribute("focused");
            Assert.That(focused, Is.EqualTo("false"), "Country is focused but should not be.");
            TestContext.Out.WriteLine("Country is not focused.");


            var scrollable = country.GetAttribute("scrollable");
            Assert.That(scrollable, Is.EqualTo("false"), "Country is scrollable but should not be.");
            TestContext.Out.WriteLine("Country is not scrollable.");


            var selected = country.GetAttribute("selected");
            Assert.That(selected, Is.EqualTo("false"), "Country is selected but should not be.");
            TestContext.Out.WriteLine("Country is not selected.");


            var checkedAttr = country.GetAttribute("checked");
            Assert.That(checkedAttr, Is.EqualTo("false"), "Country is checked but should not be.");
            TestContext.Out.WriteLine("Country is not checked.");
        }
        // public static bool IsElementDisplayed(AndroidDriver driver, By locator, int timeoutInSeconds = 10)
            // {
            //     try
            //     {
            //         var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            //         return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
            //     }
            //     catch (WebDriverTimeoutException)
            //     {
            //         return false;
            //     }
            // }
        }
}