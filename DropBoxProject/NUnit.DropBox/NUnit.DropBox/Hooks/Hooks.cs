using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace NUnit.DropBox.Hooks
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest featureName;
        private ExtentTest scenario;
        private static ExtentReports extent;               
        private IObjectContainer _objectContainer;
        public  IWebDriver _driver;
        ScenarioContext _scenarioContext;

        //
       public Hooks(IObjectContainer objectContainer,ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;            
            this._scenarioContext = scenarioContext;
           
        }

       [BeforeScenario]
        public void BeforeScenario()
        {
            DropBox_TestIter intr = null;
            intr = new DropBox_TestIter("chrome");
            _driver = intr.driver;
            _objectContainer.RegisterInstanceAs(_driver);
            this.scenario = featureName.CreateNode<Scenario>(this._scenarioContext.ScenarioInfo.Title);
        }

       [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }

       [BeforeTestRun]
       public static void InitializeReport()
       {
           string _projectFolder =  Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
           extent = new ExtentReports();
           var htmlReporter = new ExtentHtmlReporter(_projectFolder+"\\Test_Execution_Reports\\index.html");
           extent.AddSystemInfo("Environment", "LIVE");
           extent.AddSystemInfo("User Name", "Auto User");
           extent.AttachReporter(htmlReporter);
       }

       [AfterTestRun]
       public static void TearDownReport()
       {
           extent.Flush();
       }

       [BeforeFeature]
       public static void BeforeFeature(FeatureContext _featureContext)
       {
           featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
       }
              
       [AfterStep]
       public void InsertReportingSteps()
       {
             
             var stepType = this._scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            

              PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
              MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
              object TestResult = getter.Invoke(this._scenarioContext, null);
            

              if (this._scenarioContext.TestError == null)
             {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(this._scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(this._scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(this._scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(this._scenarioContext.StepContext.StepInfo.Text);
             }
             else if (this._scenarioContext.TestError != null)
             {
                 if (stepType == "Given")
                     scenario.CreateNode<Given>(this._scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                 else if (stepType == "When")
                     scenario.CreateNode<When>(this._scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                 else if (stepType == "Then")
                     scenario.CreateNode<Then>(this._scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
             }

             //Pending Status
             if (TestResult.ToString() == "StepDefinitionPending")
             {
                 if (stepType == "Given")
                     scenario.CreateNode<Given>(this._scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                 else if (stepType == "When")
                     scenario.CreateNode<When>(this._scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                 else if (stepType == "Then")
                     scenario.CreateNode<Then>(this._scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
             }
        }
    }

   enum BrowserType
   {
       Chrome,
       Firefox,
       IE
    }
}
