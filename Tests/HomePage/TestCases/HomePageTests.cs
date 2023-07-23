using Microsoft.Playwright;
using MytheresaSystemTests.Configuration;
using MytheresaSystemTests.Core;

namespace MytheresaSystemTests.Tests.HomePage.TestCases
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
            var page = await this.Page;
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

            Assert.That(errorsCount, Is.EqualTo(0), $"{errorsCount} unexpected console errors are displayed on the page.");
        }

        [Test]
        public async Task CheckForBrokenLinks()
        {
            var page = await this.Page;
            var url = $"{_testSettings.BaseUrl}/de/en/men";
            await page.GotoAsync(url);

            var anchors = await page.Locator("//a[@href]").AllAsync();
            HashSet<string> brokenLinks = new HashSet<string>();

            foreach (var anchor in anchors.ToList())
            {
                var href = await anchor.GetAttributeAsync("href");

                if (string.IsNullOrEmpty(href)
                    || href.Equals("#")
                    || href.StartsWith("mailto")
                    || href.StartsWith("javascript:")
                    || href.StartsWith("tel:")
                    || href.StartsWith("fax:"))
                {
                    continue;
                }

                var currentUrl = href.StartsWith("http")
                     ? href
                     : $"{_testSettings.BaseUrl}{href}";

                var isSuccessful = await GetIsSuccessStatusCode(currentUrl);

                if (!isSuccessful)
                {
                    brokenLinks.Add(currentUrl);
                }
            }

            Assert.That(brokenLinks.Count, Is.EqualTo(0), "Links that does not return success code are found on the page.");
        }

        private async Task<bool> GetIsSuccessStatusCode(string url)
        {
            using var client = new HttpClient();
            var result = await client.GetAsync(url);

            return result.IsSuccessStatusCode;
        }
    }
}
