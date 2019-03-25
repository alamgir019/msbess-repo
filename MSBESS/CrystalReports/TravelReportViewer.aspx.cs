using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using WebAdmin.BLL;
using System.Text;

namespace WebAdmin.CrystalReports
{
    public partial class TravelReportViewer : System.Web.UI.Page
    {
        private ReportDocument ReportDoc;
        private string ReportPath = "";
        private string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];
        DataTable MyDataTable = new DataTable();
        EmpTravelManager objTravelMgr = new EmpTravelManager();

        protected void Page_Init(object sender, EventArgs e)
        {
            ConfigureCrystalReports();
        }

        protected void CRVT_Unload(object sender, EventArgs e)
        {
            ReportDoc.Close();
            ReportDoc.Dispose();
            ReportDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        protected void CRVT_BeforeRender(object source, CrystalDecisions.Web.HtmlReportRender.BeforeRenderEvent e)
        {
            Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
        }
        private void ConfigureCrystalReports()
        {
            ReportDoc = new ReportDocument();

            switch (Session["REPORTID"].ToString())
            {
                case "TrStatus":
                    ReportPath = Server.MapPath("~/CrystalReports/rptTravelApplication.rpt");
                    ReportDoc.Load(ReportPath);

                    MyDataTable = objTravelMgr.SelectEmpTravelRpt(Session["travelId"].ToString(), Session["applicantId"].ToString(), Session["appStatus"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);

                    CRVT.ReportSource = ReportDoc;
                    break;
            }
        }
    
        public void PassParameterHeader(string ReportName, string FromDate, string ToDate)
        {
            ParameterFields pFields = new ParameterFields();
            ParameterField pfHeader = new ParameterField();
            ParameterField pfFromDate = new ParameterField();
            ParameterField pfToDate = new ParameterField();

            ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
            ParameterDiscreteValue dvFromDate = new ParameterDiscreteValue();
            ParameterDiscreteValue dvToDate = new ParameterDiscreteValue();

            pfHeader.Name = "pHeader";
            dvHeader.Value = ReportName;
            pfHeader.CurrentValues.Add(dvHeader);

            pfFromDate.Name = "pFromDate";
            dvFromDate.Value = FromDate;
            pfFromDate.CurrentValues.Add(dvFromDate);

            pfToDate.Name = "pToDate";
            dvToDate.Value = ToDate;
            pfToDate.CurrentValues.Add(dvToDate);

            pFields.Add(pfHeader);
            pFields.Add(pfFromDate);
            pFields.Add(pfToDate);

            CRVT.ParameterFieldInfo = pFields;
        }

        protected void CRVT_PreRender(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
        }
        
    }
}