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
        private readonly IDesiredShoeService _desiredShoeService;
        private readonly ILogService _logService;

        public ApplicationLogic(
            IWebDriver webDriver,
            INikePurchaseService nikePurchaseService,
            IDesiredShoeService desiredShoeService,
            ILogService logService
        )
        {
            _webDriver = webDriver;
            _nikePurchaseService = nikePurchaseService;
            _desiredShoeService = desiredShoeService;
            _logService = logService;
        }

        public void Run(string[] args)
        {
            DesiredShoe shoe = _desiredShoeService.GetUpcomingDesiredShoe();
            _logService.LogAsync("Target Shoe: " + shoe.URL);

        }

        private void WaitForReleaseDate(DesiredShoe shoe)
        {
            while (DateTime.Now < shoe.PollingStart)
            {
                System.Threading.Thread.Sleep(shoe.RefreshInterval);
            }
        }

        public void RunFirstAttempt(string[] args)
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
            //var url = "https://www.nike.com/launch/t/reach-element-87-light-orewood-brown-volt-glow-cool-grey/";
            //var url = "https://www.nike.com/launch/t/air-jordan-33-travis-scott-army-olive-black-ale-brown/";
            var url = "https://www.nike.com/launch/t/pg3-acg-multi-color/";
            //url = "https://www.nike.com/launch/t/adapt-bb-black-white-pure-platinum/";

            DateTime releaseTime = new DateTime(2019, 02, 15, 09, 00, 00);
            DesiredShoe shoe = new DesiredShoe()
            {
                URL = url,
                ShoeName = "PG3",
                TargetSize = "11.5",
                ReleaseDate = releaseTime,
                PollingStart = releaseTime.Subtract(TimeSpan.FromMilliseconds(20000)),
                PollingEnd = releaseTime.AddMilliseconds(60000),
                RefreshInterval = 3000,
                MaxRefreshCount = 0
            };

            bool bought = false;
            Console.WriteLine("starting wait time");
            while(DateTime.Now < shoe.PollingStart)
            {
                System.Threading.Thread.Sleep(shoe.RefreshInterval);
            }
            Console.WriteLine("ended wait time");

            while (!bought
                   && DateTime.Now < shoe.PollingEnd)
            {
                Console.WriteLine("Polling shoe " + DateTime.Now.ToString());
                try
                {
                    bought = _nikePurchaseService.LaunchPurchase(shoe);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Polling Exception: \n" + ex.Message + "\n" + ex.StackTrace);
                }
                System.Threading.Thread.Sleep(shoe.RefreshInterval);
            }
            Console.WriteLine("after while loop");

            //url = "https://www.nike.com/t/air-zoom-pegasus-35-mens-camo-running-shoe-cV5zsb";
            _nikePurchaseService.RegularPurchase(shoe);
        }
    }
}
