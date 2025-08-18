using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using GeneralStore.Configs;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.Events;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;

namespace GeneralStore.Pages.ProductPage
{
    public class ProductPage : DriverFactory
    {
       
        private readonly By _productList = By.Id("com.androidsample.generalstore:id/rvProductList");
        private readonly By _parentElement = By.XPath("//android.support.v7.widget.RecyclerView[@resource-id='com.androidsample.generalstore:id/rvProductList']/..");
        private readonly By _emptyBucket = By.Id("com.androidsample.generalstore:id/appbar_btn_cart");
        public IReadOnlyCollection<IWebElement> ProductElements => ProductList.FindElements(By.XPath(".//*"));
        public IReadOnlyCollection<IWebElement> ProductItems => ProductList.FindElements(By.ClassName("android.widget.RelativeLayout"));
 public ProductPage(AndroidDriver driver) : base(driver) { }
        public bool ProductListDisplayed => WaitFor(_productList).Displayed;
        public bool ProductListEnabled => WaitFor(_productList).Enabled;
        public IWebElement ProductList => WaitFor(_productList);
        public int ProductElementsCount => ProductElements.Count;
        public int ProductItemsCount => ProductItems.Count;
        public bool ParentDisplayed => WaitFor(_parentElement).Displayed;
        public bool ParentEnabled => WaitFor(_parentElement).Enabled;
        public bool ParentIsNotNull => WaitFor(_parentElement) != null;
        public void ClickOnEmptyBucket()
        {
            var emptyBucketButton = WaitFor(_emptyBucket);
            if (emptyBucketButton != null && emptyBucketButton.Displayed && emptyBucketButton.Enabled)
            {
                emptyBucketButton.Click();
            }
            else
            {
                throw new NoSuchElementException("Empty bucket button is not displayed or enabled.");
            }
        }
        public bool IsBucketDisplayed => WaitFor(_emptyBucket).Displayed;
        public bool IsBucketEnabled => WaitFor(_emptyBucket).Enabled;
    }
}