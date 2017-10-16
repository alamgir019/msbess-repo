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
    public partial class ctlLeaveApproval : System.Web.UI.UserControl
    {
        LeaveManager objLeaveMgr = new LeaveManager();
        static string strStartDate = "";
        static string strEndDate = "";
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
        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    this.OpenRecord();

        //    // your code
        //}
        private void OpenRecord()
        {
            grLeaveList.DataSource = null;
            grLeaveList.DataBind();
           
            string strStartYear = DateTime.Now.Year.ToString();
            string strStartMonth = DateTime.Now.Month.ToString();

            if (Convert.ToInt32(strStartMonth) >= 1)
            {
                strStartDate = Convert.ToString(Convert.ToInt32(strStartYear) - 1);
                strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);
                strStartDate = strStartDate + "-" + "07" + "-" + "01";
                strEndDate = strEndDate + "-" + "12" + "-" + "31";
            }
            if (Session["ISADMIN"].ToString() == "N")
            {
                grLeaveList.DataSource = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "R", strStartDate, strEndDate, Session["EMPID"].ToString().Trim());
            }
            else
            {
                grLeaveList.DataSource = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "R", strStartDate, strEndDate, "");
            }

            grLeaveList.DataBind();
            this.FormatGridDate();
        }

        protected void FormatGridDate()
        {
            int SlNo = 0;
            foreach (GridViewRow gRow in grLeaveList.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[0].Text = SlNo.ToString();
                gRow.Cells[1].Text = grLeaveList.DataKeys[SlNo - 1].Values[12].ToString() + " [" + gRow.Cells[1].Text.ToUpper() + "]" +
                    "<br/> Applied For: " + grLeaveList.DataKeys[SlNo - 1].Values[1].ToString() +
                    "<br/> Applied On : " + Common.DisplayDateTime(grLeaveList.DataKeys[SlNo - 1].Values[4].ToString(), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.DisplayDateTime(grLeaveList.DataKeys[SlNo - 1].Values[5].ToString(), false, Constant.strDateFormat) +
                  " To " + Common.DisplayDateTime(grLeaveList.DataKeys[SlNo - 1].Values[6].ToString(), false, Constant.strDateFormat) +
                  "<br/> Duration : " + Convert.ToString(Math.Round(Convert.ToDouble(grLeaveList.DataKeys[SlNo - 1].Values[7].ToString()), 1));
            }
            SlNo = 0;
        }

        protected void grLeaveApp_RowCommand(object sender, GridViewCommandEventArgs e)
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
                   string strURL = "LeaveApplicationView.aspx?params=" + grLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString() + "," + grLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + ", R"+", L";
                   // string strURL = "LeaveApplicationView.aspx";
                    sb.Append("<script>");

                    sb.Append("window.open('" + strURL + "', '', '');");
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                             sb.ToString(), false);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                    break;

                case ("ApproveClick"):
                    this.AvailableLeave("A", grLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), grLeaveList.DataKeys[_gridView.SelectedIndex].Values[2].ToString(), _gridView.SelectedIndex);
                    this.GetLeaveDates(grLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), "A", grLeaveList.DataKeys[_gridView.SelectedIndex].Values[4].ToString(), grLeaveList.DataKeys[_gridView.SelectedIndex].Values[5].ToString());
                    objLeaveMgr.UpdateLeaveAppMstForApprove(grLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                        grLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "A", hfLEnjoyed.Value.ToString(), hfLDates.Value.ToString(),
                        grLeaveList.DataKeys[_gridView.SelectedIndex].Values[10].ToString(), grLeaveList.DataKeys[_gridView.SelectedIndex].Values[2].ToString(),
                        grLeaveList.DataKeys[_gridView.SelectedIndex].Values[9].ToString(),
                        Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false,Constant.strDateFormat), strPreYrLv, grLeaveList.DataKeys[_gridView.SelectedIndex].Values[7].ToString());

                    //        ////Email Notification                
                    //        //lblMsg.Text = objMail.LeaveApprovalBySupervisor(grLeaveApp.SelectedRow.Cells[1].Text.Trim(), grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                    //        //      Session["EMPID"].ToString(), Session["USERNAME"].ToString(),
                    //        //      Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                    //        //      Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", Session["EMAILID"].ToString());
                    //        if (lblMsg.Text == "")
                    //            lblMsg.Text = "Leave has been approved successfully";
                    //        //lblMsg.Text = "Leave has been approved and mailed successfully"; 
                    SiteMaster.ShowClientMessage(Page, "Leave has been approved successfully", "success");
                    break;

                  case ("DenyClick"):
                    AvailableLeave("A", grLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), grLeaveList.DataKeys[_gridView.SelectedIndex].Values[2].ToString(), _gridView.SelectedIndex);
                    this.GetLeaveDates(grLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), "A", grLeaveList.DataKeys[_gridView.SelectedIndex].Values[4].ToString(), grLeaveList.DataKeys[_gridView.SelectedIndex].Values[5].ToString());
                    //CalculateLeaveDates("A");
                    //this.GetWeekend(grLeaveApp.SelectedRow.Cells[1].Text.Trim(), grLeaveApp.SelectedRow.Cells[4].Text.Trim(), grLeaveApp.SelectedRow.Cells[5].Text.Trim(),"A"); 
                    objLeaveMgr.UpdateLeaveAppMstForDeny(grLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                        grLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "D",
                        Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false,Constant.strDateFormat));

                    ////Email Notification
                    //lblMsg.Text = objMail.LeaveRegretBySupervisor(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), 
                    //    grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), Session["EMPID"].ToString(), 
                    //    Session["USERNAME"].ToString(), Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                    //      Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", Session["EMAILID"].ToString());
                    //if (lblMsg.Text == "")
                    //    lblMsg.Text = "Leave has been Regreted Successfully.";
                    //lblMsg.Text = "Leave has been Regreted and Mailed Successfully";
                    SiteMaster.ShowClientMessage(Page, "Leave has been Regreted Successfully.", "success");
                    break;

                case ("CancelClick"):
                    AvailableLeave("A", grLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), grLeaveList.DataKeys[_gridView.SelectedIndex].Values[2].ToString(), _gridView.SelectedIndex);
                    this.GetLeaveDates(grLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), "A", grLeaveList.DataKeys[_gridView.SelectedIndex].Values[4].ToString(), grLeaveList.DataKeys[_gridView.SelectedIndex].Values[5].ToString());
                    //CalculateLeaveDates("A");
                    //this.GetWeekend(grLeaveApp.SelectedRow.Cells[1].Text.Trim(), grLeaveApp.SelectedRow.Cells[4].Text.Trim(), grLeaveApp.SelectedRow.Cells[5].Text.Trim(),"A"); 
                    objLeaveMgr.CancelLeaveApp(grLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                        grLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "C",
                        Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime( DateTime.Now.ToString(), false, Constant.strDateFormat), false,Constant.strDateFormat));

                    ////Email Notification
                    //lblMsg.Text = objMail.LeaveCancel(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(),
                    //    grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), Session["EMPID"].ToString(),
                    //    Session["USERNAME"].ToString(), Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                    //      Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", Session["EMAILID"].ToString());
                    
                    SiteMaster.ShowClientMessage(Page, "Leave has been Cancelled Successfully.", "success");              
                    break;
            }

                    this.OpenRecord();
                    strPreYrLv = "";
            }
        private void AvailableLeave(string gridStatus, string strEmpID, string strLTypeID,Int32 intSelectedRowIndex)
        {
            DataTable dtLeaveProfile = new DataTable();
            if (gridStatus == "A")
            {
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLTypeID);
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                        hfLEnjoyed.Value = Convert.ToString(Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(grLeaveList.DataKeys[intSelectedRowIndex].Values[7].ToString().Trim()));
                    else
                        hfLEnjoyed.Value = grLeaveList.DataKeys[intSelectedRowIndex].Values[7].ToString().Trim();
                }
            }
            else if (gridStatus == "D")
            {
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLTypeID);
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                        hfLEnjoyed.Value = Convert.ToString(Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(grLeaveList.DataKeys[intSelectedRowIndex].Values[7].ToString().Trim()));
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
                        hfLDates.Value = hfLDates.Value + "," + Common.ReturnDateTimeInString(Common.DisplayDateTime(dRow["LevDate"].ToString(), false, Constant.strDateFormat), false,Constant.strDateFormat);
                    else
                        hfLDates.Value = Common.ReturnDateTimeInString(Common.DisplayDateTime(dRow["LevDate"].ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                }
            }
        }
    }
}