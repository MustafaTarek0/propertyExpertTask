using Microsoft.Playwright;


namespace PropertyExpertTask.screens;

public class InventoryScreen : ScreenBase
{
    public InventoryScreen(IPage page) : base(page) { }
    private ILocator LogOut => _page.Locator("#logout_sidebar_link");
    private ILocator SideMenuButton => _page.Locator("#react-burger-menu-btn");
    private ILocator CartButton => _page.Locator("data-test=shopping-cart-link");
    
    private ILocator InventoryItem => _page.Locator("div.inventory_item");
    private ILocator InventoryItemName =>  _page.Locator("div[data-test='inventory-item-name']");
    private ILocator InventoryItemDes =>  _page.Locator("#checkout_summary_container .cart_list .cart_item .cart_item_label .inventory_item_desc");
    private ILocator InventoryItemPrice => _page.Locator("#checkout_summary_container .cart_list .cart_item .cart_item_label .item_pricebar > div");

    public Task ClickLogOutButton()=> LogOut.ClickAsync();
    public Task ClickCartButton()=> CartButton.ClickAsync();
    public Task ClickOnSideMenuButton()=> SideMenuButton.ClickAsync();
   

    public ILocator GetInventoryItem()
    {
        return InventoryItem;
    }
    public ILocator GetInventoryItemName()
    {
        return InventoryItemName;
    }
    public ILocator GetInventoryItemDes()
    {
        return InventoryItemDes;
    }
    public ILocator GetInventoryItemPrice()
    {
        return InventoryItemPrice;
    }
}