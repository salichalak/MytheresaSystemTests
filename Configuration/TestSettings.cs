namespace MytheresaSystemTests.Configuration
{
    public class TestSettings
    {
        public string BrowserType { get; set; }

        public string BrowserChannel { get; set; }

        public string BaseUrl { get; set; } = "https://www.mytheresa.com";

        public bool IsHeadless { get; set; }

        public float SlowMotion { get; set; }

        public string[] Args { get; set; }

        public int TimeOut { get; set; } = 10000;
    }
}
