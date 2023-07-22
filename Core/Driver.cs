using Microsoft.Playwright;
using MytheresaSystemTests.Configuration;

namespace MytheresaSystemTests.Core
{
    public class Driver : IDisposable
    {
        private readonly TestSettings _settings;
        private readonly Lazy<Task<IPage>> _page;
        private readonly Lazy<Task<IBrowser>> _browser;
        private readonly Lazy<Task<IBrowserContext>> _context;

        public Driver(TestSettings testSettings)
        {
            _settings = testSettings;
            _page = new Lazy<Task<IPage>>(CreatePageAsync);
            _browser = new Lazy<Task<IBrowser>>(GetDriver);
            _context = new Lazy<Task<IBrowserContext>>(CreateBrowserContext);
        }

        public Task<IPage> Page => _page.Value;

        public Task<IBrowser> Browser => _browser.Value;

        public Task<IBrowserContext> Context => _context.Value;

        private async Task<IBrowser> GetDriver()
        {
            return await DriverFactory.CreateDriver(_settings);
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browser.Value).NewPageAsync();
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await _browser.Value).NewContextAsync();
        }

        public void Dispose()
        {
            Task.Run(async () =>
            {
                await (await Browser).CloseAsync();
                await (await Browser).DisposeAsync();
            });
        }
    }
}
