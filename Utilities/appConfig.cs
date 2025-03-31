public class AppConfig
{
    public string BaseUrl { get; set; }
    public Timeouts Timeouts { get; set; }
    public VideoConfig Video { get; set; }
    public ScreenshotsConfig Screenshots { get; set; }
    public List<BrowserConfig> Browsers { get; set; } // Add this property for browsers
}

public class Timeouts
{
    public int PageLoad { get; set; }
    public int Element { get; set; }
}

public class VideoConfig
{
    public bool Enabled { get; set; }
    public bool OnFailure { get; set; }
    public string Path { get; set; }
}

public class ScreenshotsConfig
{
    public bool Enabled { get; set; }
    public bool OnFailure { get; set; }
    public string Path { get; set; }
}

public class BrowserConfig
{
    public string Name { get; set; } // Browser name, e.g., "Chromium", "Firefox", "Webkit"
    public bool Headless { get; set; } // Whether the browser should run in headless mode
}