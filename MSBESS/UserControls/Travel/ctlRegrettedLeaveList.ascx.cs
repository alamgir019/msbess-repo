using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Data;
using System.Text;
using System.Threading;

namespace WebAdmin.UserControls.Leave
{  
    public partial class ctlRegrettedLeaveList : System.Web.UI.UserControl
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
            grRegrettedLeaveList.DataSource = null;
            grRegrettedLeaveList.DataBind();

            string strStartDate = DateTime.Now.Year.ToString();
            string strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);

            strStartDate = strStartDate + "-" + "01" + "-" + "01";
            strEndDate = strEndDate + "-" + "12" + "-" + "31";
            DataTable dtLeaveDeny = new DataTable();
            if (Session["ISADMIN"].ToString() == "N")
            {
                dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "D", strStartDate, strEndDate, Session["EMPID"].ToString().Trim());
                //dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, Session["EMPID"].ToString().Trim(), "D", strStartDate, strEndDate, "");
            }
            else
            {
                dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "D", strStartDate, strEndDate, "");
                //dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "N", "D", strStartDate, strEndDate, "");
            }

            grRegrettedLeaveList.DataSource = dtLeaveDeny;
            grRegrettedLeaveList.DataBind();
            this.FormatDenyGridDate();
            dtLeaveDeny.Rows.Clear();
            dtLeaveDeny.Dispose();
        }
        protected void FormatDenyGridDate()
        {
            int SlNo = 0;
            foreach (GridViewRow gRow in grRegrettedLeaveList.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[0].Text = SlNo.ToString();
                gRow.Cells[1].Text = grRegrettedLeaveList.DataKeys[SlNo - 1].Values[12].ToString() + " [" + gRow.Cells[1].Text.ToUpper() + "]" +
                    "<br/> Applied For: " + grRegrettedLeaveList.DataKeys[SlNo - 1].Values[1].ToString() +
                    "<br/> Applied On : " + Common.DisplayDateTime(grRegrettedLeaveList.DataKeys[SlNo - 1].Values[4].ToString(), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.DisplayDateTime(grRegrettedLeaveList.DataKeys[SlNo - 1].Values[5].ToString(), false, Constant.strDateFormat) +
                  " To " + Common.DisplayDateTime(grRegrettedLeaveList.DataKeys[SlNo - 1].Values[6].ToString(), false, Constant.strDateFormat) +
                  "<br/> Duration : " + Convert.ToString(Math.Round(Convert.ToDouble(grRegrettedLeaveList.DataKeys[SlNo - 1].Values[7].ToString()), 1));
            }
            SlNo = 0;
        }
        private void AvailableLeave(string gridStatus, string strEmpID, string strLTypeID, Int32 intSelectedRowIndex)
        {
            DataTable dtLeaveProfile = new DataTable();
            if (gridStatus == "A")
            {
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLTypeID);
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                        hfLEnjoyed.Value = Convert.ToString(Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(grRegrettedLeaveList.DataKeys[intSelectedRowIndex].Values[7].ToString().Trim()));
                    else
                        hfLEnjoyed.Value = grRegrettedLeaveList.DataKeys[intSelectedRowIndex].Values[7].ToString().Trim();
                }
            }
            else if (gridStatus == "D")
            {
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLTypeID);
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                        hfLEnjoyed.Value = Convert.ToString(Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(grRegrettedLeaveList.DataKeys[intSelectedRowIndex].Values[7].ToString().Trim()));
                    else
                        hfLEnjoyed.Value = "0";
                }
            }
        }
        protected void GetLeaveDates(string strLvAppId, string gridStatus, string strDateFrom, string strDateTo)
        {
            string strFromDate = "";
            string strToDate = "";
            if (gridStatus == "A")
            {
                strFromDate = strDateFrom;
                strToDate = strDateTo;
            }
            else if (gridStatus == "D")
            {
                strFromDate = strDateFrom;
                strToDate = strDateTo;
            }
            else if (gridStatus == "AC")
            {
                strFromDate = strDateFrom;
                strToDate = strDateTo;
            }

            DataTable dtLeaveDates = new DataTable();
            dtLeaveDates = objLeaveMgr.GetLeaveDates(strLvAppId);
            if (dtLeaveDates.Rows.Count > 0)
            {
                hfLDates.Value = "";
                foreach (DataRow dRow in dtLeaveDates.Rows)
                {
                    if (hfLDates.Value != "")
                        hfLDates.Value = hfLDates.Value + "," + Common.ReturnDateTimeInString(Common.DisplayDateTime(dRow["LevDate"].ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                    else
                        hfLDates.Value = Common.ReturnDateTimeInString(Common.DisplayDateTime(dRow["LevDate"].ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                }
            }
        }
        protected void grRegrettedLeaveList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    string strURL = "LeaveApplicationView.aspx?params=" + grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim() + "," + grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + ", D" + ", R"; ;
                    // string strURL = "LeaveApplicationView.aspx";
                    sb.Append("<script>");

                    sb.Append("window.open('" + strURL + "', '', '');");
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                             sb.ToString(), false);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                    break;

                case ("CancelClick"):
                    AvailableLeave("A", grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[2].ToString(), _gridView.SelectedIndex);
                    this.GetLeaveDates(grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), "A", grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[4].ToString(), grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[5].ToString());
                    //CalculateLeaveDates("A");
                    //this.GetWeekend(grLeaveApp.SelectedRow.Cells[1].Text.Trim(), grLeaveApp.SelectedRow.Cells[4].Text.Trim(), grLeaveApp.SelectedRow.Cells[5].Text.Trim(),"A"); 
                    objLeaveMgr.CancelLeaveApp(grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                        grRegrettedLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "C",
                        Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat));

                    SiteMaster.ShowClientMessage(Page, "Leave has been Cancelled Successfully.", "success");
                    break;
            }

            this.OpenRecord();
            strPreYrLv = "";
        }
    }
}