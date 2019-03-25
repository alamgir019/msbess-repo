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
using CrystalDecisions.Shared;

namespace WebAdmin.UserControls.Travel
{
    public partial class ctlTravelToApprove : System.Web.UI.UserControl
    {
        //LeaveManager objLeaveMgr = new LeaveManager();
        EmpTravelManager objTravelMgr = new EmpTravelManager();
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
        private void OpenRecord()
        {
            grTravelList.DataSource = null;
            grTravelList.DataBind();
           
            string strStartYear = DateTime.Now.Year.ToString();
            string strStartMonth = DateTime.Now.Month.ToString();

            if (Convert.ToInt32(strStartMonth) >= 1)
            {
                strStartDate = Convert.ToString(Convert.ToInt32(strStartYear) - 1);
                strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);
                strStartDate = strStartDate + "-" + "07" + "-" + "01";
                strEndDate = strEndDate + "-" + "12" + "-" + "31";
            }
            grTravelList.DataSource = objTravelMgr.GetTravelApp(0, "", "R", strStartDate, strEndDate, Session["EMPID"].ToString().Trim());
            
            grTravelList.DataBind();
            this.FormatGridDate();
        }
        protected void FormatGridDate()
        {
            int SlNo = 0;
            foreach (GridViewRow gRow in grTravelList.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[0].Text = SlNo.ToString();
                gRow.Cells[1].Text = grTravelList.DataKeys[SlNo - 1].Values[11].ToString() + " [" + gRow.Cells[1].Text.ToUpper() + "]" +
                    "<br/> Applied On : " + Common.DisplayDateTime(grTravelList.DataKeys[SlNo - 1].Values[3].ToString(), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.DisplayDateTime(grTravelList.DataKeys[SlNo - 1].Values[4].ToString(), false, Constant.strDateFormat) +
                  " To " + Common.DisplayDateTime(grTravelList.DataKeys[SlNo - 1].Values[5].ToString(), false, Constant.strDateFormat) +
                  "<br/> Duration : " + Convert.ToString(Math.Round(Convert.ToDouble(grTravelList.DataKeys[SlNo - 1].Values[6].ToString()), 1));
            }
            SlNo = 0;
        }

        protected void grTravelApp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MailManagerSmtpClient objMail = new MailManagerSmtpClient();
            string mailMessage = "";
            GridView _gridView = (GridView)sender;
            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            _gridView.SelectedIndex = _selectedIndex;
            char[] splitter = { ',' };
            string[] arinfo2 = new string[10];
            string leaveStart = "";
            string leaveEnd = "";
            switch (_commandName)
            {
                case ("ViewClick"):
                    GenerateReport(grTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim(), grTravelList.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim(), grTravelList.DataKeys[_gridView.SelectedIndex].Values[13].ToString().Trim());
                    break;

                case ("ApproveClick"):
                    this.GetTravelDates(grTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), "A", grTravelList.DataKeys[_gridView.SelectedIndex].Values[4].ToString(), grTravelList.DataKeys[_gridView.SelectedIndex].Values[5].ToString());
                    UpdateTravel(grTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim(), grTravelList.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim(), "A",hfLDates.Value);
                    
                    //Email Notification      
                    SiteMaster.ShowClientMessage(Page, "Leave has been approved successfully", "success");
                    break;

                  case ("DenyClick"):
                    //objLeaveMgr.UpdateLeaveAppMstForDeny(grTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                    //    grTravelList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "D",
                    //    Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false,Constant.strDateFormat));
                    
                    mailMessage = objMail.LeaveMail(grTravelList.DataKeys[_gridView.SelectedIndex].Values[11].ToString(), grTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                          Session["EMPID"].ToString(), leaveStart,leaveEnd, "D");
                    SiteMaster.ShowClientMessage(Page, "Leave has been Regreted Successfully.", "success");
                    break;

                case ("CancelClick"):
                    //objLeaveMgr.CancelLeaveApp(grTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                    //    grTravelList.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "C",
                    //    Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime( DateTime.Now.ToString(), false, Constant.strDateFormat), false,Constant.strDateFormat));
                    
                    mailMessage = objMail.LeaveMail(grTravelList.DataKeys[_gridView.SelectedIndex].Values[11].ToString(), grTravelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                          Session["EMPID"].ToString(), leaveStart,leaveEnd, "C");

                    SiteMaster.ShowClientMessage(Page, "Leave has been Cancelled Successfully.", "success");              
                    break;
                    }

                    this.OpenRecord();
            }
        protected void GetTravelDates(string strTvAppId, string gridStatus, string strDateFrom, string strDateTo)
        {
            DataTable dtTravelDates = new DataTable();
            dtTravelDates = objTravelMgr.GetTravelApp(Convert.ToInt32(strTvAppId),"","","","","");
            if (dtTravelDates.Rows.Count > 0)
            {
                hfLDates.Value = "";
                foreach (DataRow dRow in dtTravelDates.Rows)
                {
                    double day = (Convert.ToDateTime(dRow["ReturnDate"]) - Convert.ToDateTime(dRow["DepartureDate"])).TotalDays+1;
                    for (int i = 0; i < day; i++)
                    {
                        if (hfLDates.Value != "")
                            hfLDates.Value = hfLDates.Value + "," + Common.ReturnDateTimeInString(Common.DisplayDateTime(Convert.ToDateTime(dRow["DepartureDate"]).AddDays(i).ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                        else
                        {                            
                            hfLDates.Value = Common.ReturnDateTimeInString(Common.DisplayDateTime(dRow["DepartureDate"].ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                        }
                    }
                }
            }
        }
        private void UpdateTravel(string travelId, string empId, string status,string tvDates)
        {
            //DataTable dtTravelStatus;
            //dtTravelStatus = (DataTable)ViewState["TravelStatus"];
            objTravelMgr.UpdateEmpTravel(travelId,
                empId, status, "", Session["EMPID"].ToString(),
                 Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat),tvDates);
            this.SendMail(empId);
        }
        private void SendMail(string applicant)
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

            mailManager.strSubject = "Travel Approval";

            mailManager.strBody = "Your travel Application has been Approved. "
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
                //mailManager.strFromAddr = "rumi@baseltd.com";
                mailManager.strBody += empUserFrom.Rows[0]["FullName"].ToString() + " \n ";
            }
            empUserTo = empManager.SelectEmpInfo(applicant);
            if (empUserTo.Rows.Count > 0)
            {
                mailManager.strToAddr = empUserTo.Rows[0]["OfficeEmail"].ToString();
                //mailManager.strToAddr = "alamgir@baseltd.com";
            }
            string strVPath = "http://10.0.1.70:82/LogIn";
            mailManager.strBody += " \n ======================================\n"
                          + " Click here to login: " + strVPath;
            mailManager.SendMSBMail();
        }
        private void GenerateReport(string travelId, string empId, string appStatus)
        {

            StringBuilder sb = new StringBuilder();
            Session["REPORTID"] = "TrStatus";
            Session["travelId"] = travelId;
            Session["appStatus"] = appStatus;

            sb.Append("<script>");
            sb.Append("window.open('/CrystalReports/TravelReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
            
            //string ReportPath = "";
            //CrystalDecisions.CrystalReports.Engine.ReportDocument ReportDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //StringBuilder sb = new StringBuilder();
            //string fileName = Session["USERID"].ToString() + "_" + "TravelApp" + ".pdf";
            //ReportPath = Server.MapPath("~/CrystalReports/rptTravelApplication.rpt");
            //ReportDoc.Load(ReportPath);

            //DataTable travelData = objTravelMgr.SelectEmpTravelRpt(travelId, empId, appStatus);

            //ReportDoc.SetDataSource(travelData);
            //this.ExPortReport(ReportDoc, fileName);
            //this.OpenWindow(fileName, sb);
        }

        //private void OpenWindow(string fileName, StringBuilder sb)
        //{
        //    sb.Append("<script>");
        //    sb.Append("window.open('/CrystalReports/VirtualReport/" + fileName + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");
        //    sb.Append("</script>");
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
        //                             sb.ToString(), false);
        //}
        //private void ExPortReport(CrystalDecisions.CrystalReports.Engine.ReportDocument ReportDoc, string rptPath)
        //{
        //    CrystalDecisions.Shared.ExportOptions CrExportOptions;
        //    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
        //    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
        //    CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/CrystalReports/VirtualReport/" + rptPath);
        //    CrExportOptions = ReportDoc.ExportOptions;
        //    {
        //        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
        //        CrExportOptions.FormatOptions = CrFormatTypeOptions;
        //    }
        //    ReportDoc.Export();
        //}
        
    }
}