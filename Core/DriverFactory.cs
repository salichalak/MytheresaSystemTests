using Microsoft.Playwright;
using MytheresaSystemTests.Configuration;

namespace MytheresaSystemTests.Core
{
    public static class DriverFactory
    {
        public static async Task<IBrowser> CreateDriver(TestSettings settings)
        {
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Args = settings.Args,
                Timeout = settings.TimeOut,
                Headless = settings.IsHeadless,
                SlowMo = settings.SlowMotion,
                Channel = settings.BrowserChannel,
            };

            var playwright = await Playwright.CreateAsync();
            var driver = await playwright[settings.BrowserType].LaunchAsync(launchOptions);

            return driver;
        }
    }
}
