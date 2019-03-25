using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebAdmin.BLL;
using System.Threading;
using System.Text;
using CrystalDecisions.Shared;

namespace WebAdmin.UserControls.Travel
{
    public partial class ctlTravelRecommendation : System.Web.UI.UserControl
    {
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        EmpTravelManager objTravelMgr = new EmpTravelManager();
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
            grRecommendTravelList.DataSource = null;
            grRecommendTravelList.DataBind();

            string strStartDate = DateTime.Now.Year.ToString();
            string strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);

            strStartDate = strStartDate + "-" + "01" + "-" + "01";
            strEndDate = strEndDate + "-" + "12" + "-" + "31";
            DataTable dtTravelStatus = new DataTable();
            //if (Session["ISADMIN"].ToString() == "N")
            //{
                //dtTravelStaus = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "P", strStartDate, strEndDate, Session["EMPID"].ToString().Trim());
            dtTravelStatus = objTravelMgr.GetTravelApp(0, "", "P", strStartDate, strEndDate, Session["EMPID"].ToString().Trim());
            //}
            //else
            //{
                //dtTravelStaus = objLeaveMgr.SelectRequestLeaveAppMst(0, "", "P", strStartDate, strEndDate, "");
                //dtTravelStatus = objTravelMgr.GetTravelApp(0, "", "P", strStartDate, strEndDate, "");
            //}
            //ViewState["TravelStatus"] = dtTravelStatus;
            grRecommendTravelList.DataSource = dtTravelStatus;
            grRecommendTravelList.DataBind();
            this.FormatDenyGridDate();
            dtTravelStatus.Rows.Clear();
            dtTravelStatus.Dispose();
        }
        protected void FormatDenyGridDate()
        {
            int SlNo = 0;
            foreach (GridViewRow gRow in grRecommendTravelList.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[0].Text = SlNo.ToString();
                gRow.Cells[1].Text = grRecommendTravelList.DataKeys[SlNo - 1].Values[11].ToString() + " [" + gRow.Cells[1].Text.ToUpper() + "]" +
                    "<br/> Applied On : " + Common.DisplayDateTime(grRecommendTravelList.DataKeys[SlNo - 1].Values[3].ToString(), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.DisplayDateTime(grRecommendTravelList.DataKeys[SlNo - 1].Values[4].ToString(), false, Constant.strDateFormat) +
                  " To " + Common.DisplayDateTime(grRecommendTravelList.DataKeys[SlNo - 1].Values[5].ToString(), false, Constant.strDateFormat) +
                  "<br/> Duration : " + Convert.ToString(Math.Round(Convert.ToDouble(grRecommendTravelList.DataKeys[SlNo - 1].Values[6].ToString()), 1));
            }
            SlNo = 0;
        }
        protected void grRecommendTravelList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;
            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            _gridView.SelectedIndex = _selectedIndex;
            //string strPreYrLv = "";
            switch (_commandName)
            {
                case ("ViewClick"):
                    GenerateReport(grRecommendTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim(), grRecommendTravelList.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim(), grRecommendTravelList.DataKeys[_gridView.SelectedIndex].Values[13].ToString().Trim());
                    break;

                case ("RecommendClick"):
                    UpdateTravel(grRecommendTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim(), grRecommendTravelList.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim(),"R");
                    SiteMaster.ShowClientMessage(Page, "Travel has been Recommended Successfully.", "success");
                    break;
                case ("CancelClick"):

                    SiteMaster.ShowClientMessage(Page, "Leave has been Cancelled Successfully.", "success");
                    break;
            }

            this.OpenRecord();
            //strPreYrLv = "";
        }

        private void UpdateTravel(string travelId, string empId, string status)
        {
            if (string.IsNullOrEmpty(hdfApproveBy.Value))
            {
                SiteMaster.ShowClientMessage(Page, "Please Enter Approver.", "warning");
                return;
            }
            //DataTable dtTravelStatus;
            //dtTravelStatus = (DataTable)ViewState["TravelStatus"];
            objTravelMgr.UpdateEmpTravel(travelId,
                empId, status, hdfApproveBy.Value.Trim(), Session["EMPID"].ToString(),
                 Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat),"");
            this.SendMail();

        }
        private void SendMail()
        {
            MailManagerSmtpClient mailManager = new MailManagerSmtpClient();
            EmpInfoManager empManager = new EmpInfoManager();
            mailManager.strFromAddr = "";
            mailManager.strToAddr = "";
            //mailManager.MailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
            //mailManager.MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"]);
            //mailManager.SystemEmailUserName = ConfigurationManager.AppSettings["MyEmailUserName"].ToString();
            //mailManager.SystemEmailPwd = ConfigurationManager.AppSettings["MyEmailPwd"].ToString();
            //mailManager.Enablessl = ConfigurationManager.AppSettings["Enssl"].ToString().Trim();

            mailManager.strSubject = "Request to Approve Travel";

            mailManager.strBody = "Please verify the Application. "
                          + " \n "
                           + " \n\n "
                           + "With thanks "
                          + " \n\n ";

            DataTable empUserFrom = new DataTable();
            DataTable empUserTo = new DataTable();
            empUserFrom = empManager.SelectEmpInfo(Session["EMPID"].ToString());
            if (empUserFrom.Rows.Count > 0)
            {
                mailManager.strFromAddr = empUserFrom.Rows[0]["OfficeEmail"].ToString();
                //mailManager.strFromAddr = "alamgir@baseltd.com";
                mailManager.strBody += empUserFrom.Rows[0]["FullName"].ToString() + " \n ";
            }
            empUserTo = empManager.SelectEmpInfo(hdfApproveBy.Value.Trim());
            if (empUserTo.Rows.Count > 0)
            {
                mailManager.strToAddr = empUserTo.Rows[0]["OfficeEmail"].ToString();
                //mailManager.strToAddr = "rumi@baseltd.com";
            }
            string strVPath = "http://10.0.1.70:82/LogIn";
            mailManager.strBody += " \n ======================================\n"
                          + " Click here to login for approval: " + strVPath;
            mailManager.SendMSBMail();
        }
        private void GenerateReport(string travelId, string empId, string appStatus)
        {
            StringBuilder sb = new StringBuilder();
            Session["REPORTID"] = "TrStatus";
            Session["travelId"] = travelId;
            Session["appStatus"] = appStatus;
            Session["applicantId"] = empId;

            sb.Append("<script>");
            sb.Append("window.open('/CrystalReports/TravelReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
            
        }

        protected void txtApproveBy_TextChanged(object sender, EventArgs e)
        {
            TextBox txtApproveBy = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtApproveBy.Parent.Parent;
            GridView _gridView = (GridView)row.Parent.Parent;
            var tt = _gridView.Rows[row.RowIndex].Cells[3];
            if (txtApproveBy.Text.Trim() == string.Empty)
            {
                SiteMaster.ShowClientMessage(Page, "Please Enter Approver ID.", "error");
                hdfApproveBy.Value = "";
                return;
            }

            DataTable dtApproval = objEmpInfoMgr.SelectEmpInfoHRAction(txtApproveBy.Text.Trim());
            if (dtApproval.Rows.Count == 0)
            {
                SiteMaster.ShowClientMessage(Page, "Approver ID is Invalid.", "error");
                txtApproveBy.Text = "";
                hdfApproveBy.Value = "";
                return;
            }
            else
            {
                TextBox txtName = (TextBox)_gridView.Rows[row.RowIndex].Cells[3].FindControl("txtApproveByName");
                txtName.Text = dtApproval.Rows[0]["FullName"].ToString().Trim();
                hdfApproveBy.Value = txtApproveBy.Text.Trim();
            }
        }
    }
}