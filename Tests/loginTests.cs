using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Allure.Core;
using NUnit.Framework;
using PropertyExpertTask.screens;
using PropertyExpertTask.Utilities;

namespace PropertyExpertTask.Tests
{
    [TestFixture("Chromium")]
    [TestFixture("Firefox")]
    [TestFixture("Webkit")]
    [Parallelizable(ParallelScope.All)]
    public class LoginTests : TestBase
    { 
        
        public LoginTests(string browserName) : base(browserName) { }
        
        [Test]
        [Parallelizable(ParallelScope.Self)]
        public async Task Login_WithValidCredentials_ShouldRedirectToInventoryPage()
        { 
            // Loop through each login data set from the JSON file
            foreach (var data in _jsonData.loginData)
            {
                // Perform login using data from JSON
                _commonSteps.Login(data.username.ToString(), data.password.ToString());
               
            }
            
            
        }
        [Test]
        [Parallelizable(ParallelScope.Self)]
        public async Task LoginWithInvalidCredentials()
        {
            // Loop through each login data set from the JSON file
            foreach (var data in _jsonData.loginData)
            {
                var loginScreen = new LoginScreen(_page);
                // Perform login using data from JSON
                _commonSteps.Login(data.invalidUserName.ToString(), data.invalidPassword.ToString());
                ILocator ErrorMsg = _page.Locator("data-test=error");
                Assert.That(await loginScreen.GetErrorMessageTextAsync(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
                string actualColor = await loginScreen.GetErrorMessageColorAsync();
                Assert.That(actualColor, Is.EqualTo("rgb(226, 35, 26)"), "Error message color should be red");

                
            }
            
            
        }
    }
}
