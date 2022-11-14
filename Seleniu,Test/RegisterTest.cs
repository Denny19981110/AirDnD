using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace Seleniu_Test
{
    [TestFixture]
    public class RegisterTest
    {
        private IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;
        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
        }
        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }
        [Test]
        public void register()
        {
            driver.Navigate().GoToUrl("https://airdnd-frontend.azurewebsites.net/");
            Thread.Sleep(200);
            driver.FindElement(By.Id("noShowAgainCheckbox")).Click();
            Thread.Sleep(200);
            driver.FindElement(By.Id("close-btn")).Click();

            driver.FindElement(By.CssSelector(".dropdownbox")).Click();
            driver.FindElement(By.CssSelector(".list-group-item:nth-child(2)")).Click();
            driver.FindElement(By.Id("check-email")).Click();
            //信箱要改
            driver.FindElement(By.Id("check-email")).SendKeys("bjf22957@nezid.com");
            driver.FindElement(By.Id("continuebtn")).Click();
            driver.FindElement(By.Id("first-name")).Click();
            //要改
            driver.FindElement(By.Id("first-name")).SendKeys("正中");
            driver.FindElement(By.Id("last-name")).Click();
            //要改
            driver.FindElement(By.Id("last-name")).SendKeys("蔣");
            driver.FindElement(By.Id("birthday")).Click();
            //要改
            driver.FindElement(By.Id("birthday")).SendKeys("1983-10-11");
            driver.FindElement(By.Id("birthday")).Click();
            driver.FindElement(By.Id("password")).Click();
            //要改
            driver.FindElement(By.Id("password")).SendKeys("123456");
            driver.FindElement(By.Id("signupbtn")).Click();
            Assert.Pass();
        }
    }
}
