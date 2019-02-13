using System.Threading.Tasks;
using SeleniumNetCoreConsole.Models;

namespace SeleniumNetCoreConsole.Services.Interfaces
{
    public interface INikePurchaseService
    {
        Task<bool> LaunchPurchase(DesiredShoe shoe);
        Task<bool> RegularPurchase(DesiredShoe shoe);
    }
}