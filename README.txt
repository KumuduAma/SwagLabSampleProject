Project Overview
---------------------

This project is a Selenium-based automation testing framework designed to perform testing on Swag Lab web application (https://www.saucedemo.com/). It utilizes key design patterns like the Page Object Model (POM) for maintainability and reusability. In addition, the framework generates detailed Extent Reports for test execution results, captures Screenshots for failed tests, and organizes test cases efficiently using NUnit framework.

Key Features
--------------------

Page Object Model (POM): Enhances code reusability, readability, and maintainability by separating test logic from UI interactions.
Extent Reports: Provides rich, interactive HTML reports for test execution results.
Screenshots: Automatically captures screenshots for failed test cases, aiding in debugging.
NUnit Integration: Supports parallel test execution, data-driven testing.

Technologies Used
-----------------------

Programming Language: C#
Test Framework: NUnit
Browser Automation: Selenium WebDriver
Reporting: ExtentReports

Page Object Model (POM)
----------------------------

This project follows the Page Object Model (POM) design pattern. Each web page is represented by a corresponding C# class, where interactions with page elements (buttons, text fields, icons etc.) are encapsulated as methods.

Reporting with Extent Reports
-----------------------------------

Extent Reports are generated after each test run, providing detailed logs, test statuses, and captured screenshots of failures.

Location: The reports are saved in the Reports directory/ directory as ExtentReport.html.

----------------------------------------

The report includes:

Test pass/fail status
Test start/end time
Error logs for failed tests
Screenshots for failed tests

Screenshots
----------------------

Screenshots are automatically taken for any test case that fails during execution. These screenshots are stored in the Screenshots.