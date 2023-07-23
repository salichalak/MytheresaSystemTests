using Microsoft.Playwright;

namespace MytheresaSystemTests.Tests.HomePagePOM
{
    public partial class HomePage
    {
        public async Task<IReadOnlyList<ILocator>> GetAllAnchors()
        {
            return await _page.Locator("//a[@href]").AllAsync();
        }

        public ILocator LoginIcon => _page.Locator("//div[@class = 'useractions']/a[contains(@href, '/login')]");
    }
}
