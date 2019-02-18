using System;
using System.Threading.Tasks;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole.Services
{
    public class ConsoleLogService : ILogService
    {
        public async Task LogAsync(string message)
        {
            Console.WriteLine(message);
        }

        public async Task LogAsync(Exception ex)
        {
            await LogAsync(ex.Message + "\n" + ex.StackTrace);
        }
    }
}
