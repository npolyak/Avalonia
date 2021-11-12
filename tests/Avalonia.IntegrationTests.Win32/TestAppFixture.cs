﻿using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Mac;
using OpenQA.Selenium.Appium.Windows;

namespace Avalonia.IntegrationTests.Win32
{
    public class TestAppFixture : IDisposable
    {
        private const string TestAppPath = @"..\..\..\..\..\samples\IntegrationTestApp\bin\Debug\net6.0\IntegrationTestApp";

        public TestAppFixture()
        {
            var opts = new AppiumOptions();
            var path = Path.GetFullPath(TestAppPath);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                opts.AddAdditionalCapability(MobileCapabilityType.App, path + ".exe");
                opts.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Windows);
                opts.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

                Session = new WindowsDriver<AppiumWebElement>(
                    new Uri("http://127.0.0.1:4723"),
                    opts);

                // https://github.com/microsoft/WinAppDriver/issues/1025
                SetForegroundWindow(new IntPtr(int.Parse(
                    Session.WindowHandles[0].Substring(2),
                    NumberStyles.AllowHexSpecifier)));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                opts.AddAdditionalCapability(MobileCapabilityType.App, path + ".exe");
                opts.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.MacOS);
                opts.AddAdditionalCapability(MobileCapabilityType.AutomationName, "mac2");

                Session = new MacDriver<AppiumWebElement>(
                    new Uri("http://127.0.0.1:4723/wd/hub"),
                    opts);
            }
            else
            {
                throw new NotSupportedException("Unsupported platform.");
            }
        }

        public AppiumDriver<AppiumWebElement> Session { get; }

        public void Dispose() => Session.Close();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}