using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace WebAdmin
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });

            //Jquery
            ScriptManager.ScriptResourceMapping.AddDefinition(
               "jquery",
               new ScriptResourceDefinition
               {
                   Path = "~/Content/plugins/jQuery/jquery-2.2.3.min.js",
                   DebugPath = "~/Content/plugins/jQuery/jquery-2.2.3.min.js",
               });
            //bootstrap
            ScriptManager.ScriptResourceMapping.AddDefinition(
               "bootstrap",
               new ScriptResourceDefinition
               {
                   Path = "~/Content/plugins/bootstrap/js/bootstrap.min.js",
                   DebugPath = "~/Content/plugins/bootstrap/js/bootstrap.min.js",
               });

            //FastClick
            ScriptManager.ScriptResourceMapping.AddDefinition(
               "fastclick",
               new ScriptResourceDefinition
               {
                   Path = "~/Content/plugins/fastclick/fastclick.min.js",
                   DebugPath = "~/Content/plugins/fastclick/fastclick.js",
               });

            //AdminLTE App
            ScriptManager.ScriptResourceMapping.AddDefinition(
              "app",
              new ScriptResourceDefinition
              {
                  Path = "~/Content/dist/js/app.min.js",
                  DebugPath = "~/Content/dist/js/app.js",
              });
            //Sparkline
            ScriptManager.ScriptResourceMapping.AddDefinition(
              "sparkline",
              new ScriptResourceDefinition
              {
                  Path = "~/Content/plugins/sparkline/jquery.sparkline.min.js",
                  DebugPath = "~/Content/plugins/sparkline/jquery.sparkline.js",
              });
            //jvectormap
            ScriptManager.ScriptResourceMapping.AddDefinition(
              "jvectormap",
              new ScriptResourceDefinition
              {
                  Path = "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                  DebugPath = "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
              });
            
            ScriptManager.ScriptResourceMapping.AddDefinition(
              "jvectormap-world",
              new ScriptResourceDefinition
              {
                  Path = "~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                  DebugPath = "~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
              });
            //SlimScroll 1.3.0
            ScriptManager.ScriptResourceMapping.AddDefinition(
              "slimscroll",
              new ScriptResourceDefinition
              {
                  Path = "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
                  DebugPath = "~/Content/plugins/slimScroll/jquery.slimscroll.js",
              });
            //ChartJS 1.0.1
            ScriptManager.ScriptResourceMapping.AddDefinition(
             "Chart",
             new ScriptResourceDefinition
             {
                 Path = "~/Content/plugins/chartjs/Chart.min.js",
                 DebugPath = "~/Content/plugins/chartjs/Chart.js",
             });
            //notify
            ScriptManager.ScriptResourceMapping.AddDefinition(
             "notify",
             new ScriptResourceDefinition
             {
                 Path = "~/Content/plugins/notify/notify.min.js",
                 DebugPath = "~/Content/plugins/notify/notify.js",
             });

           

            //toastr
            ScriptManager.ScriptResourceMapping.AddDefinition(
             "iCheck",
             new ScriptResourceDefinition
             {
                 Path = "~/Content/plugins/iCheck/icheck.min.js",
                 DebugPath = "~/Content/plugins/iCheck/icheck.js",
             });
        }
    }
}