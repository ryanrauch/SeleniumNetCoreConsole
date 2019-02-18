using System.Threading.Tasks;
using SeleniumNetCoreConsole.Models;

namespace SeleniumNetCoreConsole.Services.Interfaces
{
    public interface INikePurchaseService
    {
        bool LaunchPurchase(DesiredShoe shoe);
        bool RegularPurchase(DesiredShoe shoe);
    }
}