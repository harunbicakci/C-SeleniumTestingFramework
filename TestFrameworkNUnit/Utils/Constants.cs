using System;
using System.IO;

namespace TestFrameworkNUnit.Utils
{
    public static class Constants
    {
        public static readonly string CHROME_DRIVER_PATH = Path.Combine(Directory.GetCurrentDirectory(), "drivers", "chromedriver");
        public static readonly string GECKO_DRIVER_PATH = Path.Combine(Directory.GetCurrentDirectory(), "drivers", "geckodriver");
        public static readonly string CONFIGURATION_FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), "src", "test", "resources", "configs", "configuration.properties");
        public static readonly int IMPLICIT_WAIT_TIME = 10;
        public static readonly int EXPLICIT_WAIT_TIME = 30;
        public static readonly string SCREENSHOT_FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), "screenshot") + Path.DirectorySeparatorChar;
    }
}

