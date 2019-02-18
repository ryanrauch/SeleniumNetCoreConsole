using System;
using System.Collections.Generic;
using SeleniumNetCoreConsole.Models;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole.Services
{
    public class HardCodedDesiredShoeService : IDesiredShoeService
    {
        public List<DesiredShoe> GetDesiredShoes()
        {
            return new List<DesiredShoe>() { GetUpcomingDesiredShoe() };
        }

        public DesiredShoe GetUpcomingDesiredShoe()
        {
            DateTime releaseTime = new DateTime(2019, 02, 18, 09, 00, 00);
            Int32 pollBeforeMiliseconds = 20000;
            Int32 pollAfterMiliseconds = 60000;

            return new DesiredShoe()
            {
                URL = "https://www.nike.com/launch/t/jordan-why-not-zero-2-camo-green-volt-infrared/",
                ShoeName = "WHY NOT ZER0.2",
                TargetSize = "11.5",
                ReleaseDate = releaseTime,
                PollingStart = releaseTime.Subtract(TimeSpan.FromMilliseconds(pollBeforeMiliseconds)),
                PollingEnd = releaseTime.AddMilliseconds(pollAfterMiliseconds),
                RefreshInterval = 3000,
                MaxRefreshCount = 0,
                Purchased = false,
                Enabled = true
            };
        }
    }
}
