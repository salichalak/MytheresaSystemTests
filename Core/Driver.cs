using Microsoft.Playwright;
using MytheresaSystemTests.Configuration;

namespace MytheresaSystemTests.Core
{
    public class Driver : IDisposable
    {
        private readonly TestSettings _settings;
        private readonly Task<IPage> _page;
        private readonly Task<IBrowser> _browser;
        private readonly Task<IBrowserContext> _context;

        public Driver(TestSettings testSettings)
        {
            _settings = testSettings;
            _browser = Task.Run(GetDriver);
            _context = Task.Run(CreateBrowserContext);
            _page = Task.Run(CreatePageAsync);
        }

        public Task<IPage> Page => _page;

        public Task<IBrowser> Browser => _browser;

        public Task<IBrowserContext> Context => _context;

        private async Task<IBrowser> GetDriver()
        {
            return await DriverFactory.CreateDriver(_settings);
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browser).NewPageAsync();
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await _browser).NewContextAsync();
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
