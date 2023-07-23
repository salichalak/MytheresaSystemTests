using System.Reflection;
using System.Text.Json;

namespace MytheresaSystemTests.Configuration
{
    public static class TestSettingsReader
    {
        public static TestSettings ReadSettings()
        {
            var settings = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/runsettings.json");
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<TestSettings>(settings, options);
        }
    }
}
