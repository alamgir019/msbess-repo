using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Text;

namespace WebAdmin.UserControls.Report
{
    public partial class ctlReportViewer : System.Web.UI.UserControl
    {
        EmpInfoManager empManager = new EmpInfoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Common.FillYearList(5, ddlYear);
                DateTime now = DateTime.Now;
                ddlYear.SelectedValue = Convert.ToInt32(now.Year).ToString();
                Common.FillMonthList(ddlMonthFrm);
                ddlMonthFrm.SelectedValue = Convert.ToInt32(now.Month).ToString();
                txtDesig.Text = Session["DESIGNATION"].ToString();
                txtEmpCode.Text = Session["EMPID"].ToString();
                if (Session["REPORTID"].ToString()=="PS")
                {
                Common.FillDropDownList(empManager.SelectFiscalYear(0, "FA"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false, "-1");
                    PanelVisibilityMst("0", "1", "0", "0", "0", "1", "0", "0", "1", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                }
                else if (Session["REPORTID"].ToString() == "PF")
                {
                Common.FillDropDownList(empManager.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false, "-1");
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                }
                else if (Session["REPORTID"].ToString() == "ATD")
                {
                    Common.FillDropDownList(empManager.SelectFiscalYear(0, "P"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false, "-1");
                    PanelVisibilityMst("0", "0", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                }

                else if (Session["REPORTID"].ToString() == "ITA")
                {
                    Common.FillDropDownList(empManager.SelectFiscalYear(0, "T"), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false, "-1");
                    PanelVisibilityMst("0", "0", "0", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                }
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            Session["SalDiv"] = ddlDivision.SelectedValue.ToString();
            Session["FisYearText"] = ddlFisYear.SelectedItem.Text.ToString();
            Session["FisYear"] = ddlFisYear.SelectedValue.ToString();
            Session["VMonth"] = ddlMonthFrm.SelectedValue.ToString();
            Session["VYear"] = ddlYear.SelectedValue.ToString();


            Session["Flag"] = "E";
            Session["FromDate"] = txtFromDate.Text;
            Session["ToDate"] = txtToDate.Text;
            Session["DivisionId"] = ddlDivision.SelectedValue.ToString();
            Session["SbuId"] = "-1";
            Session["DeptId"] = ddlDept.SelectedValue.ToString();
            Session["EmpId"] = txtEmpCode.Text.Trim();
            Session["ShiftID"] = "-1";
            Session["IsClosed"] = ddlIsClosed.SelectedValue.ToString();

            //Open New Window
            StringBuilder sb = new StringBuilder();

            sb.Append("<script>");
            sb.Append("window.open('PayrollReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
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