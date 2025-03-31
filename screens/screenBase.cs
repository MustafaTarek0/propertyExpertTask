
using Microsoft.Playwright;

namespace PropertyExpertTask.screens
{
    public class ScreenBase
    {
        protected IPage _page;
        
        // Constructor to initialize the page
        public ScreenBase(IPage page)
        {
            _page = page;
        }

        public async Task<string> GetElementBackgroundColorAsync(ILocator element)
        {
            return await element.EvaluateAsync<string>("el => window.getComputedStyle(el).backgroundColor");
        }
    }
    }

