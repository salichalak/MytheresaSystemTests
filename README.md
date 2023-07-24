# MytheresaSystemTests

### Overview
Test automation using Playwright and C# for automating the following scenarios:
- Check for console errors on the home page
- Check for broken links on the home page
- Login with valid credentials and bypassing recaptcha using cookie
- Fetching pull request of a Github repo

### Installation
There are few key steps to execute the test and get the results. 
```
git clone
```
```
dotnet build
```
### Execution
To execute all tests execute the following command:
```
dotnet test
```
Running a single test file

```
dotnet test --filter "MyClassName" 
```

### Configuration
The test environment,the browser and the channel can be configred from the following config file: <b>runsettings.json</b>

If BaseUrl is empty the default option is to fallback to PROD environment.

```
{
  "BrowserType": "chromium",
  "BrowserChannel": "chrome",
  "BaseUrl": "https://www.mytheresa.com",
  "IsHeadless": false,
  "SlowMotion": 0,
  "TimeOut": 7000
}
```

The available test environments can be found in <b>App.config</b>
```
	<appSettings>
		<add key="ProdUrl" value="https://www.mytheresa.com"/>
		<add key="LocalUrl" value="https://local.mytheresa.com"/>
		<add key="StagingUrl" value="https://staging.mytheresa.com"/>
		<add key="TestUrl" value="https://test.mytheresa.com"/>
	</appSettings>
```