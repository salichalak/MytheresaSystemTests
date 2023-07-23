using Microsoft.Playwright;

namespace MytheresaSystemTests.Tests.Pages.LoginPagePOM
{
    public partial class LoginPage
    {
        public ILocator PasswordInput => _page.Locator("div[class='signinform'] input[name='password']");

        public ILocator EmailAddressInput => _page.GetByLabel("Email address *", new() { Exact = true });

        public ILocator LoginButton => _page.Locator("//div[@class = 'signinform__submit']/div");

        public ILocator UserAccountActions => _page.Locator("ul.sidebar__nav__items");

        public ILocator AccountOverviewHeader => _page.Locator("div.overview__title");
    }
}
