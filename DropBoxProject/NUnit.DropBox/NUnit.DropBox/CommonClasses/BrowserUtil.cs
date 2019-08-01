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
    public class BrowserUtil
    {
        private IWebDriver _driver;

        public BrowserUtil(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void mouseOverClick( IWebDriver driver, IWebElement webObj)
        {

            String strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ((IJavaScriptExecutor)driver).ExecuteScript(strJavaScript, webObj);

            webObj.Click();
        }

        public void WaitForElement(IWebDriver driver, string ObjectXpath, int timeout = 40)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until<bool>((IWebDriver drv) =>
            {
                    IWebElement element = drv.FindElement(By.XPath(ObjectXpath));
                    if (element != null)
                    {
                        return true;
                    }else
                    { 
                        return false;
                    }
            });
        }

        public void pageLoad(IWebDriver driver)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(500000)).Until(
                  d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            Thread.Sleep(2000);
        }
     }
}
