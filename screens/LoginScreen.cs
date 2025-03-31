using Microsoft.Playwright;


namespace PropertyExpertTask.screens;

public class LoginScreen : ScreenBase
{
    public LoginScreen(IPage page) : base(page) { }
    private ILocator Username => _page.Locator("#user-name");
    private ILocator Password => _page.Locator("#password");
    private ILocator LoginButton => _page.Locator("#login-button");
    private ILocator ErrorMsg => _page.Locator("data-test=error");
    private ILocator ErrorMsgContainer => _page.Locator("#login_button_container > div > form > div.error-message-container.error");

    
    
    public Task ClickLogin()=> LoginButton.ClickAsync();

    public async Task Login(string username, string password)
    {
        await Username.FillAsync(username);
        await Password.FillAsync(password);
        
    }
    public async Task<string> GetErrorMessageTextAsync()
    {
        // Wait for the error message to be visible
        await ErrorMsg.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        // Fetch and return the error message text
        return await ErrorMsg.TextContentAsync();
    }
    public async Task<string> GetErrorMessageColorAsync()
    {
        return await GetElementBackgroundColorAsync(ErrorMsgContainer);
    }
    
}