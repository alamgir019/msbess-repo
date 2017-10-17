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
            Common.FillYearList(5, ddlYear);
            DateTime now = DateTime.Now;
            ddlYear.SelectedValue = Convert.ToInt32(now.Year).ToString();
            ConfigureCrystalReports();
            PanelVisibilityMst("0", "1", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
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
            //Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
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
            //ParameterFields pFields = new ParameterFields();
            //ParameterField pfHeader = new ParameterField();
            //ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();

            //pfHeader.Name = "pHeader";
            //dvHeader.Value = ReportName;
            //pfHeader.CurrentValues.Add(dvHeader);

            //pFields.Add(pfHeader);

            //CRVT.ParameterFieldInfo = pFields;
        }

        public void PassParameterHeader(string ReportName, string FiscalYr)
        {
            //ParameterFields pFields = new ParameterFields();
            //ParameterField pfHeader = new ParameterField();
            //ParameterField pfFiscalYr = new ParameterField();

            //ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
            //ParameterDiscreteValue dvFiscalYr = new ParameterDiscreteValue();

            //pfHeader.Name = "pHeader";
            //dvHeader.Value = ReportName;
            //pfHeader.CurrentValues.Add(dvHeader);

            //pfFiscalYr.Name = "pFiscalYr";
            //dvFiscalYr.Value = FiscalYr;
            //pfFiscalYr.CurrentValues.Add(dvFiscalYr);

            //pFields.Add(pfHeader);
            //pFields.Add(pfFiscalYr);

            //CRVT.ParameterFieldInfo = pFields;
        }


        public void PassParameterHeader(string ReportName, string FromDate, string ToDate)
        {
            //ParameterFields pFields = new ParameterFields();
            //ParameterField pfHeader = new ParameterField();
            //ParameterField pfFromDate = new ParameterField();
            //ParameterField pfToDate = new ParameterField();

            //ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
            //ParameterDiscreteValue dvFromDate = new ParameterDiscreteValue();
            //ParameterDiscreteValue dvToDate = new ParameterDiscreteValue();

            //pfHeader.Name = "pHeader";
            //dvHeader.Value = ReportName;
            //pfHeader.CurrentValues.Add(dvHeader);

            //pfFromDate.Name = "pFromDate";
            //dvFromDate.Value = FromDate;
            //pfFromDate.CurrentValues.Add(dvFromDate);

            //pfToDate.Name = "pToDate";
            //dvToDate.Value = ToDate;
            //pfToDate.CurrentValues.Add(dvToDate);

            //pFields.Add(pfHeader);
            //pFields.Add(pfFromDate);
            //pFields.Add(pfToDate);

            //CRVT.ParameterFieldInfo = pFields;
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
            //Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
        }
        private void PanelVisibilityMst(string sSearchBy, string sBranch, string sDiv, string sDept, string sDate, string sShow, string sPostingDist,
        string sClosed, string PFisY, string PMonthFrom, string PMonTo, string PYear, string P_GridSalSubLoc, string PSalaryLocation, string PSalarySubLocation, string gvPost,
        string P_GridEmpList, string P_SCEl, string P_AV, string P_GridPostDist, string P_PayType, string P_EmpId, string PGrade, string PDesig,
        string PSalHead, string PSector, string Religion, string Fastival, string SalSource, string sQuarter, string sRptType, string sPTax, string sEmpType,
        string sP_Date, string sP_LT, string sP_ComText)
        {
            ddlReportBy.SelectedIndex = 0;
            if (sSearchBy == "1")
                PSearchBy.Visible = true;
            else
                PSearchBy.Visible = false;
            if (sBranch == "1")
                PBranch.Visible = true;
            else
                PBranch.Visible = false;
            if (sDiv == "1")
                PDiv.Visible = true;
            else
                PDiv.Visible = false;
            if (sDept == "1")
                PDept.Visible = true;
            else
                PDept.Visible = false;
            if (sPostingDist == "1")
                PPostingDist.Visible = true;
            else
                PPostingDist.Visible = false;

            if (sDate == "1")
                pDate.Visible = true;
            else
                pDate.Visible = false;

            if (sShow == "1")
                PShow.Visible = true;
            else
                PShow.Visible = false;
            if (sClosed == "1")
                PClosed.Visible = true;
            else
                PClosed.Visible = false;
            if (PFisY == "1")
                this.PFisY.Visible = true;
            else
                this.PFisY.Visible = false;
            if (PMonthFrom == "1")
                this.PMonthFrom.Visible = true;
            else
                this.PMonthFrom.Visible = false;
            if (PYear == "1")
                this.PYear.Visible = true;
            else
                this.PYear.Visible = false;
            if (P_GridSalSubLoc == "1")
                this.PGridSalSubLoc.Visible = true;
            else
                this.PGridSalSubLoc.Visible = false;
            if (PSalaryLocation == "1")
                this.P_SalaryLocation.Visible = true;
            else
                this.P_SalaryLocation.Visible = false;
            if (PSalarySubLocation == "1")
                this.P_SalarySubLocation.Visible = true;
            else
                this.P_SalarySubLocation.Visible = false;
            if (gvPost == "1")
                this.grPostDivision.Visible = true;
            else
                this.grPostDivision.Visible = false;
            if (P_GridEmpList == "1")
                this.PGridEmpList.Visible = true;
            else
                this.PGridEmpList.Visible = false;

            if (P_SCEl == "1")
                this.PSCEl.Visible = true;
            else
                this.PSCEl.Visible = false;
            if (P_AV == "1")
                this.P_AV.Visible = true;
            else
                this.P_AV.Visible = false;
            if (P_GridPostDist == "1")
                this.PGridPostDist.Visible = true;
            else
                this.PGridPostDist.Visible = false;

            if (P_PayType == "1")
                this.P_PayType.Visible = true;
            else
                this.P_PayType.Visible = false;

            if (P_EmpId == "1")
                this.PEmpId.Visible = true;
            else
                this.PEmpId.Visible = false;

            if (PGrade == "1")
                this.PGrade.Visible = true;
            else
                this.PGrade.Visible = false;

            if (PDesig == "1")
                this.PDesig.Visible = true;
            else
                this.PDesig.Visible = false;
            if (PSalHead == "1")
                this.P_SalHead.Visible = true;
            else
                this.P_SalHead.Visible = false;
            if (PSector == "1")
                this.P_Sector.Visible = true;
            else
                this.P_Sector.Visible = false;

            if (Religion == "1")
                this.P_Religion.Visible = true;
            else
                this.P_Religion.Visible = false;

            if (Fastival == "1")
                this.P_Festival.Visible = true;
            else
                this.P_Festival.Visible = false;

            if (SalSource == "1")
                this.P_SalSource.Visible = true;
            else
                this.P_SalSource.Visible = false;

            if (sQuarter == "1")
                this.P_Quarter.Visible = true;
            else
                this.P_Quarter.Visible = false;
            if (sRptType == "1")
                this.P_RptType.Visible = true;
            else
                this.P_RptType.Visible = false;
            if (sPTax == "1")
                this.PanelTax.Visible = true;
            else
                this.PanelTax.Visible = false;
            if (sEmpType == "1")
                this.pnlEmpType.Visible = true;
            else
                this.pnlEmpType.Visible = false;

            if (sP_Date == "1")
                this.P_Date.Visible = true;
            else
                this.P_Date.Visible = false;

            if (sP_LT == "1")
                this.P_LT.Visible = true;
            else
                this.P_LT.Visible = false;

            if (sP_ComText == "1")
                this.P_ComText.Visible = true;
            else
                this.P_ComText.Visible = false;
        }
    }
}