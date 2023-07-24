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
The test environment and the browser can be configred from the following config file: runsettings.json