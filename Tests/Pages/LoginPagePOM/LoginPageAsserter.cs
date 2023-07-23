using MytheresaSystemTests.TestData;

namespace MytheresaSystemTests.Tests.Pages.LoginPagePOM
{
    public partial class LoginPage
    {
        public void AssertLoggedInUrl(string pageUrl)
        {
            Assert.IsTrue(pageUrl.Contains(UsersTestData.LoggedInPartialUrl),
                "The user was not able to log in.");
        }

        public void AssertUserAccountActionsAreDisplaed()
        {
            Assert.IsNotNull(this.UserAccountActions,
                "The sidebar with account actions is not attached to the page.");
        }

        public async void AssertAccountOverviewHeaderIsCorrect()
        {
            var actualContent = await this.AccountOverviewHeader.TextContentAsync();
            var expectedContent = $"Welcome {UsersTestData.UserFirstName}";
            Assert.AreEqual(expectedContent, actualContent,
                "The username in the header is not correct.");
        }
    }
}
