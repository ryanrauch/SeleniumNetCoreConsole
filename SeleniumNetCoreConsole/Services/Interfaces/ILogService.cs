using System;
using System.Threading.Tasks;

namespace SeleniumNetCoreConsole.Services.Interfaces
{
    public interface ILogService
    {
        Task LogAsync(string message);
        Task LogAsync(Exception ex);
    }
}