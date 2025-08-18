// using OpenQA.Selenium;
// using OpenQA.Selenium.Appium;
// using OpenQA.Selenium.Appium.Android;
// using OpenQA.Selenium.Support.UI;
// using GeneralStore.Configs;
// using GeneralStore.Pages.ProductPage;
// using GeneralStore.Pages.MainPage;
// using OpenQA.Selenium.Interactions;
// using OpenQA.Selenium.Appium.Android.Enums;


// namespace ProductPageTests

// {

//     [TestFixture]

//     public class ProductPageTests

//     {

//          private AndroidDriver driver;
//         private MainPage _registrationForm;
//         private ProductPage _productPage;
//         private WebDriverWait? wait;

//         public void SwipeHorizontal(AndroidDriver driver)
//         {
//             var size = driver.Manage().Window.Size;
//             int startX = 1;
//             int endX = (int)(size.Width * 0.9);
//             int y = size.Height / 2;
 
//             SwipeWithActions(driver, startX, y, endX, y);
//         }


//         public void SwipeWithActions(AndroidDriver driver, int startX, int startY, int endX, int endY)
//         {
//             var finger = new PointerInputDevice(PointerKind.Touch);
//             var swipe = new ActionSequence(finger, 0);

//             swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.Zero));
//             swipe.AddAction(finger.CreatePointerDown(0));
//             swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromMilliseconds(1000)));
//             swipe.AddAction(finger.CreatePointerUp(0));

//             driver.PerformActions(new List<ActionSequence> { swipe });
//         }
//         public void SwipeBackGesture(AndroidDriver driver)
//         {
//             var size = driver.Manage().Window.Size;

//             int startX = 1;
//             int endX = (int)(size.Width * 0.7);
//             int y = size.Height / 2;

//             var finger = new PointerInputDevice(PointerKind.Touch);
//             var swipe = new ActionSequence(finger, 0);

//             swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, y, TimeSpan.Zero));
//             swipe.AddAction(finger.CreatePointerDown(0));
//             swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, y, TimeSpan.FromMilliseconds(1000)));
//             swipe.AddAction(finger.CreatePointerUp(0));

//             driver.PerformActions(new List<ActionSequence> { swipe });
//         }
//         public void GoBack(AndroidDriver driver, string mode = "swipe")
// {
//     if (mode.ToLower() == "swipe")
//     {
//         var size = driver.Manage().Window.Size;
 
//         int startX = 1;
//         int endX = (int)(size.Width * 0.7);    
//         int y = size.Height / 2;               
 
//         var finger = new PointerInputDevice(PointerKind.Touch);
//         var swipe = new ActionSequence(finger, 0);
 
//         swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, y, TimeSpan.Zero));
//         swipe.AddAction(finger.CreatePointerDown(0));
//         swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, y, TimeSpan.FromMilliseconds(1000)));
//         swipe.AddAction(finger.CreatePointerUp(0));
 
//         driver.PerformActions(new List<ActionSequence> { swipe });
 
//     }
//     else if (mode.ToLower() == "key")
//     {
//         driver.PressKeyCode(AndroidKeyCode.Back);
//     }
//     else
//     {
//         throw new ArgumentException("Mode must be either 'swipe' or 'key'.");
//     }
// }

//         [SetUp]
//         public void SetUp()
//         {
//             var serverUrl = new Uri(
//                 Environment.GetEnvironmentVariable("APPIUM_SERVER_URL")
//                 ?? "http://localhost:4723/wd/hub");

//             driver = new AndroidDriver(
//                 serverUrl,
//                 AppiumConfig.BuildAndroidOptions(),
//                 TimeSpan.FromSeconds(180));

//             driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
//             _registrationForm = new MainPage(driver);
//             _productPage = new ProductPage(driver);
//         }
//         [Test]
//         public void LogInToProductPage()
//         {
//             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
//             _registrationForm.EnterName("Test User");
//             _registrationForm.ClickLetsShopButton();
//             // SwipeHorizontal(driver);
//             Assert.Multiple(() =>
//             {
//                 Assert.That(_registrationForm.ToolbarTitleDisplayed, Is.True, "Toolbar title is not displayed.");
//                 Assert.That(_registrationForm.ToolbarTitleEnabled, Is.True, "Toolbar title is not enabled.");
//                 Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("Products"), "Toolbar title text is not correct.");

//                 Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("Products"), "Products page title is not correct after clicking Let's Shop button.");

//                 Assert.That(_productPage.ParentIsNotNull, "У RecyclerView нет родительского элемента");

//                 Assert.That(_productPage.ProductListDisplayed, Is.True, "RecyclerView is not displayed.");
//                 Assert.That(_productPage.ProductElementsCount, Is.GreaterThan(0), "RecyclerView does not contain any items.");
//                 Console.WriteLine("Items found in RecyclerView: " + _productPage.ProductElementsCount);
//                 Console.WriteLine("Product items found in RecyclerView: " + _productPage.ProductItemsCount);
//                 Assert.That(_productPage.ProductItemsCount, Is.GreaterThan(0), "Product items are not displayed in the RecyclerView.");
//                 var productAdd = wait.Until(drv => drv.FindElement(By.XPath("(//android.widget.TextView[@resource-id='com.androidsample.generalstore:id/productAddCart'])[1]")));
//                 productAdd.Click();
//                 _productPage.ClickOnEmptyBucket();
//                 GoBack(driver, "swipe");
//                 Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("Products"), "Products page title is not correct after clicking back gesture.");
                
//             });
            
//         }
//         [Test]
//         public void ClickOnEmptyBucket()
//         {
//             _registrationForm.EnterName("Test User");
//             _registrationForm.ClickLetsShopButton();
//             Assert.That(_productPage.IsBucketDisplayed, Is.True, "Bucket button is not displayed.");
//             Assert.That(_productPage.IsBucketEnabled, Is.True, "Bucket button is not enabled.");
//             _productPage.ClickOnEmptyBucket();
//             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
//             bool toastAppeared = wait.Until(d => d.PageSource.Contains("Please add some product at first"));
//             Assert.That(toastAppeared, Is.True, "Toast message not found in page source.");
//         }    


//         [TearDown]
//         public void TearDown()
//         {
//             try
//             {
//                 driver?.TerminateApp("com.androidsample.generalstore");
//                 driver?.RemoveApp("com.androidsample.generalstore");
//             }
//             catch (Exception e)
//             {
//                 TestContext.Progress.WriteLine($"Не удалось удалить приложение: {e.Message}");
//             }
//             finally
//             {
//                 driver?.Quit();
//                 driver?.Dispose();
//             }

//         }

//     }

// }

using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using GeneralStore.Configs;
using GeneralStore.Pages.ProductPage;
using GeneralStore.Pages.MainPage;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.Android.Enums;
using System.Threading;
 
namespace ProductPageTests
{
    [TestFixture]
    public class ProductPageTests
    {
        private AndroidDriver driver;
        private MainPage _registrationForm;
        private ProductPage _productPage;
        private WebDriverWait? wait;
 
        
        public void SwipeProductListHorizontally(ProductPage productPage, double startPercent = 0.8, double endPercent = 0.2, int durationMs = 2000)
        {
            var element = productPage.ProductList;
            var location = element.Location;
            var size = element.Size;
 
            int startX = location.X + (int)(size.Width * startPercent);
            int endX = location.X + (int)(size.Width * endPercent);
            int y = location.Y + size.Height / 2;
 
            Thread.Sleep(300);
 
            var finger = new PointerInputDevice(PointerKind.Touch);
            var swipe = new ActionSequence(finger, 0);
 
            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, y, TimeSpan.Zero));
            swipe.AddAction(finger.CreatePointerDown(0));
            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, y, TimeSpan.FromMilliseconds(durationMs)));
            swipe.AddAction(finger.CreatePointerUp(0));
 
            driver.PerformActions(new List<ActionSequence> { swipe });
            Thread.Sleep(500); 
        }


        //         public void GoBack(AndroidDriver driver, string mode = "swipe")
        // {
        //     if (mode.ToLower() == "swipe")
        //     {
        //         var size = driver.Manage().Window.Size;
        //         int startX = (int)(size.Width * 0.01); 
        //         int endX = (int)(size.Width * 0.8);    
        //         int y = size.Height / 2;

        //         Thread.Sleep(300);

        //         var finger = new PointerInputDevice(PointerKind.Touch);
        //         var swipe = new ActionSequence(finger, 0);

        //         swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, y, TimeSpan.Zero));
        //         swipe.AddAction(finger.CreatePointerDown(0));
        //         swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, y, TimeSpan.FromMilliseconds(1500))); 

        //         driver.PerformActions(new List<ActionSequence> { swipe });
        //         Thread.Sleep(1000); // дать время системе
        //     }
        //     else if (mode.ToLower() == "key")
        //     {
        //         driver.PressKeyCode(AndroidKeyCode.Back);
        //     }
        //     else
        //     {
        //         throw new ArgumentException("Mode must be either 'swipe' or 'key'.");
        //     }
        // }
public void GoBack(AndroidDriver driver)
        {
            driver.Navigate().Back();
            Thread.Sleep(500); 
        }

        // --- Setup ---
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
            _productPage = new ProductPage(driver);
        }
 
      
        [Test]
        public void LogInToProductPage()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            _registrationForm.EnterName("Test User");
            _registrationForm.ClickLetsShopButton();
 
            Assert.Multiple(() =>
            {
                Assert.That(_registrationForm.ToolbarTitleDisplayed, Is.True, "Toolbar title is not displayed.");
                Assert.That(_registrationForm.ToolbarTitleEnabled, Is.True, "Toolbar title is not enabled.");
                Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("Products"), "Toolbar title text is not correct.");
                Assert.That(_productPage.ParentIsNotNull, "У RecyclerView нет родительского элемента");
                Assert.That(_productPage.ProductListDisplayed, Is.True, "RecyclerView is not displayed.");
                Assert.That(_productPage.ProductElementsCount, Is.GreaterThan(0), "RecyclerView does not contain any items.");
 
                Console.WriteLine("Items found in RecyclerView: " + _productPage.ProductElementsCount);
                Console.WriteLine("Product items found in RecyclerView: " + _productPage.ProductItemsCount);
 
                Assert.That(_productPage.ProductItemsCount, Is.GreaterThan(0), "Product items are not displayed in the RecyclerView.");
 
              
                // SwipeProductListHorizontally(_productPage);
 
                var productAdd = wait.Until(drv => drv.FindElement(By.XPath("(//android.widget.TextView[@resource-id='com.androidsample.generalstore:id/productAddCart'])[1]")));
                productAdd.Click();
 
                _productPage.ClickOnEmptyBucket();
                GoBack(driver);
 
                Assert.That(_registrationForm.ToolbarTitleText, Is.EqualTo("Products"), "Products page title is not correct after clicking back gesture.");
            });
        }
 
        
        [Test]
        public void ClickOnEmptyBucket()
        {
            _registrationForm.EnterName("Test User");
            _registrationForm.ClickLetsShopButton();
            Assert.That(_productPage.IsBucketDisplayed, Is.True, "Bucket button is not displayed.");
            Assert.That(_productPage.IsBucketEnabled, Is.True, "Bucket button is not enabled.");
 
            _productPage.ClickOnEmptyBucket();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
 
            bool toastAppeared = wait.Until(d => d.PageSource.Contains("Please add some product at first"));
            Assert.That(toastAppeared, Is.True, "Toast message not found in page source.");
        }
 
        // --- TearDown ---
        [TearDown]
        public void TearDown()
        {
            try
            {
                driver?.TerminateApp("com.androidsample.generalstore");
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

 

 