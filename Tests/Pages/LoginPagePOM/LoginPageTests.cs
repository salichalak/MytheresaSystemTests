using Microsoft.Playwright;
using MytheresaSystemTests.Configuration;
using MytheresaSystemTests.Core;
using MytheresaSystemTests.Tests.HomePagePOM;

namespace MytheresaSystemTests.Tests.Pages.LoginPagePOM
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class LoginPageTests : Driver
    {
        private static readonly TestSettings _testSettings = TestSettingsReader.ReadSettings();

        public LoginPageTests() : base(_testSettings)
        {
        }

        [Test]
        public async Task LoginWithValidCredentials()
        {
            Cookie disableRecaptchaCookie = new Cookie
            {
                Name = "mt_csf",
                Value = "15340000",
                Domain = _testSettings.BaseUrl,
                Path = "/",
            };

            var page = await Page;
            LoginPage loginPage = new LoginPage(page);
            HomePage homePage = new HomePage(page);
            var url = $"{_testSettings.BaseUrl}/de/en/men";

            await page.GotoAsync(url);
            await page.EvaluateAsync(@"() => {
    document.cookie = 'mt_csf=15340000; expires=Fri, 31 Dec 9999 23:59:59 GMT; domain=.mytheresa.com';
}");
            await page.Context.AddCookiesAsync(new[] { disableRecaptchaCookie });
            await page.FrameLocator("iframe#privacy-iframe").
                GetByRole(AriaRole.Button, new() { Name = "Accept all and continue" }).ClickAsync();

            await homePage.LoginIcon.ClickAsync();
            await loginPage.Login();

            loginPage.AssertLoggedInUrl(page.Url);
            loginPage.AssertUserAccountActionsAreDisplaed();
            loginPage.AssertAccountOverviewHeaderIsCorrect();
        }
    }
}
