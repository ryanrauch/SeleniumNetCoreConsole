using System;
using OpenQA.Selenium;
using SeleniumNetCoreConsole.Models;
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

        public async System.Threading.Tasks.Task RunAsync(string[] args)
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

            //url = "https://www.nike.com/launch/t/adapt-bb-black-white-pure-platinum/";

            DateTime releaseTime = new DateTime(2019, 02, 14, 09, 00, 00);
            DesiredShoe shoe = new DesiredShoe()
            {
                URL = url,
                ShoeName = "Reach Element 87",
                TargetSize = "11.5",
                ReleaseDate = releaseTime,
                PollingStartDate = releaseTime.AddMinutes(-1),
                PollingEndDate = releaseTime.AddMinutes(4),
                RefreshInterval = 1000,
                MaxRefreshCount = 0
            };

            bool bought = await _nikePurchaseService.LaunchPurchase(shoe);

            //url = "https://www.nike.com/t/air-zoom-pegasus-35-mens-camo-running-shoe-cV5zsb";
            await _nikePurchaseService.RegularPurchase(shoe);
        }
    }
}
