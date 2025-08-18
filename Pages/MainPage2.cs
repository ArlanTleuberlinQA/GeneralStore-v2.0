// using OpenQA.Selenium;
// using OpenQA.Selenium.Appium.Android;
// using OpenQA.Selenium.Appium;
// using GeneralStore.Configs;
// using NUnit.Framework;
// using System;
 
// namespace GeneralStore.Pages.MainPage
// {
//     public class MainPage : DriverFactory
//     {
//         #region Locators
//         private static class L
//         {
//             public static readonly By Splash              = By.Id("com.androidsample.generalstore:id/splashscreen");
//             public static readonly By NavBarBackground    = By.Id("android:id/navigationBarBackground");
//             public static readonly By LetsShopButton      = By.Id("com.androidsample.generalstore:id/btnLetsShop");
//             public static readonly By NameField           = By.Id("com.androidsample.generalstore:id/nameField");
//             public static readonly By SelectCountryPrompt = By.XPath("//android.widget.TextView[@text='Select the country where you want to shop']");
//             public static readonly By CountrySpinner      = By.Id("com.androidsample.generalstore:id/spinnerCountry");
//             public static readonly By SelectedCountry     = By.Id("android:id/text1");
//             public static readonly By BackgroundImage     = By.ClassName("android.widget.ImageView");
//             public static readonly By ToolbarTitle        = By.Id("com.androidsample.generalstore:id/toolbar_title");
//             public static readonly By RadioFemale         = By.Id("com.androidsample.generalstore:id/radioFemale");
//             public static readonly By RadioMale           = By.Id("com.androidsample.generalstore:id/radioMale");
//         }
//         #endregion
 
//         public MainPage(AndroidDriver driver) : base(driver) { }
 
//         #region Generic helpers
//         private IWebElement El(By locator) => WaitFor(locator);
//         private bool VisibleAndEnabled(By locator)
//         {
//             var e = El(locator);
//             return e.Displayed && e.Enabled;
//         }
//         private string? Attr(By locator, string name) => El(locator).GetAttribute(name);
//         #endregion
 
//         #region Splash‑screen
//         public bool IsSplashReady     => VisibleAndEnabled(L.Splash);
//         public bool IsSplashClickable => Attr(L.Splash, "clickable") == "true";
//         public bool SplashHasText     => !string.IsNullOrEmpty(El(L.Splash).Text);
//         #endregion
 
//         #region Navigation bar / toolbar
//         public bool IsNavBarReady   => VisibleAndEnabled(L.NavBarBackground);
//         public string ToolbarTitle  => El(L.ToolbarTitle).Text;
//         #endregion
 
//         #region “Let’s shop” кнопка
//         public bool IsLetsShopReady => VisibleAndEnabled(L.LetsShopButton);
//         public MainPage ClickLetsShop()
//         {
//             El(L.LetsShopButton).Click();
//             return this;
//         }
//         #endregion
 
//         #region Имя пользователя
//         public bool IsNameFieldReady => VisibleAndEnabled(L.NameField);
//         public MainPage EnterName(string name)
//         {
//             El(L.NameField).SendKeys(name);
//             return this;
//         }
//         #endregion
 
//         #region Страна
//         public bool IsSelectCountryPromptReady => VisibleAndEnabled(L.SelectCountryPrompt);
//         public string SelectCountryPromptText  => El(L.SelectCountryPrompt).Text;
//         public bool IsCountrySpinnerReady      => VisibleAndEnabled(L.CountrySpinner);
//         public bool IsCountrySpinnerClickable  => Attr(L.CountrySpinner, "clickable") == "true";
//         public string SelectedCountryText      => El(L.SelectedCountry).Text;
 
//         public MainPage SelectCountry(string country)
//         {
//             El(L.CountrySpinner).Click();
 
//             var countryElement = Wait.Until(d => d.FindElement(
//                 MobileBy.AndroidUIAutomator(
//                     $"new UiScrollable(new UiSelector().className(\"android.widget.ListView\"))" +
//                     $".scrollIntoView(new UiSelector().text(\"{country}\"))")));
 
//             countryElement.Click();
//             return this;
//         }
//         #endregion
 
//         #region Gender
//         public enum Gender { Female, Male }
//         public bool IsFemaleSelected => Attr(L.RadioFemale, "checked") == "true";
//         public bool IsMaleSelected   => Attr(L.RadioMale,   "checked") == "true";
 
//         public MainPage SelectGender(Gender gender)
//         {
//             switch (gender)
//             {
//                 case Gender.Female: El(L.RadioFemale).Click(); break;
//                 case Gender.Male:   El(L.RadioMale).Click();   break;
//                 default: throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
//             }
//             return this;
//         }
//         #endregion
 
//         #region Assertion util
//         public static void AssertCountryDefaultProps(IWebElement country, string expectedText = "Afghanistan")
//         {
//             Assert.Multiple(() =>
//             {
//                 Assert.That(country.Text, Is.EqualTo(expectedText), "Wrong country text");
//                 Assert.That(country.Displayed,  "Not displayed");
//                 Assert.That(country.Enabled,    "Not enabled");
//                 Assert.That(country.GetAttribute("clickable"), Is.EqualTo("false"), "Should not be clickable");
//                 Assert.That(country.GetAttribute("focusable"), Is.EqualTo("false"), "Should not be focusable");
//                 Assert.That(country.GetAttribute("focused"),   Is.EqualTo("false"), "Should not be focused");
//                 Assert.That(country.GetAttribute("scrollable"),Is.EqualTo("false"), "Should not be scrollable");
//                 Assert.That(country.GetAttribute("selected"), Is.EqualTo("false"), "Should not be selected");
//                 Assert.That(country.GetAttribute("checked"),  Is.EqualTo("false"), "Should not be checked");
//             });
//         }
//         #endregion
//     }
// }