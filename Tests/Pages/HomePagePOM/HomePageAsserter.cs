namespace MytheresaSystemTests.Tests.HomePagePOM
{
    public partial class HomePage
    {
        public void AssertNoConsoleErrorsAreDisplayed(int count)
        {
            Assert.That(count, Is.EqualTo(0), $"{count} unexpected console errors are displayed on the page.");
        }
    }
}
