using System;
using SeleniumNetCoreConsole.Models;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole.Services
{
    public class SecretsFileShippingAndBillingService : IShippingAndBillingService
    {
        public BillingInformation GetBillingInformation()
        {
            return new BillingInformation()
            {
                CreditCardNumber = Secrets.CreditCardNumber,
                CVV2 = Secrets.CVNumber,
                ExpirationDate = Secrets.ExpDate,
                ShippingInfo = GetShippingInformation()
            };
        }

        public ShippingInformation GetShippingInformation()
        {
            return new ShippingInformation()
            {
                FirstName = Secrets.FirstName,
                LastName = Secrets.LastName,
                Address1 = Secrets.Address1,
                City = Secrets.City,
                State = Secrets.State,
                ZipCode = Secrets.ZipCode,
                Email = Secrets.Email,
                PhoneNumber = Secrets.PhoneNumber
            };
        }
    }
}
