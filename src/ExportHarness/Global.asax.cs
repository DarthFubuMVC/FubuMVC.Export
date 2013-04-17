using System;
using FubuMVC.Core;

namespace ExportHarness
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            FubuApplication.BootstrapApplication<ExportHarnessApplication>();
        }
    }
}