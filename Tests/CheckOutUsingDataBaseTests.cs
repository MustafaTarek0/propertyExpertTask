using Microsoft.Playwright;
using NUnit.Framework;
using PropertyExpertTask.screens;
using PropertyExpertTask.Utilities;

namespace PropertyExpertTask.Tests
{
    [TestFixture("Chromium")]
    [TestFixture("Firefox")]
    [TestFixture("Webkit")]
    [Parallelizable(ParallelScope.All)]
    public class CheckOutUsingDataBaseTests : TestBase
    {
        
        public CheckOutUsingDataBaseTests(string browserName) : base(browserName) { }
        private string connectionString = "Server=localhost;Database=mustafa;User=root;Password=1234;"; // my local connection string

        [Test]
        [Parallelizable(ParallelScope.Self)]
        
        public async Task CheckOutFlowUsingDataBase()
        {
            
            var inventoryScreen = new InventoryScreen(_page);
            var checkoutScreen = new CheckOutScreen(_page);

            // Loop through each login data set from the JSON file
            foreach (var data in _jsonData.loginData)
            {
           
         
                // Perform login using data from JSON
                await _commonSteps.Login(data.username.ToString(), data.password.ToString());
                await Task.Delay(3000);
                inventoryScreen.GetInventoryItem().WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                
                // Locate all inventory items using the shared locator
                var inventoryItems = inventoryScreen.GetInventoryItem();
                var itemCount = await inventoryItems.CountAsync(); // Get total number of items
                
                // Set boundary for random selection (0 to 5 since we have 6 items)
                var random = new Random();
                int randomIndex = random.Next(0, Math.Min(itemCount, 6)); // Ensure we stay within bounds
                
                // Select the random inventory item
                var selectedItem = inventoryItems.Nth(randomIndex);
                
                // Fetch the item's name using the correct locator, scoped to the selected item
                var itemName = await selectedItem.Locator(inventoryScreen.GetInventoryItemName()).TextContentAsync();
                
                // Convert the item name into the format used in the button ID
                var formattedItemName = itemName.ToLower().Replace(" ", "-");
                
                // Escape the formatted item name to make it a valid CSS selector
                string escapedItemName = _commonSteps.EscapeCssSelector(formattedItemName);
                
                // Construct the "Add to Cart" button locator dynamically
                string buttonId = $"#add-to-cart-{escapedItemName}";
                
                // Click the "Add to Cart" button for the selected item
                var addToCartButton = _page.Locator(buttonId);
                await addToCartButton.ClickAsync();
                Console.WriteLine($"Added '{itemName}' to the cart.");
                
                inventoryScreen.ClickCartButton();
                await Assertions.Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/cart.html");
                await Task.Delay(2000);
                
                checkoutScreen.ClickCheckOutButton();
                await Assertions.Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/checkout-step-one.html");
                
                checkoutScreen.FillCheckOutInfo(data.firstName.ToString(), data.lastName.ToString(), data.zipCode.ToString());
                await Task.Delay(2000);

                checkoutScreen.ClickOnContinueButton();
                await Assertions.Expect(inventoryScreen.GetInventoryItemDes()).ToBeVisibleAsync();
                await Assertions.Expect(inventoryScreen. GetInventoryItemPrice()).ToBeVisibleAsync();
                await Assertions.Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/checkout-step-two.html");
                checkoutScreen.ClickFinishButton();
                await Assertions.Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/checkout-complete.html");
                
                Assert.That(await checkoutScreen.GetSuccessMsg.TextContentAsync(), Is.EqualTo(data.successMsg.ToString()));
                Assert.That(await checkoutScreen.GetCompleteTxt.TextContentAsync(), Is.EqualTo(data.completeText.ToString()));
                
                // Now, validate the order in the database
                var databaseHelper = new dataBaseAssertion(connectionString);
                
                // Fetch latest order from the database
                (string Username, string ProductName, decimal TotalPrice, DateTime OrderDate) dbOrder =
                    await databaseHelper.GetLatestOrderFromDatabase(data.username.ToString());
                Console.WriteLine(dbOrder);
               // Access the tuple values using the names defined in the method

                decimal dbTotalPrice = dbOrder.TotalPrice; 
                DateTime dbOrderDate = dbOrder.OrderDate; 
                string dbUsername = dbOrder.Username; 
                string dbProductName = dbOrder.ProductName; 


                // Validate that the database values match the checkout details
                Assert.That(dbUsername, Is.EqualTo(data.username.ToString()), "Username does not match!");
                Assert.That(dbProductName, Is.EqualTo(itemName), "Product name does not match!");
                Assert.That(dbTotalPrice, Is.EqualTo(Convert.ToDecimal(data.totalPrice.ToString())), "Total price does not match!");

                Console.WriteLine("Database validation passed");

            }} } }

