using System.Collections.Generic;
using SeleniumNetCoreConsole.Models;

namespace SeleniumNetCoreConsole.Services.Interfaces
{
    public interface IDesiredShoeService
    {
        List<DesiredShoe> GetDesiredShoes();
        DesiredShoe GetUpcomingDesiredShoe();
    }
}