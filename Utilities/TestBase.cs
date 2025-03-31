using Microsoft.Playwright;
using NUnit.Framework;
using Newtonsoft.Json;
using PropertyExpertTask.Utilities;

namespace PropertyExpertTask.Tests
{
    public class TestBase
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IBrowserContext _context;
        protected IPage _page;
        protected AppConfig _config;
        protected dynamic _jsonData;
        public CommonSteps _commonSteps;
        private readonly string _browserName;
        private string _videoPath;
        private string _screenshotPath;
        
        public TestBase(string browserName)
        {
            _browserName = browserName;
        }

        [SetUp]
        public async Task Setup()
        {
            // Load configuration and test data
            _config = LoadJson<AppConfig>("Config/testConfig.json");
            _jsonData = LoadJson<dynamic>("DataDriven/testData.json");

            _playwright = await Playwright.CreateAsync();

            var browserSetting = _config.Browsers.FirstOrDefault(b => b.Name == _browserName);
            if (browserSetting == null)
            {
                throw new InvalidOperationException($"Unsupported browser: {_browserName}");
            }

            // Configure video recording options
            BrowserNewContextOptions contextOptions = new()
            {
                RecordVideoDir = _config.Video.Enabled ? _config.Video.Path : null
            };

            _browser = browserSetting.Name switch
            {
                "Chromium" => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = browserSetting.Headless }),
                "Firefox" => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = browserSetting.Headless }),
                "Webkit" => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = browserSetting.Headless }),
                _ => throw new InvalidOperationException($"Unsupported browser: {_browserName}")
            };

            _context = await _browser.NewContextAsync(contextOptions);
            _page = await _context.NewPageAsync();
            _commonSteps = new CommonSteps(_page);

            await _page.GotoAsync(_config.BaseUrl);
        }

        [TearDown]
        public async Task TearDown()
        {
            // Check if the test failed
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                await CaptureScreenshot();
                await SaveVideo();
            }

            await _context.CloseAsync();
            await _browser.CloseAsync();
        }

        private async Task CaptureScreenshot()
        {
            if (_config.Screenshots.Enabled && _config.Screenshots.OnFailure)
            {
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string screenshotDir = Path.Combine(projectRoot, _config.Screenshots.Path);
        
                // Ensure directory exists
                Directory.CreateDirectory(screenshotDir);

                var screenshotPath = Path.Combine(screenshotDir, $"{TestContext.CurrentContext.Test.Name}.png");
                await _page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
                TestContext.AddTestAttachment(screenshotPath, "Screenshot on failure");
                Console.WriteLine($"Screenshot saved: {screenshotPath}");
            }
        }

        private async Task SaveVideo()
        {
            if (_config.Video.Enabled && _config.Video.OnFailure)
            {
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string videoDir = Path.Combine(projectRoot, _config.Video.Path);

                // Ensure directory exists
                Directory.CreateDirectory(videoDir);

                var videoFilePath = await _page.Video.PathAsync();
                var testVideoPath = Path.Combine(videoDir, $"{TestContext.CurrentContext.Test.Name}.webm");

                File.Move(videoFilePath, testVideoPath);
                TestContext.AddTestAttachment(testVideoPath, "Test Failure Video");
                Console.WriteLine($"Video recorded: {testVideoPath}");
            }
        }


        private T LoadJson<T>(string relativeFilePath)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.Combine(baseDir, "..", "..", ".."); // Moves up to the project root

            string fullPath = Path.Combine(projectRoot, relativeFilePath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File not found at {fullPath}");
            }

            var jsonData = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

    }
}
