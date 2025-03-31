using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using PropertyExpertTask.screens;

namespace PropertyExpertTask.Tests
{
    [TestFixture("Chromium")]
    [TestFixture("Firefox")]
    [TestFixture("Webkit")]
    [Parallelizable(ParallelScope.All)]
    public class LogoutTests : TestBase
    {
        public LogoutTests(string browserName) : base(browserName) { }

        [Test] [Parallelizable(ParallelScope.Self)]
        public async Task logout()
        {
            // Loop through each login data set from the JSON file
            foreach (var data in _jsonData.loginData)
            {
                var inventoryScreen = new InventoryScreen(_page);
                _commonSteps.Login(data.username.ToString(), data.password.ToString());
                await inventoryScreen.ClickOnSideMenuButton();
                await inventoryScreen.ClickLogOutButton();
                await Assertions.Expect(_page).Not.ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
                await Assertions.Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/");


            }
            
            
        }
    }
}
