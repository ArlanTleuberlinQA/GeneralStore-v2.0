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
    opts.AddAdditionalAppiumOption("ignoreHiddenApiPolicyError", true);

    // таймаути
    opts.AddAdditionalAppiumOption("uiautomator2ServerInstallTimeout", 120000);
    opts.AddAdditionalAppiumOption("uiautomator2ServerLaunchTimeout", 90000);
    opts.AddAdditionalAppiumOption("adbExecTimeout", 120000);
    opts.AddAdditionalAppiumOption("newCommandTimeout", 300);

    // важливе
    // opts.AddAdditionalAppiumOption("skipServerInstallation", false);
    opts.AddAdditionalAppiumOption("skipDeviceInitialization", false);
    opts.AddAdditionalAppiumOption("skipSettingsAppInstallation", false);
    opts.AddAdditionalAppiumOption("disableWindowAnimation", true);

    return opts;
}
 
        private static string GetEnvOr(string @default, string envVar) =>
            Environment.GetEnvironmentVariable(envVar) ?? @default;
    }
}