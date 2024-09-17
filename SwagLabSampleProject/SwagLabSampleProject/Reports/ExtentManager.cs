using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SwagLabSampleProject.Reports
{
    public class ExtentManager
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _sparkReporter;

        public static ExtentReports GetExtent(string extentReport)
        {
            if (_extent == null)
            {
                string reportPath = @"E:\Youtube_Videos\QA\Automation Testing\TestProject\SampleProjectForGit\SwagLabSampleProject\SwagLabSampleProject\Reports\" + extentReport + ".html";

                _sparkReporter = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
            }
            return _extent;
        }
    }
}
