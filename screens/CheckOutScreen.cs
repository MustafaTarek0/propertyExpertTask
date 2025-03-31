using Microsoft.Playwright;


namespace PropertyExpertTask.screens;

public class CheckOutScreen : ScreenBase
{
    public CheckOutScreen(IPage page) : base(page) { }
    private ILocator CheckOutButton => _page.Locator("#checkout");
    private ILocator FirstName => _page.Locator("#first-name");
    private ILocator LastName => _page.Locator("#last-name");
    private ILocator PostalCode => _page.Locator("#postal-code");
    private ILocator FinishButton => _page.Locator("#finish");
    private ILocator successMsg => _page.Locator("data-test=complete-header");

    private ILocator CompleteText => _page.Locator("data-test=complete-text");

    private ILocator ClickOnCheckOutContinueButton => _page.Locator("#continue");
    
    public Task ClickCheckOutButton()=> CheckOutButton.ClickAsync();
    public Task ClickFinishButton()=> FinishButton.ClickAsync();
    public Task ClickOnContinueButton()=> ClickOnCheckOutContinueButton.ClickAsync();
public ILocator SuccessMsg => successMsg;

public ILocator CompleteTxt => CompleteText;

    public async Task FillCheckOutInfo(string firstName, string lastName , string postalCode)
    {
        await FirstName.FillAsync(firstName);
        await LastName.FillAsync(lastName);
        await PostalCode.FillAsync(postalCode);

        
    }
   

}