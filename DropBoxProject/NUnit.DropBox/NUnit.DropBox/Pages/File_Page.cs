using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NUnit.DropBox
{
    public class File_Page
    {
        private IWebDriver FileDriver;
        private BrowserUtil bu;
        private string _projectFolder;
        public File_Page(IWebDriver driver)
        {
            this.FileDriver = driver;
            this.bu = new BrowserUtil(FileDriver);
            _projectFolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        }

        Dictionary<string, string> dicRepo = new Dictionary<string, string>()
        {
            { "FilesLink"                   , "//*[@id='files']"},
            { "CreateFolderModal"           , "//*[@id='cdm-create-folder-modal']"},
            { "NewFolderName"               , ".//input[@class='cdm-create-folder-modal__content-name mc-input']" },
            { "OnlyYouCanAccessFolder"      , ".//input[@id='not_confidential_option']"},
            { "CreateButton"                , ".//button[@class='button-primary dbmodal-button']" },
            { "NewFolderLink"               , ".//div[@class='ue-effect-container uee-AppActionsView-SecondaryActionMenu-text-new-folder']"},
            { "UploadFileLink"              , ".//div[@class='ue-effect-container uee-AppActionsView-SecondaryActionMenu-text-upload-file']"},
            { "FolderCheckBox"              , ".//span[@class='mc-checkbox-border']"},
            { "ShareBtn"                    , ".//button[@class='primary-action-menu__button action-share mc-button mc-button-primary']"},
            { "emailID"                     , ".//input[@class='mc-tokenized-input-input']"},
            { "FtShareBtn"                  , ".//button[@class='scl-sharing-modal-footer-inband__button mc-button mc-button-primary']"},
            { "MyFilesLink"                 , ".//span[@class='ue-effect-container uee-FeatureNav-myFiles']/a"},
            { "FileListView"                , ".//div[@class='brws-files-view brws-files-view--list_view']"},
            { "tblFileView"                 , ".//tbody[@class='mc-table-body mc-table-body-culled']"},
            { "folderLink"                  , ".//a[@class='brws-file-name-cell-filename']"},
            { "folderName"                  , ".//a[@class='brws-file-name-cell-filename']/div/span"},
            { "deleteBtn"                   , ".//button[@class='mc-tertiary-link-button secondary-action-menu__button action-delete']"},
            { "removeShareFolder"           , ".//div[@class='ReactModal__Content ReactModal__Content--after-open mc-modal']"},
            { "removeBtn"                   , ".//button[@class='mc-button mc-button-primary']"},
            { "HomePageTitle"               , ".//span[@class='ue-effect-container uee-PathBreadcrumbs-HomeTitle']"},
            { "FileLstViewbtbody"           , ".//table[@class='mc-table brws-files-view-list']/tbody"},
            { "FileNameLink"                , ".//a[@class='brws-file-name-cell-filename']"},
            { "FileName"                    , ".//div[@class='brws-file-name-element']/span"},
            { "membersTitle"                , ".//div[@class='shared-with-members clearfix']"},
            { "audiencedescription"         , ".//div[@class='audience-description']"},
            { "SharingMembersList"          , ".//div[@class='sharing-member-list']/ul/div/li"},
            { "shareMemberName"             , ".//span[@class='scl-member-row-details-span']"},
            { "cancelBtn"                   , ".//button[@class='scl-sharing-modal-header__close mc-button-styleless']"},
            { "FolderTitle"                 , ".//nav[@id='path-breadcrumbs']/span[2]"}
        };

        public void clickon(string objectName)
        {
            bu.WaitForElement(FileDriver, dicRepo[objectName],2);
            FileDriver.FindElement(By.XPath(dicRepo[objectName])).Click();
        }

        public void createNewFolder(string folderName)
        {
           
                bu.WaitForElement(FileDriver, dicRepo["NewFolderLink"], 2);
                FileDriver.FindElement(By.XPath(dicRepo["NewFolderLink"])).Click();
                bu.WaitForElement(FileDriver, dicRepo["CreateFolderModal"], 2);
                IWebElement crtFolderModal = FileDriver.FindElement(By.XPath(dicRepo["CreateFolderModal"]));
                crtFolderModal.FindElement(By.XPath(dicRepo["NewFolderName"])).SendKeys(folderName);
                crtFolderModal.FindElement(By.XPath(dicRepo["OnlyYouCanAccessFolder"])).Click();
                crtFolderModal.FindElement(By.XPath(dicRepo["CreateButton"])).Click();
                Thread.Sleep(5000);
                bu.pageLoad(FileDriver);            
        }

        public void uploadFiles(string files,string folderName)
        {
            string[] _files = files.Split(':');
            foreach (string _file in _files)
            {
                selectFolder(folderName);
                FileDriver.FindElement(By.XPath(dicRepo["FolderTitle"])).Click();
                bu.WaitForElement(FileDriver, dicRepo["UploadFileLink"], 2);
                FileDriver.FindElement(By.XPath(dicRepo["UploadFileLink"])).Click();
                Thread.Sleep(20000);
                SendKeys.SendWait(_projectFolder+"\\TestData\\FilesToUpload\\" + _file);
                Thread.Sleep(5000);
                SendKeys.SendWait(@"{Enter}");
                Thread.Sleep(5000);
            }
            
        }

        public void shareFolder(string folderName, string eMailID)
        {
            IWebElement _desiredFolderRow = findFolder(folderName);
            IWebElement _folderRowCheckBox = _desiredFolderRow.FindElement(By.XPath(dicRepo["FolderCheckBox"]));

            bu.mouseOverClick(FileDriver, _folderRowCheckBox);

            bu.WaitForElement(FileDriver, dicRepo["ShareBtn"], 2);
           
            IWebElement shareButton = FileDriver.FindElement(By.XPath(dicRepo["ShareBtn"]));
            shareButton.Click();
            bu.WaitForElement(FileDriver, dicRepo["emailID"], 2);
           
            IWebElement eMailId = FileDriver.FindElement(By.XPath(dicRepo["emailID"]));
            eMailId.SendKeys(eMailID);

            IWebElement _shareButton = FileDriver.FindElement(By.XPath(dicRepo["FtShareBtn"]));
            bu.mouseOverClick(FileDriver, _shareButton);

            Thread.Sleep(15000);
        }

        public void selectFolder(string _foldrName)
        {
            IWebElement myFiles = FileDriver.FindElement(By.XPath(dicRepo["MyFilesLink"]));
            myFiles.Click();

            Thread.Sleep(10000);
            bu.WaitForElement(FileDriver, dicRepo["FileListView"], 2);
            
            IWebElement filesView = FileDriver.FindElement(By.XPath(dicRepo["FileListView"]));
            IWebElement tblfileView = filesView.FindElement(By.XPath(dicRepo["tblFileView"]));

            IList<IWebElement> _folders = tblfileView.FindElements(By.TagName("tr"));

            foreach (IWebElement _folder in _folders)
            {

                IWebElement _folderLink = _folder.FindElement(By.XPath(dicRepo["folderLink"]));
                String _folderName = _folder.FindElement(By.XPath(dicRepo["folderName"])).Text;
                if (_folderName.Equals(_foldrName))
                {
                    _folderLink.Click();
                    break;
                }
            }
            bu.pageLoad(FileDriver);
        }

        public IWebElement findFolder(string _foldrName)
        {
            IWebElement desiredFolder = null;
            try
            {
                IWebElement myFiles = FileDriver.FindElement(By.XPath(dicRepo["MyFilesLink"]));
                myFiles.Click();


                bu.WaitForElement(FileDriver, dicRepo["FileListView"], 1);
                IWebElement filesView = FileDriver.FindElement(By.XPath(dicRepo["FileListView"]));
                IWebElement tblfileView = filesView.FindElement(By.XPath(dicRepo["tblFileView"]));

                IList<IWebElement> _folders = tblfileView.FindElements(By.TagName("tr"));

                foreach (IWebElement _folder in _folders)
                {

                    IWebElement _folderLink = _folder.FindElement(By.XPath(dicRepo["folderLink"]));
                    String _folderName = _folder.FindElement(By.XPath(dicRepo["folderName"])).Text;
                    if (_folderName.Equals(_foldrName))
                    {
                        desiredFolder = _folder;
                        break;
                    }
                }
            }catch(Exception e)
            {

            }
            return desiredFolder;
        }
               
        public void deleteFolder(string folderName)
        {
            IWebElement _desiredFolderRow = findFolder(folderName);
            IWebElement _folderRowCheckBox = _desiredFolderRow.FindElement(By.XPath(dicRepo["FolderCheckBox"]));
                        
            bu.mouseOverClick(FileDriver, _folderRowCheckBox);
            bu.WaitForElement(FileDriver, dicRepo["deleteBtn"], 2);
           
            IWebElement deleteButton = FileDriver.FindElement(By.XPath(dicRepo["deleteBtn"]));
            
            bu.mouseOverClick(FileDriver, deleteButton);
            bu.WaitForElement(FileDriver, dicRepo["removeShareFolder"], 2);
        
            IWebElement removeShareFolder = FileDriver.FindElement(By.XPath(dicRepo["removeShareFolder"]));
            IWebElement removeBtn = removeShareFolder.FindElement(By.XPath(dicRepo["removeBtn"]));

            removeBtn.Click();
            bu.pageLoad(FileDriver);
        }

        public void verifyPageTitle(string pageTitle)
        {
            string _pageTitle = FileDriver.FindElement(By.XPath(dicRepo["HomePageTitle"])).Text;
            Assert.AreEqual(_pageTitle, pageTitle);
        }

        public void veriyFolderExists(string folder, Boolean ieExists)
        {
            Boolean _isExists = false;
            try
            {
                IWebElement flder = findFolder(folder);
                String _folderName = FileDriver.FindElement(By.XPath(dicRepo["folderName"])).Text;
                if (_folderName.Equals(folder))
                {
                    _isExists = true;
                }
            }catch(Exception e)
            {
                _isExists = false;
            }
            Assert.AreEqual(ieExists, _isExists);
        }

        public void veriyFileExists(string flNames, string folder)
        {
            selectFolder(folder);
            bu.WaitForElement(FileDriver, dicRepo["FileLstViewbtbody"], 2);
            string[] fileNames = flNames.Split(':');

            foreach (string fileName in fileNames)
            {
                IWebElement tblFileViewList = FileDriver.FindElement(By.XPath(dicRepo["FileLstViewbtbody"]));
                IList<IWebElement> filesList = tblFileViewList.FindElements(By.TagName("tr"));
                Boolean _isExists = false;
                foreach (IWebElement file in filesList)
                {
                    IWebElement fileLink = file.FindElement(By.XPath(dicRepo["FileNameLink"]));
                    String _fileName = file.FindElement(By.XPath(dicRepo["FileName"])).Text;
                    if (fileName.Equals(_fileName))
                    {
                        _isExists = true;
                        break;
                    }                    
                }
                Assert.AreEqual(true, _isExists);
            }

            
        }

        public void verifyFolderisSharedTo(string folderName, string sharedUser)
        {

            Boolean isShareMemeber = false;

            IWebElement _desiredFolderRow = findFolder(folderName);
            IWebElement _folderRowCheckBox = _desiredFolderRow.FindElement(By.XPath(dicRepo["FolderCheckBox"]));
           
            bu.mouseOverClick(FileDriver, _folderRowCheckBox);

            IWebElement membersTitle = _desiredFolderRow.FindElement(By.XPath(dicRepo["membersTitle"]));
            IWebElement audiencedescription = membersTitle.FindElement(By.XPath(dicRepo["audiencedescription"]));

            Actions action = new Actions(FileDriver);
            action.MoveToElement(audiencedescription).Click(audiencedescription).Build().Perform();

          
            bu.WaitForElement(FileDriver, dicRepo["SharingMembersList"], 2);
            IList<IWebElement> sharingMemeberList = FileDriver.FindElements(By.XPath(dicRepo["SharingMembersList"]));

            foreach (IWebElement sharingMemeber in sharingMemeberList)
            {
                String shareMemberName = sharingMemeber.FindElement(By.XPath(dicRepo["shareMemberName"])).Text;
                if (shareMemberName.Equals(sharedUser))
                {
                    isShareMemeber = true;
                }
            }

            Assert.AreEqual(true, isShareMemeber);

            IWebElement cancelBtn = FileDriver.FindElement(By.XPath(dicRepo["cancelBtn"]));
            cancelBtn.Click();
        }
    }
}
