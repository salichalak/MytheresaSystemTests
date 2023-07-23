using Microsoft.Playwright;
using MytheresaSystemTests.Configuration;
using MytheresaSystemTests.Core;

namespace MytheresaSystemTests.Tests.Pages.Github
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class GithubTests : Driver
    {
        private static readonly TestSettings _testSettings = TestSettingsReader.ReadSettings();
        private const string REPO_URL = "https://github.com/appwrite/appwrite/pulls";
        private const string API_TOKEN = "";
        private const string BASE_URL = "https://api.github.com";


        public GithubTests() : base(_testSettings)
        {
        }

        [Test]
        public async Task VerifyRepoPullRequest()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("Accept", "application/vnd.github.v3+json");
            headers.Add("Authorization", "token " + API_TOKEN);

            var response = await this.Context.Result.APIRequest.FetchAsync(BASE_URL,
                new APIRequestContextOptions { Headers = headers });
            var responseBody = response.JsonAsync();

            Assert.IsTrue(response.Ok, "The request to the repository was not successful.");
            Assert.IsTrue(response.Ok, "Missing response body.");
            Console.WriteLine(responseBody);

            await response.DisposeAsync();
        }
    }
}
