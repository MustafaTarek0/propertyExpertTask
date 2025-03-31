# saucedemo propertyExpert

Property Task using playwright C#
## Setup Playwright


dotnet add package Microsoft.Playwright
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package MySql.Data

 ```

## Run tests


You can run with different browsers by changing the configurations in config file

```console
    dotnet test 


```

For run only some test, for example run a specific testCase

```console
    dotnet test --filter "Checkoutests"
    dotnet test --filter "LoginTests"
    dotnet test --filter "LogoutTests"

```

BASE_ADDRESS : https://www.saucedemo.com/
BROWSER_TYPE : Chromium or Firefox or Webkit



## Run tests in parallel

For run test in parallel you need to add the attribute [Parallelizable] to the Test class

You can setup the number or workers

```console
    dotnet test -- NUnit.NumberOfTestWorkers=2
```
``` generate Test Reports
dotnet test --logger "trx;LogFileName=TestResults.trx"
```
### Project Structure

page object model is applied

Config file for test configrations to utilzie it in Testbase in order to

Data package to add screenshots and video recording

DataDriver to add my testing data in json file to read from it

helpers to add database connection class and database assertions and queries

Screens cotians screenBase as a perant class and other screen classes inhiret from it  to initialize the page and common methods for screens

Tests cotains test cases for each flow in the task

Utlites cotains common steps like login and Test base is the parent class for Testcases classes where im intiziting the step up and tear down of the project and also the methods of reading from json and screenshots , video recording and multiple browsers exection



### installed packages

Top-level Package                     Requested   Resolved
> Microsoft.Data.SqlClient            6.0.1       6.0.1 
> 
> Microsoft.NET.Test.Sdk              17.13.0     17.13.0
> 
> Microsoft.Playwright                1.51.0      1.51.0
> 
> Microsoft.Playwright.NUnit          1.51.0      1.51.0
> 
> more.xunit.runner.visualstudio      2.3.1       2.3.1
> 
> MySql.Data                          9.2.0       9.2.0
> 
> Newtonsoft.Json                     13.0.3      13.0.3
> 
> NugetPackageManager                 1.0.0       1.0.0
> 
> NUnit                               4.3.2       4.3.2
> 
> NUnit3TestAdapter                   5.0.0       5.0.0
> 
> xunit.runner.visualstudio           3.0.2       3.0.2

