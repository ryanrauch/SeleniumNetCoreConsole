using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumNetCoreConsole.Models;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole.Services
{
    public class NikePurchaseService : INikePurchaseService
    {
        private readonly IWebDriver _webDriver;
        private readonly IShippingAndBillingService _shippingAndBillingService;

        public NikePurchaseService(
            IWebDriver webDriver,
            IShippingAndBillingService shippingAndBillingService)
        {
            _webDriver = webDriver;
            _shippingAndBillingService = shippingAndBillingService;
        }

        public bool LaunchPurchase(DesiredShoe shoe)
        {
            ShippingInformation shipping = _shippingAndBillingService.GetShippingInformation();
            BillingInformation billing = _shippingAndBillingService.GetBillingInformation();
            IWebDriver driver = _webDriver;
            driver.Navigate().GoToUrl(shoe.URL);

            bool unreleased = false;
            while(!unreleased)
            {
                try
                {
                    driver.Navigate().GoToUrl(shoe.URL);

                    var notifyButton = driver.FindElement(By.CssSelector(".product-info .cta-btn"));
                    if (notifyButton.Text.Contains("Notify", StringComparison.OrdinalIgnoreCase))
                    {
                        unreleased = false;
                    }
                    //else
                    //{
                    //    driver.Quit();
                    //}
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    unreleased = true;
                }
                //if (notifyButton.Text.Contains("Notify", StringComparison.OrdinalIgnoreCase))
                //{
                //    unreleased = true;
                //}
                System.Threading.Thread.Sleep(shoe.RefreshInterval);
                Console.WriteLine("refreshed at " + DateTime.Now.ToString());
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
                                                         shoe.TargetSize))
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
            SeleniumSetMethods.EnterText(driver, "firstName", shipping.FirstName, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "lastName", shipping.LastName, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "address1", shipping.Address1, FindBy.Id);
            // need to expand address2 section here
            SeleniumSetMethods.EnterText(driver, "city", shipping.City, FindBy.Id);
            SeleniumSetMethods.SelectDropDown(driver, "state", shipping.State, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "postalCode", shipping.ZipCode, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "email", shipping.Email, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "phoneNumber", shipping.PhoneNumber, FindBy.Id);

            // Save and Continue Button
            SeleniumSetMethods.Click(driver, ".js-next-step", FindBy.CssSelector);

            // Continue to Payment
            SeleniumSetMethods.Click(driver, ".js-next-step", FindBy.CssSelector);

            System.Threading.Thread.Sleep(1000); ///////////////////////////////

            driver.SwitchTo().Frame(4);

            // Billing Information
            string ccn = billing.CreditCardNumber;
            SeleniumSetMethods.EnterText(driver, "creditCardNumber", ccn.Substring(0, 4), FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "creditCardNumber", ccn.Substring(4, 4), FindBy.Id);
            string creditCardNumber = ccn.Substring(8);
            foreach (var c in creditCardNumber.ToCharArray())
            {
                SeleniumSetMethods.EnterText(driver, "creditCardNumber", c.ToString(), FindBy.Id);
            }

            SeleniumSetMethods.EnterText(driver, "expirationDate", billing.ExpirationDate, FindBy.Id);
            SeleniumSetMethods.EnterText(driver, "cvNumber", billing.CVV2, FindBy.Id);

            if(Constants.DEBUG_MODE)
            {
                driver.Quit();
                return false; //works
            }

            driver.SwitchTo().ParentFrame();//relative=parent
            // Purchase
            SeleniumSetMethods.Click(driver, ".d-lg-ib", FindBy.CssSelector);
            return true;
        }

        public bool RegularPurchase(DesiredShoe shoe)
        {
            return false;
        }
    }
}
