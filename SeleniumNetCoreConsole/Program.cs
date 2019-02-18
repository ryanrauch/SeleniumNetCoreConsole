using System;
using Autofac;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumNetCoreConsole.Services;
using SeleniumNetCoreConsole.Services.Interfaces;

namespace SeleniumNetCoreConsole
{
    class Program
    {
        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationLogic>().AsSelf();
            builder.RegisterType<NikePurchaseService>().As<INikePurchaseService>().SingleInstance();
            builder.RegisterType<SecretsFileShippingAndBillingService>().As<IShippingAndBillingService>().SingleInstance();
            builder.RegisterType<HardCodedDesiredShoeService>().As<IDesiredShoeService>().SingleInstance();

            //builder.RegisterType<LocalFileLogService>().As<ILogService>().SingleInstance();
            builder.RegisterType<ConsoleLogService>().As<ILogService>().SingleInstance();

            //builder.RegisterType<ChromeDriver>().As<IWebDriver>();
            builder.RegisterType<FirefoxDriver>().As<IWebDriver>();

            return builder.Build();
        }

        static void Main(string[] args)
        {
            var container = ConfigureContainer();
            var application = container.Resolve<ApplicationLogic>();
            application.Run(args);
        }
    }
}
