using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Data;

namespace WebAdmin.UserControls.Payroll
{
    public partial class ctlMonthlyPayslip : System.Web.UI.UserControl
    {
        EmpInfoManager objEmp = new EmpInfoManager();
        PayrollReportManager objPayRptMgr = new PayrollReportManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common.FillMonthList(ddlMonth);
                Common.FillYearList(5, ddlYear);
                ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
                ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);

                DataTable dtPersonal = objEmp.SelectEmpPayslipPersonalInfo(Session["EMPID"].ToString().Trim());
                if (dtPersonal.Rows.Count > 0)
                {
                    // FILL EMPLOYEE PERSONAL INFO
                    Common.PrepareEditView(dtPersonal.Rows[0], this.Controls);
                }
               // this.OpenRecord(rdbSalaryType.SelectedValue.ToString());
            }
        }
        private void OpenRecord(string strSalaryType)
        {
            if (strSalaryType == "S")
                lblMonth.Text = "Payslip for the Month of " + ddlMonth.SelectedItem.ToString().Trim() + ", " + ddlYear.SelectedValue.ToString().Trim();
            else
                lblMonth.Text = "Festival Allowance - " + ddlMonth.SelectedItem.ToString().Trim() + ", " + ddlYear.SelectedValue.ToString().Trim();


            DataTable dt = new DataTable();
            dt = (DataTable)objPayRptMgr.GetPayslipMonthlyGrossAndBenefits(lblEmpId.Text.Trim(), ddlMonth.SelectedValue.ToString().Trim(), ddlYear.SelectedValue.ToString().Trim(), strSalaryType);

            grGrossandBenefits.DataSource = dt;
            grGrossandBenefits.DataBind();
            clsCashWord cal = new clsCashWord();
            int nouOfRow = dt.Rows.Count;
            if (string.IsNullOrEmpty(dt.Rows[nouOfRow - 1]["PAYAMT"].ToString().Trim()) == false)
                lblTakaInWord.Text = "<b>In Words : </b>" + cal.getCashWord(Common.ReturnZeroForNull(dt.Rows[nouOfRow - 1]["PAYAMT"].ToString().Trim())); //nouOfRow.ToString();

            dt.Rows.Clear();
            lblRemarks.Text = objPayRptMgr.GetPayrollRemarksForPayslip(lblEmpId.Text.Trim(), ddlMonth.SelectedValue.ToString().Trim(), ddlYear.SelectedValue.ToString().Trim(), strSalaryType);
            if (string.IsNullOrEmpty(lblRemarks.Text) == false)
                lblRemarks.Text = "* " + lblRemarks.Text;

            this.FormatGrossAndBenefitsGrid();
            this.FormatDeductionGrid();
        }
        protected void FormatGrossAndBenefitsGrid()
        {
            foreach (GridViewRow gRow in grGrossandBenefits.Rows)
            {
                if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "A")
                {
                    gRow.Font.Bold = true;
                }
                if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "B")
                {
                    gRow.Font.Bold = true;
                }
                if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "Y")
                {
                    gRow.Cells[1].Text = "";
                    gRow.Font.Bold = true;
                }
            }
        }
        protected void FormatDeductionGrid()
        {
            foreach (GridViewRow gRow in grDeduct.Rows)
            {
                if (grDeduct.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "C")
                {
                    gRow.Font.Bold = true;
                }
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            this.OpenRecord(rdbSalaryType.SelectedValue.ToString());
        }
    }
}
