using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole.Services
{
    public class NikePurchaseService : INikePurchaseService
    {
        private readonly IWebDriver _webDriver;

        public NikePurchaseService(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public async Task<bool> LaunchPurchase(string url)
        {
            IWebDriver driver = _webDriver;
            driver.Navigate().GoToUrl(url);

            var notifyButton = driver.FindElement(By.CssSelector(".product-info .cta-btn"));
            if(notifyButton.Text.Contains("Notify", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Select Size
            SeleniumSetMethods.Click(driver, "label", FindBy.Class);
            System.Threading.Thread.Sleep(250); ////////////////////////////////

            // 10.5 on Chrome???
            // 11.5 on Firefox
            //SeleniumSetMethods.Click(driver, ".size:nth-child(12) > .size-grid-dropdown", FindBy.CssSelector);
            //SeleniumSetMethods.Click(driver, ".size:nth-child(17) > .size-grid-dropdown", FindBy.CssSelector);
            bool foundSize = false;
            for (int i = 1; i < 20; ++i)
            {
                if (SeleniumSetMethods.ClickAndVerifyText(driver,
                                                         ".size:nth-child(" + i.ToString() + ") > .size-grid-dropdown",
                                                         FindBy.CssSelector,
                                                         Secrets.ShoeSize))
                {
                    foundSize = true;
                    break;
                }
                //else
                //{
                //    //Console.WriteLine("attempted nth-child: " + i.ToString());
                //}
            }
            if (!foundSize)
            {
                //Console.WriteLine("Unable to find correct shoe size.");
                return false;
            }
            System.Threading.Thread.Sleep(500); ////////////////////////////////

            // Add to cart
            SeleniumSetMethods.Click(driver, ".ncss-btn-black", FindBy.CssSelector);

            System.Threading.Thread.Sleep(500); ////////////////////////////////

            // Checkout
            SeleniumSetMethods.Click(driver, ".ncss-btn-black:nth-child(2)", FindBy.CssSelector);

            System.Threading.Thread.Sleep(500); ////////////////////////////////

            // Continue as guest
            SeleniumSetMethods.Click(driver, "qa-guest-checkout", FindBy.Id);

            System.Threading.Thread.Sleep(500); ////////////////////////////////

            // Shipping Information
            SeleniumSetMethods.EnterText(driver, "firstName", Secrets.FirstName, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "lastName", Secrets.LastName, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "address1", Secrets.Address1, FindBy.Id);
            // need to expand address2 section here
            SeleniumSetMethods.EnterText(driver, "city", Secrets.City, FindBy.Id);
            SeleniumSetMethods.SelectDropDown(driver, "state", Secrets.State, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "postalCode", Secrets.ZipCode, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "email", Secrets.Email, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "phoneNumber", Secrets.PhoneNumber, FindBy.Id);

            // Save and Continue Button
            SeleniumSetMethods.Click(driver, ".js-next-step", FindBy.CssSelector);

            // Continue to Payment
            SeleniumSetMethods.Click(driver, ".js-next-step", FindBy.CssSelector);

            System.Threading.Thread.Sleep(1000); ///////////////////////////////

            driver.SwitchTo().Frame(4);

            // Billing Information
            string ccn = Secrets.CreditCardNumber;
            SeleniumSetMethods.EnterText(driver, "creditCardNumber", ccn.Substring(0, 4), FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "creditCardNumber", ccn.Substring(4, 4), FindBy.Id);
            string creditCardNumber = ccn.Substring(8);
            foreach (var c in creditCardNumber.ToCharArray())
            {
                SeleniumSetMethods.EnterText(driver, "creditCardNumber", c.ToString(), FindBy.Id);
            }

            SeleniumSetMethods.EnterText(driver, "expirationDate", Secrets.ExpDate, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "cvNumber", Secrets.CVNumber, FindBy.Id);

            if(Secrets.DEBUG_MODE)
            {
                return false; //works
            }

            driver.SwitchTo().ParentFrame();//relative=parent
            // Purchase
            SeleniumSetMethods.Click(driver, ".d-lg-ib", FindBy.CssSelector);
            return true;
        }

        public async Task<bool> RegularPurchase(string url)
        {
            return false;
        }
    }
}
