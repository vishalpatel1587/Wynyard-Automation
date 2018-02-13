REM SET VARS
SET baseNuGetPath=..\NuGetPackages
SET dll=..\bin\Release\TestAutomation.dll

cd "%~dp0\"
@echo Deleting TestFailureScreenshots folder...
IF exist "..\TestFailureScreenshots" rd /s /q "..\TestFailureScreenshots"
@echo Deleted TestFailureScreenshots folder.

@echo Checking if htmlReports exists
IF NOT EXIST "..\htmlReports" MKDIR "..\htmlReports"
IF NOT EXIST "..\htmlReports\TestResults" MKDIR "..\htmlReports\TestResults"

@echo Deleting htmlReports\FailureScreenshots folder containing html page with screenshots...
IF exist "..\htmlReports\FailureScreenshots" rd /s /q "..\htmlReports\FailureScreenshots"

@echo Launching NUnit console...

REM setting NUnit result to fail the build in case where there is an error in tests
set nUnitResult=0

"%baseNuGetPath%\NUnit.Runners.2.6.3\tools\nunit-console.exe" /labels /out="..\htmlReports\TestResults\TestResult.txt" /xml="..\htmlReports\TestResults\TestResult.xml" %dll%
@if NOT %errorlevel% == 0 (
echo "Some of the tests failed - %errorlevel%"
set nUnitResult=%errorlevel%
)

@echo Launching specflow.exe and creating html output...
cd "..\htmlReports\TestResults\"
"..\%baseNuGetPath%\SpecFlow.1.9.0\tools\specflow.exe" nunitexecutionreport "..\..\TestAutomation.csproj" /xsltFile:"..\%baseNuGetPath%\SpecFlow.1.9.0\ReportTemplates\nunit-dream\ExecutionReport.xslt" /out:"TestResults.html"
@if NOT %errorlevel% == 0 (
echo "Error generating NUnit test report - %errorlevel%"
EXIT %errorlevel%
)

@echo generation html documentation with Pickles...
"..\%baseNuGetPath%\Pickles.CommandLine.0.19.0\tools\pickles.exe" --feature-directory="..\..\Features" --output-directory="..\Documentation" --trfmt=nunit --lr=TestResult.xml --documentation-format=html

cd "%WORKSPACE%\Artifacts\_Testing\AutomationTesting"

REM PREPARE TEST ARTIFACTS FOLDER
REM ================================
RMDIR /S /Q "TestArtifacts"
MKDIR "TestArtifacts\"

REM COPYING FILES TO TestArtifacts FOLDER
REM =================================
XCOPY /f /s /e /y /i "TestOutputData" "TestArtifacts\TestOutputData" 
XCOPY /f /s /e /y /i "TestFailureScreenshots" "TestArtifacts\TestFailureScreenshots"

@echo Unmounting images if any from mount image pro..
cd "..\..\..\..\..\..\GetData\Mount Image Pro v5\"
MIP5.exe UNMOUNT /ALL

EXIT %nUnitResult%



