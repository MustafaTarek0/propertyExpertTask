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
    private ILocator SuccessSign => _page.Locator("data-test=pony-express");

    private ILocator Title => _page.Locator("data-test=title");
    private ILocator BackToProductsButton => _page.Locator("#back-to-products");

    public Task ClickCheckOutButton()=> CheckOutButton.ClickAsync();
    public Task ClickFinishButton()=> FinishButton.ClickAsync();
    public Task ClickOnContinueButton()=> ClickOnCheckOutContinueButton.ClickAsync();
    public ILocator GetSuccessMsg => successMsg;
    public ILocator GetSuccessMark => SuccessSign;
    public ILocator GetCompleteTxt => CompleteText;
    public ILocator GetTitle => Title;

    public async Task FillCheckOutInfo(string firstName, string lastName , string postalCode)
    {
        await FirstName.FillAsync(firstName);
        await LastName.FillAsync(lastName);
        await PostalCode.FillAsync(postalCode);

        
    }
    public Task ClickBackToProductsButton()=> BackToProductsButton.ClickAsync();


}