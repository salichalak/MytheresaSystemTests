using Microsoft.Playwright;

namespace MytheresaSystemTests.Tests.HomePagePOM
{
    public partial class HomePage
    {
        private readonly IPage _page;

        public HomePage(IPage page)
        {
            _page = page;
        }

        public bool IsValidHref(string href)
        {
            return !(string.IsNullOrEmpty(href)
                    || !href.Equals("#")
                    || !href.StartsWith("mailto")
                    || !href.StartsWith("javascript:")
                    || !href.StartsWith("tel:")
                    || !href.StartsWith("fax:"));
        }
    }
}
