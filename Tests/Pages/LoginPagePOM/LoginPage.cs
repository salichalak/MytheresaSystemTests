using Microsoft.Playwright;
using MytheresaSystemTests.TestData;

namespace MytheresaSystemTests.Tests.Pages.LoginPagePOM
{
    public partial class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task Login()
        {
            await EmailAddressInput.FillAsync(UsersTestData.DummyUserEmail);
            await PasswordInput.FillAsync(UsersTestData.DummyUserPass);
            await LoginButton.ClickAsync();
        }
    }
}
