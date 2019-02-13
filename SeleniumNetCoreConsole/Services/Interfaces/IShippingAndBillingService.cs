using SeleniumNetCoreConsole.Models;

namespace SeleniumNetCoreConsole.Services.Interfaces
{
    public interface IShippingAndBillingService
    {
        BillingInformation GetBillingInformation();
        ShippingInformation GetShippingInformation();
    }
}