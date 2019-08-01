Feature: Validate DropBox Folder share functionality,
		 Create folder and Upload files in DropBox
		 And then share the Folder to any user.



Scenario Outline: Verify user can upload files to a folder and can Share
	Given user <userName> with <passWord> logs into the <DropBox_Application>
	Then  user should see the 'Home' as PageHeader
	Then  user NaviateTo 'Files'
	Then  user should see the 'Dropbox' as PageTitle
	Then  user creates NewFolder <FolderName>
	Then  user should see the newly create Folder <FolderName>
	Then  user upload files <UploadFileNames> to <FolderName>
	Then  verify newly Upload files <UploadFileNames> exists under the folder <FolderName>
	Then  user can share <FolderName> to <ShareFolderTo>	
	And   user Logs out of the application

Examples:
    | userName							 | passWord    | DropBox_Application           | FolderName			 | UploadFileNames                | ShareFolderTo			 |
    | autotesting.user.02@gmail.com		 | Sydney@0607 | https://www.dropbox.com/login | DropBox_TestFolder  | TestFile_1.txt:TestFile_2.txt  | m.bhowmik@riteq.com.au   |