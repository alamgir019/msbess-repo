using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using WebAdmin.BLL;
using System.Threading;

namespace WebAdmin.UserControls.Leave
{
    public partial class ctlApprovedLeaveList : System.Web.UI.UserControl
    {
        LeaveManager objLeaveMgr = new LeaveManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null)
                {
                    // ShowClientMessage(Page, "Session Expired. You are redirected to Login Again!", "error");
                    Thread.Sleep(1000);
                    Response.Redirect("~/Login.aspx");
                }
                this.OpenRecord();
            }
        }
        private void OpenRecord()
        {
            grApproveLeaveList.DataSource = null;
            grApproveLeaveList.DataBind();

            string strStartDate = DateTime.Now.Year.ToString();
            string strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);

            strStartDate = strStartDate + "-" + "01" + "-" + "01";
            strEndDate = strEndDate + "-" + "12" + "-" + "31";
            DataTable dtLeaveDeny = new DataTable();
            if (Session["ISADMIN"].ToString() == "N")
            {
                dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "A", strStartDate, strEndDate, Session["EMPID"].ToString().Trim());
                //dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, Session["EMPID"].ToString().Trim(), "A", strStartDate, strEndDate, "");
            }
            else
            {
                dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "A", strStartDate, strEndDate, "");
                //dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "N", "A", strStartDate, strEndDate, "");
            }

            grApproveLeaveList.DataSource = dtLeaveDeny;
            grApproveLeaveList.DataBind();
            this.FormatDenyGridDate();
            dtLeaveDeny.Rows.Clear();
            dtLeaveDeny.Dispose();
        }
        protected void FormatDenyGridDate()
        {
            int SlNo = 0;
            foreach (GridViewRow gRow in grApproveLeaveList.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[0].Text = SlNo.ToString();
                gRow.Cells[1].Text = grApproveLeaveList.DataKeys[SlNo - 1].Values[12].ToString() + " [" + gRow.Cells[1].Text.ToUpper() + "]" +
                    "<br/> Applied For: " + grApproveLeaveList.DataKeys[SlNo - 1].Values[1].ToString() +
                    "<br/> Applied On : " + Common.DisplayDateTime(grApproveLeaveList.DataKeys[SlNo - 1].Values[4].ToString(), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.DisplayDateTime(grApproveLeaveList.DataKeys[SlNo - 1].Values[5].ToString(), false, Constant.strDateFormat) +
                  " To " + Common.DisplayDateTime(grApproveLeaveList.DataKeys[SlNo - 1].Values[6].ToString(), false, Constant.strDateFormat) +
                  "<br/> Duration : " + Convert.ToString(Math.Round(Convert.ToDouble(grApproveLeaveList.DataKeys[SlNo - 1].Values[7].ToString()), 1));
            }
            SlNo = 0;
        }
        protected void grApproveLeaveList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;
            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            _gridView.SelectedIndex = _selectedIndex;
            string strPreYrLv = "";
            switch (_commandName)
            {
                case ("ViewClick"):
                    StringBuilder sb = new StringBuilder();
                    string strURL = "LeaveApplicationView.aspx?params=" + grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim() + "," + grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + ", A" + ", A"; ;
                    // string strURL = "LeaveApplicationView.aspx";
                    sb.Append("<script>");

                    sb.Append("window.open('" + strURL + "', '', '');");
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                             sb.ToString(), false);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                    break;

                    //case ("CancelClick"):
                    //    AvailableLeave("A", grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[2].ToString(), _gridView.SelectedIndex);
                    //    this.GetLeaveDates(grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), "A", grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[4].ToString(), grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[5].ToString());
                    //    //CalculateLeaveDates("A");
                    //    //this.GetWeekend(grLeaveApp.SelectedRow.Cells[1].Text.Trim(), grLeaveApp.SelectedRow.Cells[4].Text.Trim(), grLeaveApp.SelectedRow.Cells[5].Text.Trim(),"A"); 
                    //    objLeaveMgr.CancelLeaveApp(grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                    //        grApproveLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "C",
                    //        Session["USERID"].ToString(), Common.ReturnDateTimeInString(DateTime.Now.ToString(), false, Constant.strDateFormat));

                    //    SiteMaster.ShowClientMessage(Page, "Leave has been Cancelled Successfully.", "success");
                    //    break;
            }

            this.OpenRecord();
            strPreYrLv = "";
        }
    }
}