using System;
using System.Threading.Tasks;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole.Services
{
    public class LocalFileLogService : ILogService
    {
        public async Task WriteLineAsync(string message)
        {
            await LogAsync(message + "\n");
        }

        public async Task LogAsync(string message)
        {
            throw new NotImplementedException();
        }

        public async Task LogAsync(Exception ex)
        {
            await LogAsync(ex.Message);
        }
    }
}
