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
    public class Login_Page
    {
        private IWebDriver LoginDriver;
        private BrowserUtil bu;

        public Login_Page(IWebDriver driver)
        {
            this.LoginDriver = driver;
            this.bu = new BrowserUtil(LoginDriver);
        }

        Dictionary<string, string> dicRepo = new Dictionary<string, string>()
        {
            { "UserName"              , "//*[@name='login_email']"},
            { "UserPassword"          , "//*[@name='login_password']"},
            { "LoginButon"            , "//*[@class='login-button signin-button button-primary']"},
            { "HomeLink"              , "//*[@id='home']"},
            { "avatar"                , "//*[@id='maestro-header']/div/div[2]/div/span"},
            { "signOutBtn"            , "//a[@href='https://www.dropbox.com/logout']"}
        };

        public void gotologinPage(string userName, string password, string url)
        {
            try
            {
                LoginDriver.Navigate().GoToUrl(url);
                new WebDriverWait(LoginDriver, TimeSpan.FromSeconds(500000)).Until(
                 d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
               
                LoginDriver.Manage().Window.Maximize();
                LoginDriver.FindElement(By.XPath(dicRepo["UserName"])).SendKeys(userName);
                LoginDriver.FindElement(By.XPath(dicRepo["UserPassword"])).SendKeys(password);
                LoginDriver.FindElement(By.XPath(dicRepo["LoginButon"])).Click();

                bu.WaitForElement(LoginDriver, dicRepo["HomeLink"], 2);

                IWebElement HomeLink =LoginDriver.FindElement(By.XPath(dicRepo["HomeLink"]));
                Assert.AreEqual(true, HomeLink.Displayed);
            }
            catch(Exception e)
            {
                throw (e);
            }
            
        }

        public void signOut()
        {
            try
            {
                bu.WaitForElement(LoginDriver, dicRepo["avatar"], 2);
                IWebElement avatarBtn  = LoginDriver.FindElement(By.XPath(dicRepo["avatar"]));
                avatarBtn.Click();
      
                bu.WaitForElement(LoginDriver, dicRepo["signOutBtn"], 2);
                IWebElement signOutBtn = LoginDriver.FindElement(By.XPath(dicRepo["signOutBtn"]));
                signOutBtn.Click();
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        
    }
}
