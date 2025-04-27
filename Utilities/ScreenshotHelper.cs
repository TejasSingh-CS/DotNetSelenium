using OpenQA.Selenium;

namespace DotNetSelenium.Utilities
{
    public static class ScreenshotHelper
    {
        public static void CaptureScreenshot(IWebDriver driver)
        {
            // Call the screenshot helper
            string folderPath = @"C:\Local\Other\Tutorial\SeleniumC#\DotNetSelenium\DotNetSelenium\Screenshots\";
            // Create a unique file name
            string fileName = Path.Combine(folderPath, $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "WebDriver cannot be null when taking a screenshot.");
            }

            try
            {
                // Create the folder if it does not exist
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var screenshotDriver = driver as ITakesScreenshot;
                if (screenshotDriver != null)
                {
                    var screenshot = screenshotDriver.GetScreenshot();
                    screenshot.SaveAsFile(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Screenshot capture failed: " + ex.Message);
            }
        }
    }
}
