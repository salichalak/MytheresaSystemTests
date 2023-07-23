using Microsoft.Playwright;
using MytheresaSystemTests.Configuration;
using MytheresaSystemTests.Core;
using MytheresaSystemTests.Tests.HomePagePOM;

namespace MytheresaSystemTests.Tests.Pages.HomePagePOM
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class HomePageTests : Driver
    {
        private static readonly TestSettings _testSettings = TestSettingsReader.ReadSettings();

        public HomePageTests() : base(_testSettings) { }

        [Test]
        public async Task CheckForConsoleErrors()
        {
            var page = await Page;
            HomePage homePage = new HomePage(page);
            var url = $"{_testSettings.BaseUrl}/de/en/men";
            await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

            List<IConsoleMessage> consoleErrors = new List<IConsoleMessage>();
            page.Console += (_, msg) =>
              {
                  if (msg.Type == "error")
                  {
                      consoleErrors.Add(msg);
                  }
              };

            await page.WaitForConsoleMessageAsync(new PageWaitForConsoleMessageOptions { Timeout = 10000 });
            int errorsCount = consoleErrors.Count;
            homePage.AssertNoConsoleErrorsAreDisplayed(errorsCount);
        }

        [Test]
        public async Task CheckForBrokenLinks()
        {
            var page = await Page;
            HomePage homePage = new HomePage(page);
            var url = $"{_testSettings.BaseUrl}/de/en/men";
            await page.GotoAsync(url);

            var anchors = await homePage.GetAllAnchors();
            HashSet<string> brokenLinks = new HashSet<string>();

            foreach (var anchor in anchors.ToList())
            {
                var href = await anchor.GetAttributeAsync("href");

                if (homePage.IsValidHref(href))
                {
                    continue;
                }

                var currentUrl = href.StartsWith("http")
                     ? href
                     : $"{_testSettings.BaseUrl}{href}";

                var response = await GetPageResponse(currentUrl);
                if (!response.IsSuccessStatusCode)
                {
                    brokenLinks.Add(currentUrl);
                }
            }

            Assert.That(brokenLinks.Count, Is.EqualTo(0), "Links that does not return success code are found on the page.");
        }

        private async Task<HttpResponseMessage> GetPageResponse(string pageUrl)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(pageUrl);

            return response;
        }
    }
}
