using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Data;

namespace WebAdmin.UserControls.Leave
{
    public partial class ctlLeaveStatement : System.Web.UI.UserControl
    {
        LeaveManager objLeaveMgr = new LeaveManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetData(Session["EMPID"].ToString().Trim());
                lblPrintDate.Text = Common.DisplayDateTime(DateTime.Today.ToShortDateString(),false,Constant.strDateFormat);
            }
        }
        protected void GetData(string strEmpID)
        {
            DataTable dtEmp = objLeaveMgr.SelectEmpInfoForLeave(strEmpID);
            if (dtEmp.Rows.Count > 0)
            {
                lblEmpID.Text = strEmpID.Trim();
                lblEmpName.Text = dtEmp.Rows[0]["FullName"].ToString();
                lblPosition.Text = dtEmp.Rows[0]["DesigName"].ToString();
                lblOffice.Text = dtEmp.Rows[0]["DivisionName"].ToString();
                lblJoinDate.Text = Common.DisplayDateTime(dtEmp.Rows[0]["JoiningDate"].ToString(), false, Constant.strDateFormat);
                // strEmpSex = dtEmp.Rows[0]["Gender"].ToString();
                this.GetLeaveBalance(strEmpID, dtEmp.Rows[0]["Gender"].ToString().Trim());
            }
        }
        protected void GetLeaveBalance(string strEmpID, string strSex)
        {
            DataTable dtLeaveBalance = objLeaveMgr.SelectEmpLeaveProfileEXCPL(strEmpID, "0", strSex);
            if (dtLeaveBalance.Rows.Count > 0)
            {
                grLeaveBalance.DataSource = dtLeaveBalance;
                grLeaveBalance.DataBind();
                this.FormatLeaveStatusGridNumber();

                this.GetLeaveHistory(strEmpID,
                   dtLeaveBalance.Rows[0]["LeaveStartPeriod"].ToString().Trim(),
                   dtLeaveBalance.Rows[0]["LeaveEndPeriod"].ToString().Trim());
            }
        }
        protected void GetLeaveHistory(string strEmpID, string strLvStartPeriod, string strLvEndPeriod)
        {
            lblLvStartPeriod.Text = Common.DisplayDateTime(strLvStartPeriod, false, Constant.strDateFormat);
            lblLvEndPeriod.Text = Common.DisplayDateTime(strLvEndPeriod, false, Constant.strDateFormat);

            //strLvStartPeriod = Common.DisplayDateTime(strLvStartPeriod, false, Constant.strDateFormat);
            //strLvEndPeriod = Common.DisplayDateTime(strLvEndPeriod, false, Constant.strDateFormat);


            DataTable dtLeaveHistory = objLeaveMgr.SelectEmpLeaveDetails(strEmpID, strLvStartPeriod, strLvEndPeriod);

            if (dtLeaveHistory.Rows.Count > 0)
            {
                grLeaveHistory.DataSource = null;
                grLeaveHistory.DataBind();


                grLeaveHistory.DataSource = dtLeaveHistory;
                grLeaveHistory.DataBind();
                this.FormatLeaveDetailsGridNumber();
            }
        }
        protected void FormatLeaveStatusGridNumber()
        {
            int i = 0;
            foreach (GridViewRow gRow in grLeaveBalance.Rows)
            {
                gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1));
                gRow.Cells[2].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
                gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
                gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1) + (Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1)) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)));

                if (Convert.ToDecimal(gRow.Cells[4].Text) < 0)
                {
                    gRow.Cells[4].Text = "0";
                }
                i++;
            }
        }

        protected void FormatLeaveDetailsGridNumber()
        {
            int i = 1;
            foreach (GridViewRow gRow in grLeaveHistory.Rows)
            {
                gRow.Cells[0].Text = i.ToString();
                gRow.Cells[2].Text = Common.DisplayDateTime(gRow.Cells[2].Text.Trim(), false, Constant.strDateFormat);
                gRow.Cells[3].Text = Common.DisplayDateTime(gRow.Cells[3].Text.Trim(), false, Constant.strDateFormat);
                gRow.Cells[4].Text = Common.DisplayDateTime(gRow.Cells[4].Text.Trim(), false, Constant.strDateFormat);

                if (Common.CheckNullString(gRow.Cells[5].Text) != "")
                    gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDecimal(gRow.Cells[5].Text), 1));

                if (gRow.Cells[6].Text.Trim() == "A")
                    gRow.Cells[6].Text = "Approved";
                else if(gRow.Cells[6].Text.Trim() == "P")
                    gRow.Cells[6].Text = "Requested";
                else if (gRow.Cells[6].Text.Trim() == "R")
                    gRow.Cells[6].Text = "Recommended";
                else if (gRow.Cells[6].Text.Trim() == "D")
                    gRow.Cells[6].Text = "Regretted";
                else if (gRow.Cells[6].Text.Trim() == "C")
                    gRow.Cells[6].Text = "Cancelled";

                if (Common.CheckNullString(gRow.Cells[7].Text) != "")
                    gRow.Cells[7].Text = gRow.Cells[7].Text;

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[8].Text)) == false)
                    gRow.Cells[8].Text = Common.DisplayDateTime(gRow.Cells[8].Text.Trim(), false, Constant.strDateFormat);
                i++;
            }
        }
    }
}