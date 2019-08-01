#  Validating DropBox application

   A BDD Automation Test Framework to validate the DropBox Share Folder functionality 

   Built on 
   Selenium WebDriver,NUnit, Specflow, C# 

## CONFIG

1.  `Path` value is where the DropBoxProject Folder is copied. Example cd 
	`Path` = C:\git   This can be changed..
	 Example:
	 Path`\DropBoxProject 
 
	 Download the AutoProjects.rar from the DropBox and UnZip and place the DropBoxProject folder in the `Path` 

2.   Open the Project in The Visual Studio 2017  

3.   Build the Solution. 

4.   Browser is Chrome - Version -  75.0.3770.142 

5.   Provided Option to supply customized Data in the Validate_DropBox.feature 
	 Note: To Upload more Number of files, Add File in the ~\NUnit.DropBox\TestData and simply appened with ":" in the "|UploadFileNames|" section in Validate_DropBox.feature  	
     		
	 
## RUN

1.  Run the Tests from the Test Explorer, if not visible you can open it from the Test>Windows>Test Explorer.                
									Or
2.  Run from the command line using  NUnit.ConsoleRunner

    Open Command Prompt 
	CD `Path`+DropBoxProject\NUnit.DropBox\packages\NUnit.ConsoleRunner.3.10.0\tools`
	
	Then Execute 
	
	nunit3-console.exe `Path`+"\DropBoxProject\NUnit.DropBox\NUnit.DropBox\bin\Debug\NUnit.DropBox.dll

3.  Check the Output results in the `Path`+\DropBoxProject\NUnit.DropBox\Test_Execution_Reports\index.html 


TODO:

1) Remote Web Driver Implementation 
2) Multi Browser 
3) Distributed Testing.
4) Screen Shots on Failure in Reports
5) Jenkins or TeamCity Intergration
6) Exception handling 
7) Cusotmizing the Reports 
8) Verificaiton check Folder is shared to user or not. 
etc..


