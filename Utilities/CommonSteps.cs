using Microsoft.Playwright;
using System.Threading.Tasks;
using PropertyExpertTask.screens;

namespace PropertyExpertTask.Utilities
{
    public class CommonSteps
    {
        private readonly IPage _page;

        // Inject IPage into the class via constructor
        public CommonSteps(IPage page)
        {
            _page = page;
        }
        public async Task Login(string username, string password)
        {
            var loginScreen = new LoginScreen(_page);
            // Perform login using data from JSON
            await loginScreen.Login(username, password);
            await loginScreen.ClickLogin();
            await Assertions.Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
            
            
        }
        public string EscapeCssSelector(string selector)
        {
            return string.Concat(selector.Select(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' ? c.ToString() : "\\" + c));
        }
    }
}