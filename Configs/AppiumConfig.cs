using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
 
namespace GeneralStore.Configs
{
    public static class AppiumConfig
    {
        private const string DefaultApk          = "General-Store.apk";
        private const string DefaultDeviceName   = "Android Emulator";
        private const string DefaultPlatformName = "Android";
        private const string DefaultAutomation   = "UiAutomator2";
        private const string DefaultAppPackage   = "com.androidsample.generalstore";
        private const string DefaultAppActivity  = ".SplashActivity";
 
public static AppiumOptions BuildAndroidOptions()
{
    var opts = new AppiumOptions();
    opts.PlatformName   = GetEnvOr(DefaultPlatformName,  "APPIUM_PLATFORM_NAME");
    opts.DeviceName     = GetEnvOr(DefaultDeviceName,    "APPIUM_DEVICE_NAME");
    opts.AutomationName = GetEnvOr(DefaultAutomation,    "APPIUM_AUTOMATION_NAME");

    var apkPath = GetEnvOr(
        Path.Combine(TestContext.CurrentContext.TestDirectory, DefaultApk),
        "APPIUM_APK_PATH");

    opts.App = apkPath;
    opts.AddAdditionalAppiumOption("appPackage", DefaultAppPackage);
    opts.AddAdditionalAppiumOption("appActivity", DefaultAppActivity);

    return opts;
}
 
        private static string GetEnvOr(string @default, string envVar) =>
            Environment.GetEnvironmentVariable(envVar) ?? @default;
    }
}