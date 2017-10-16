using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using WebAdmin.BLL;

namespace WebAdmin.UserControls.Report
{
    public partial class ctlReportViewer : System.Web.UI.UserControl
    {
        private ReportDocument ReportDoc;
        private string ReportPath = "";
        private string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];

        ReportManager rptManager = new ReportManager();
        DataTable MyDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigureCrystalReports();

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (null != MyDataTable)
                MyDataTable.Dispose();
            if (ReportDoc != null)
            {
                ReportDoc.Close();
                ReportDoc.Dispose();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
            base.Render(writer);
        }

        private void ConfigureCrystalReports()
        {
            ReportDoc = new ReportDocument();
            switch (Session["REPORTID"].ToString())
            {
                case "AE":
                    //Report no 2 : Employee wise attendance
                    //Anol                
                    ReportPath = Server.MapPath("~/CrystalReports/rptAttndEmpWise.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.Get_MonthlyAttnd(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                         Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(),
                        Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                    string strPresent = CountStatus("P", MyDataTable);
                    string strAbsent = CountStatus("A", MyDataTable);
                    string strLeave = CountStatus("LV", MyDataTable);
                    string strDelay = CountStatus("L", MyDataTable);
                    string strWeekend = CountStatus("W", MyDataTable);
                    string strHoliday = CountStatus("H", MyDataTable);
                    break;
            }
        }
        protected string CountStatus(string strStatus, DataTable dt)

        {
            //useless function
            string strExpr = "Status='" + strStatus + "'";
            DataRow[] foundRows = dt.Select(strExpr);
            return Convert.ToString(foundRows.Length);
        }
        public void PassParameterHeader(string ReportName)
        {
            ParameterFields pFields = new ParameterFields();
            ParameterField pfHeader = new ParameterField();
            ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();

            pfHeader.Name = "pHeader";
            dvHeader.Value = ReportName;
            pfHeader.CurrentValues.Add(dvHeader);

            pFields.Add(pfHeader);

            CRVT.ParameterFieldInfo = pFields;
        }

        public void PassParameterHeader(string ReportName, string FiscalYr)
        {
            ParameterFields pFields = new ParameterFields();
            ParameterField pfHeader = new ParameterField();
            ParameterField pfFiscalYr = new ParameterField();

            ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
            ParameterDiscreteValue dvFiscalYr = new ParameterDiscreteValue();

            pfHeader.Name = "pHeader";
            dvHeader.Value = ReportName;
            pfHeader.CurrentValues.Add(dvHeader);

            pfFiscalYr.Name = "pFiscalYr";
            dvFiscalYr.Value = FiscalYr;
            pfFiscalYr.CurrentValues.Add(dvFiscalYr);

            pFields.Add(pfHeader);
            pFields.Add(pfFiscalYr);

            CRVT.ParameterFieldInfo = pFields;
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
    }
}