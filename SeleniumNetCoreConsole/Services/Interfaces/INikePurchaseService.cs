using System.Threading.Tasks;

namespace SeleniumNetCoreConsole.Services.Interfaces
{
    public interface INikePurchaseService
    {
        Task<bool> LaunchPurchase(string url);
        Task<bool> RegularPurchase(string url);
    }
}