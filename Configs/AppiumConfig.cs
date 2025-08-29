using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace GeneralStore.Configs
{
    public static class AppiumConfig
    {
        private const string DefaultApk = "General-Store.apk";
        private const string DefaultDeviceName = "emulator-5554";
        private const string DefaultPlatformName = "Android";
        private const string DefaultAutomation = "UiAutomator2";
        private const string DefaultAppPackage = "com.androidsample.generalstore";
        private const string DefaultAppActivity = ".SplashActivity";

       public static AppiumOptions BuildAndroidOptions()
{
    var opts = new AppiumOptions();
    opts.PlatformName = GetEnvOr(DefaultPlatformName, "APPIUM_PLATFORM_NAME");
    opts.DeviceName = GetEnvOr(DefaultDeviceName, "APPIUM_DEVICE_NAME");
    opts.AutomationName = GetEnvOr(DefaultAutomation, "APPIUM_AUTOMATION_NAME");

    var apkPath = GetEnvOr(
        Path.Combine(TestContext.CurrentContext.TestDirectory, DefaultApk),
        "APPIUM_APK_PATH");

    if (!File.Exists(apkPath))
    {
        throw new FileNotFoundException($"APK file not found at: {apkPath}");
    }

    opts.App = apkPath;
    opts.AddAdditionalAppiumOption("appPackage", DefaultAppPackage);
    opts.AddAdditionalAppiumOption("appActivity", DefaultAppActivity);
    opts.AddAdditionalAppiumOption("ignoreHiddenApiPolicyError", true);

    // Таймаути
    opts.AddAdditionalAppiumOption("uiautomator2ServerInstallTimeout", 240000);
    opts.AddAdditionalAppiumOption("uiautomator2ServerLaunchTimeout", 240000);
    opts.AddAdditionalAppiumOption("adbExecTimeout", 240000);
    opts.AddAdditionalAppiumOption("newCommandTimeout", 600);
    opts.AddAdditionalAppiumOption("androidInstallTimeout", 300000);

    // ✅ КРИТИЧНО ВАЖЛИВІ ЗМІНИ:
    opts.AddAdditionalAppiumOption("skipServerInstallation", false);      // ← ЗМІНЕНО на false
    opts.AddAdditionalAppiumOption("skipDeviceInitialization", false);
    opts.AddAdditionalAppiumOption("skipSettingsAppInstallation", false);
    opts.AddAdditionalAppiumOption("disableWindowAnimation", true);

    // ✅ Додаткові опції для стабільності:
    opts.AddAdditionalAppiumOption("autoGrantPermissions", true);
    opts.AddAdditionalAppiumOption("autoAcceptAlerts", true);
    opts.AddAdditionalAppiumOption("isHeadless", true);
    
    // ✅ Додати для уникнення конфліктів інсталяції:
    opts.AddAdditionalAppiumOption("allowTestPackages", true);
    opts.AddAdditionalAppiumOption("enforceAppInstall", false);           // ← ЗМІНЕНО на false

    return opts;
}
        private static string GetEnvOr(string @default, string envVar) =>
            Environment.GetEnvironmentVariable(envVar) ?? @default;
    }
}