using System;
using OpenQA.Selenium;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole
{
    public class ApplicationLogic
    {

        private readonly IWebDriver _webDriver;
        private readonly INikePurchaseService _nikePurchaseService;

        public ApplicationLogic(
            IWebDriver webDriver,
            INikePurchaseService nikePurchaseService
        )
        {
            _webDriver = webDriver;
            _nikePurchaseService = nikePurchaseService;
        }

        public void Run(string[] args)
        {
            //run code here
            // un-released:
            //https://www.nike.com/launch/t/adapt-bb-black-white-pure-platinum/

            // launch
            //https://www.nike.com/launch/t/air-max-720-satrn-motorsport-black-dynamic-yellow/

            //Nike React Element 87 'Light Orange'
            //https://www.nike.com/launch/t/reach-element-87-light-orewood-brown-volt-glow-cool-grey/

            //Nike Air Zoom Pegasus 35 Mens 'Camo'
            //https://www.nike.com/t/air-zoom-pegasus-35-mens-camo-running-shoe-cV5zsb

            //var url = "https://www.nike.com/launch/t/air-max-720-satrn-motorsport-black-dynamic-yellow/";
            var url = "https://www.nike.com/launch/t/reach-element-87-light-orewood-brown-volt-glow-cool-grey/";
            _nikePurchaseService.LaunchPurchase(url);

            //url = "https://www.nike.com/t/air-zoom-pegasus-35-mens-camo-running-shoe-cV5zsb";
            _nikePurchaseService.RegularPurchase(url);
        }
    }
}
