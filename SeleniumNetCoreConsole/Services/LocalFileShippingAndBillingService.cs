using System;
using SeleniumNetCoreConsole.Models;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole.Services
{
    public class LocalFileShippingAndBillingService : IShippingAndBillingService
    {
        public BillingInformation GetBillingInformation()
        {
            throw new NotImplementedException();
        }

        public ShippingInformation GetShippingInformation()
        {
            throw new NotImplementedException();
        }
    }
}
