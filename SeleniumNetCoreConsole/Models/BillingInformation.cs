using System;
namespace SeleniumNetCoreConsole.Models
{
    public class BillingInformation
    {
        public ShippingInformation ShippingInfo { get; set; }
        public string CreditCardNumber { get; set; }
        public string CVV2 { get; set; }
        public string ExpirationDate { get; set; }
    }
}
