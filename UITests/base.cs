﻿//******************************************************************************
//
// Copyright (c) 2016 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace UITests
{
    public class Test_Base
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
#if DEBUG
        private const string AppUIBasicAppId = "Microsoft.WinUI3ControlsGallery.Debug_s9y1p3hwd5qda!App";
#else
        private const string AppUIBasicAppId = "Microsoft.WinUI3ControlsGallery_s9y1p3hwd5qda!App";
#endif
        protected static WindowsDriver<WindowsElement> session = null;

        public static void Setup(TestContext context)
        {
            
            if (session == null)
            {
                AppiumOptions appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", AppUIBasicAppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");

                try
                {
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
                }
                catch
                {  }
                Thread.Sleep(125000);
                if (session == null)
                {
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
                }
                Assert.IsNotNull(session);
                Assert.IsNotNull(session.SessionId);
                // Dismiss the disclaimer window that may pop up on the very first application launch
                try
                {
                    session.FindElementByName("Disclaimer").FindElementByName("Accept").Click();
                }
                catch { }
            }
            session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static void TearDown()
        {
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}