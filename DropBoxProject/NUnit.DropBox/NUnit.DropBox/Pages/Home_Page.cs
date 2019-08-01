using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NUnit.DropBox
{
    public class Home_Page
    {
        private IWebDriver HomeDriver;
        private BrowserUtil bu;
        public Home_Page(IWebDriver driver)
        {
            this.HomeDriver = driver;
            this.bu = new BrowserUtil(HomeDriver);
        }

        Dictionary<string, string> dicRepo = new Dictionary<string, string>()
        {
            { "FilesLink"              , "//*[@id='files']"},
            { "HomePageTitle"          , ".//h1[@class='page-header__heading']"}
        };

        public void goTo_FilesPage()
        {
            HomeDriver.FindElement(By.XPath(dicRepo["FilesLink"])).Click();
        }

        public void clickon(string objectName)
        {
            bu.WaitForElement(HomeDriver, dicRepo[objectName], 2);
            HomeDriver.FindElement(By.XPath(dicRepo[objectName])).Click();
        }

        public void verifyPageHeader(string pageHeader)
        {
            try
            {
                IAlert alert = HomeDriver.SwitchTo().Alert();
            }
            catch (Exception e)
            {

            }
            string _pageHeader = HomeDriver.FindElement(By.XPath(dicRepo["HomePageTitle"])).Text;
            Assert.AreEqual(_pageHeader, pageHeader);
        }
    }
}
