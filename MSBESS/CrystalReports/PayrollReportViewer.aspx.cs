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

namespace WebAdmin.CrystalReports
{
    public partial class PayrollReportViewer : System.Web.UI.Page
    {
        private ReportDocument ReportDoc;
        private string ReportPath = "";
        private string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];
        DataTable MyDataTable = new DataTable();
        ReportManager rptManager = new ReportManager();
        CashInWord InWord = new CashInWord();

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
            string desigId = Session["DESIGID"].ToString() == "" ? "-1" : Session["DESIGID"].ToString();
            switch (Session["REPORTID"].ToString())
            {
                case "PS":
                    ReportPath = Server.MapPath("~/CrystalReports/rptSalPaySlipAll.rpt");
                    ReportDoc.Load(ReportPath);
                    string month = Session["VMonth"].ToString();
                    MyDataTable = rptManager.Get_PayslipMonthlyAll(Session["FisYear"].ToString(), month, Session["VYear"].ToString(), Session["EmpID"].ToString(), desigId, Session["SalDiv"].ToString());
                    DataTable dtPaySlipAll = (DataTable)MyDataTable;
                    if (dtPaySlipAll.Rows.Count > 0)
                    {
                        dtPaySlipAll.Columns.Add(new DataColumn("TakaInWord", typeof(string)));
                        foreach (DataRow dRow in dtPaySlipAll.Rows)
                        {
                            dRow["TakaInWord"] = InWord.getCashWord(dRow["NetSal"].ToString().Trim());
                        }
                    }
                    ReportDoc.SetDataSource(dtPaySlipAll);
                    ReportDoc.SetParameterValue("P_Header", "Salary/Wages for the month of-- " + Common.ReturnMonthNameFull(Session["VMonth"].ToString()) + ", " + Session["VYear"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                case "PF":
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/rptYearlyPFBalance.rpt");
                        ReportDoc.Load(ReportPath);
                        MyDataTable = rptManager.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "YPFC", "-1"); //Session["YearlyType"].ToString()
                        ReportDoc.SetDataSource(MyDataTable);
                        ReportDoc.SetParameterValue("P_Header", "Yearly PF Contribution For The Fiscal Year " + Session["FisYearText"].ToString());
                        ReportDoc.SetParameterValue("ComLogo", LogoPath);
                        CRVT.ReportSource = ReportDoc;
                        break;
                    }
                case "ATD":
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/rptAttandance.rpt");
                        ReportDoc.Load(ReportPath);

                        MyDataTable = rptManager.Get_Attandance(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                            Session["DivisionId"].ToString(), Session["SbuId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(),
                            Session["ShiftID"].ToString(), Session["isClosed"].ToString());
                        
                        ReportDoc.SetDataSource(MyDataTable);
                        ReportDoc.SetParameterValue("pDIV", Session["DivisionId"].ToString());
                        ReportDoc.SetParameterValue("pSBU",Session["SbuId"].ToString());
                        ReportDoc.SetParameterValue("pDEP", Session["DeptId"].ToString().Trim());
                        ReportDoc.SetParameterValue("FromDate",Session["FromDate"].ToString());
                        ReportDoc.SetParameterValue("ToDate",Session["ToDate"].ToString());
                        ReportDoc.SetParameterValue("pHeader", "Attendance Report");
                        ReportDoc.SetParameterValue("ComLogo", LogoPath);
                        CRVT.ReportSource = ReportDoc;
                        break;
                    }
                case "ITA":
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/rptITAssessment.rpt");
                        ReportDoc.Load(ReportPath);
                        MyDataTable = rptManager.Get_ITAssessment(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["EmpID"].ToString());
                        ReportDoc.SetDataSource(MyDataTable);
                        //ReportDoc.SetParameterValue("P_EmpId", MyDataTable.Rows[0]["EmpId"].ToString().Trim());
                        ReportDoc.SetParameterValue("P_Header", "Income Tax Assessment");
                        ReportDoc.SetParameterValue("P_FiscalYear", "Income Year :" + Session["FisYearText"].ToString());
                        ReportDoc.SetParameterValue("P_HouseRentEx", 300000);
                        ReportDoc.SetParameterValue("P_MedicalEx", 120000);
                        ReportDoc.SetParameterValue("P_TransportEx", 30000);
                        ReportDoc.SetParameterValue("ComLogo", LogoPath);
                        CRVT.ReportSource = ReportDoc;
                        break;
                    }
                case "test":
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/CrystalReport1.rpt");
                        ReportDoc.Load(ReportPath);
                        ReportDoc.SetParameterValue("P_Header", "Income Tax Assessment");
                        CRVT.ReportSource = ReportDoc;
                        break;
                    }
            }
        }
        //protected string CountStatus(string strStatus, DataTable dt)

        //{
        //    //useless function
        //    string strExpr = "Status='" + strStatus + "'";
        //    DataRow[] foundRows = dt.Select(strExpr);
        //    return Convert.ToString(foundRows.Length);
        //}
        //public void PassParameterHeader(string ReportName)
        //{
        //    ParameterFields pFields = new ParameterFields();
        //    ParameterField pfHeader = new ParameterField();
        //    ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();

        //    pfHeader.Name = "pHeader";
        //    dvHeader.Value = ReportName;
        //    pfHeader.CurrentValues.Add(dvHeader);

        //    pFields.Add(pfHeader);

        //    CRVT.ParameterFieldInfo = pFields;
        //}

        //public void PassParameterHeader(string ReportName, string FiscalYr)
        //{
        //    ParameterFields pFields = new ParameterFields();
        //    ParameterField pfHeader = new ParameterField();
        //    ParameterField pfFiscalYr = new ParameterField();

        //    ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        //    ParameterDiscreteValue dvFiscalYr = new ParameterDiscreteValue();

        //    pfHeader.Name = "pHeader";
        //    dvHeader.Value = ReportName;
        //    pfHeader.CurrentValues.Add(dvHeader);

        //    pfFiscalYr.Name = "pFiscalYr";
        //    dvFiscalYr.Value = FiscalYr;
        //    pfFiscalYr.CurrentValues.Add(dvFiscalYr);

        //    pFields.Add(pfHeader);
        //    pFields.Add(pfFiscalYr);

        //    CRVT.ParameterFieldInfo = pFields;
        //}


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