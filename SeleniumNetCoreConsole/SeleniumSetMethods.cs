using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumNetCoreConsole
{
    public class SeleniumSetMethods
    {
        public static void EnterText(IWebDriver driver, string element, string value, FindBy elementType)
        {
            switch(elementType)
            {
                case FindBy.Id:
                    driver.FindElement(By.Id(element)).SendKeys(value);
                    break;
                case FindBy.Name:
                    driver.FindElement(By.Name(element)).SendKeys(value);
                    break;
                case FindBy.Class:
                    driver.FindElement(By.ClassName(element)).SendKeys(value);
                    break;
                default:
                    throw new ArgumentException(elementType.ToString() + " elementType not recognized.");
            }
        }

        public static bool ClickAndVerifyText(IWebDriver driver, string element, FindBy elementType, string value)
        {
            switch (elementType)
            {
                case FindBy.Id:
                    //driver.FindElement(By.Id(element)).Click();
                    break;
                case FindBy.Name:
                    //driver.FindElement(By.Name(element)).Click();
                    break;
                case FindBy.Class:
                    //driver.FindElement(By.ClassName(element)).Click();
                    break;
                case FindBy.CssSelector:
                    var target = driver.FindElement(By.CssSelector(element));
                    if(target.Text.Equals(value))
                    {
                        target.Click();
                        return true;
                    }
                    break;
                case FindBy.LinkText:
                    //driver.FindElement(By.LinkText(element)).Click();
                    break;
                default:
                    throw new ArgumentException(elementType.ToString() + " elementType not recognized.");
            }
            return false;
        }

        public static void Click(IWebDriver driver, string element, FindBy elementType)
        {
            switch (elementType)
            {
                case FindBy.Id:
                    driver.FindElement(By.Id(element)).Click();
                    break;
                case FindBy.Name:
                    driver.FindElement(By.Name(element)).Click();
                    break;
                case FindBy.Class:
                    driver.FindElement(By.ClassName(element)).Click();
                    break;
                case FindBy.CssSelector:
                    driver.FindElement(By.CssSelector(element)).Click();
                    break;
                case FindBy.LinkText:
                    driver.FindElement(By.LinkText(element)).Click();
                    break;
                default:
                    throw new ArgumentException(elementType.ToString() + " elementType not recognized.");
            }
        }


        public static void SelectDropDown(IWebDriver driver, string element, string value, FindBy elementType)
        {
            switch(elementType)
            {
                case FindBy.Id:
                    new SelectElement(driver.FindElement(By.Id(element))).SelectByText(value);
                    break;
                case FindBy.Name:
                    new SelectElement(driver.FindElement(By.Name(element))).SelectByText(value);
                    break;
                case FindBy.Class:
                    new SelectElement(driver.FindElement(By.ClassName(element))).SelectByText(value);
                    break;
                default:
                    throw new ArgumentException(elementType.ToString() + " elementType not recognized.");
            }
        }
    }
}
