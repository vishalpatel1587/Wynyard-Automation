SET baseNuGetPath=..\..\..\_lib\NuGetPackages

cd "%~dp0\"
@echo Deleting TestFailureScreenshots folder...
IF exist "..\TestFailureScreenshots" rd /s /q "..\TestFailureScreenshots"
@echo Deleted TestFailureScreenshots folder.

@echo Checking if htmlReports exists
IF NOT EXIST "..\htmlReports" MKDIR "..\htmlReports"
IF NOT EXIST "..\htmlReports\TestResults" MKDIR "..\htmlReports\TestResults"

@echo Deleting htmlReports\FailureScreenshots folder containing html page with schreenshots...
IF exist "..\htmlReports\FailureScreenshots" rd /s /q "..\htmlReports\FailureScreenshots"

@echo Starting rebuilding solution...
REM "%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" "..\TestAutomation.csproj"  /p:Configuration=Debug
@echo Finished rebuilding solution

@echo Launching NUnit console...

REM setting NUnit result to fail the build in case where there is an error in tests
set nUnitResult=0

"%baseNuGetPath%\NUnit.Runners.2.6.3\tools\nunit-console.exe" /labels /out="..\htmlReports\TestResults\TestResult.txt" /xml="..\htmlReports\TestResults\TestResult.xml" "..\bin\Debug\TestAutomation.dll"
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

EXIT %nUnitResult%



