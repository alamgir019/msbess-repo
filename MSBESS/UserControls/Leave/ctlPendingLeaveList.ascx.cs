using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Threading;
using System.Data;
using System.Text;

namespace WebAdmin.UserControls.Leave
{
    public partial class ctlPendingLeaveList : System.Web.UI.UserControl
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
            grPendingLeaveList.DataSource = null;
            grPendingLeaveList.DataBind();

            string strStartDate = DateTime.Now.Year.ToString();
            string strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);

            strStartDate = strStartDate + "-" + "01" + "-" + "01";
            strEndDate = strEndDate + "-" + "12" + "-" + "31";
            DataTable dtLeaveDeny = new DataTable();
            if (Session["ISADMIN"].ToString() == "N")
            {
                //dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "P", strStartDate, strEndDate, Session["EMPID"].ToString().Trim());
                dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, Session["EMPID"].ToString().Trim(), "PRDCA", strStartDate, strEndDate, "");
            }
            else
            {
               // dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "P", strStartDate, strEndDate, "");
                dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "N", "PR", strStartDate, strEndDate, "");
            }
            
            grPendingLeaveList.DataSource = dtLeaveDeny;
            grPendingLeaveList.DataBind();
            this.FormatDenyGridDate();
            dtLeaveDeny.Rows.Clear();
            dtLeaveDeny.Dispose();
        }
        protected void FormatDenyGridDate()
        {
            int SlNo = 0;
            foreach (GridViewRow gRow in grPendingLeaveList.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[0].Text = SlNo.ToString();
                gRow.Cells[1].Text = grPendingLeaveList.DataKeys[SlNo - 1].Values[12].ToString() + " [" + gRow.Cells[1].Text.ToUpper() + "]" +
                    "<br/> Applied For: " + grPendingLeaveList.DataKeys[SlNo - 1].Values[1].ToString() +
                    "<br/> Applied On : " + Common.DisplayDateTime(grPendingLeaveList.DataKeys[SlNo - 1].Values[4].ToString(), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.DisplayDateTime(grPendingLeaveList.DataKeys[SlNo - 1].Values[5].ToString(), false, Constant.strDateFormat) +
                  " To " + Common.DisplayDateTime(grPendingLeaveList.DataKeys[SlNo - 1].Values[6].ToString(), false, Constant.strDateFormat) +
                  "<br/> Duration : " + Convert.ToString(Math.Round(Convert.ToDouble(grPendingLeaveList.DataKeys[SlNo - 1].Values[7].ToString()), 1));
            }
            SlNo = 0;
        }
        protected void grPendingLeaveList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;
            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            _gridView.SelectedIndex = _selectedIndex;
           // string strPreYrLv = "";
            switch (_commandName)
            {
                case ("ViewClick"):
                    StringBuilder sb = new StringBuilder();
                    string strURL = "LeaveApplicationView.aspx?params=" + grPendingLeaveList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim() + "," + grPendingLeaveList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "," + grPendingLeaveList.DataKeys[_gridView.SelectedIndex].Values[14].ToString().Trim() + ", A"; ;
                    // string strURL = "LeaveApplicationView.aspx";
                    sb.Append("<script>");

                    sb.Append("window.open('" + strURL + "', '', '');");
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                             sb.ToString(), false);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                    break;   
            }

            this.OpenRecord();
            //strPreYrLv = "";
        }
    }
}