using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Data;
using System.Threading;

namespace WebAdmin.UserControls.Leave
{
    public partial class ctlLeaveAppView : System.Web.UI.UserControl
    {
        static string strStartDate = "";
        static string strEndDate = "";
        static string strStartLeavePeriod = "";
        static string strEndLeavePeriod = "";
        string[] strVal;
        DataTable personTable;

        LeaveManager objLeaveMgr = new LeaveManager();
        EmpInfoManager objEmpMgr = new EmpInfoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strParams = Request.QueryString["params"];
                if (string.IsNullOrEmpty(strParams) == false)
                {
                    strVal = null;
                    char[] splitter = { ',' };
                    strVal = Common.str_split(strParams, splitter);

                    strStartDate = DateTime.Now.Year.ToString();
                    strEndDate = Convert.ToString(Convert.ToInt32(strStartDate));

                    strStartDate = strStartDate + "-" + "01" + "-" + "01";
                    strEndDate = strEndDate + "-" + "12" + "-" + "31";

                    this.CreateTable();
                    personTable.Rows.Add(strVal[1], strVal[0], strVal[2], strStartDate, strEndDate);
                    ViewState["dt"] = personTable;
                    //Leave Information
                    DataTable dtLeaveApp = new DataTable();
                    dtLeaveApp = objLeaveMgr.SelectLeaveAppMstRpt(Convert.ToInt32(strVal[1]), strVal[0], strVal[2], strStartDate, strEndDate);
               
                    //Leave Profile
                    if (dtLeaveApp.Rows.Count > 0)
                    {
                        lblApplicantName.Text = dtLeaveApp.Rows[0]["FullName"].ToString().Trim()+", "+ dtLeaveApp.Rows[0]["DesigName"].ToString().Trim();
                        lblOffice.Text = dtLeaveApp.Rows[0]["DeptName"].ToString().Trim();
                        lblDateofApplication.Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtLeaveApp.Rows[0]["AppDate"].ToString().Trim(),false,Constant.strDateFormat),false,Constant.strDateFormat);
                        lblAppliedFor.Text = dtLeaveApp.Rows[0]["LTypeTitle"].ToString().Trim();
                        lblLeaveApplied.Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtLeaveApp.Rows[0]["LeaveStart"].ToString().Trim(), false, Constant.strDateFormat), false,Constant.strDateFormat)+" to "+ Common.ReturnDateTimeInString(Common.DisplayDateTime(dtLeaveApp.Rows[0]["LeaveEnd"].ToString().Trim(),false, Constant.strDateFormat), false,Constant.strDateFormat);
                        lblPurpose.Text = dtLeaveApp.Rows[0]["LTReason"].ToString().Trim();
                        lblContact.Text = dtLeaveApp.Rows[0]["AddrAtLeave"].ToString().Trim();
                        lblResumingDate.Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString().Trim(), false, Constant.strDateFormat), false,Constant.strDateFormat);
                        lblApprovedBy.Text = dtLeaveApp.Rows[0]["ApprovedBy"].ToString().Trim();
                        lblApprovedOn.Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtLeaveApp.Rows[0]["ApproveDate"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                        lblLeaveBalanceDate.Text= "Leave Balance Record As Of: "+ Common.ReturnDateTimeInString(DateTime.Today.ToShortDateString(), false, Constant.strDateFormat);
                        if (dtLeaveApp.Rows[0]["Gender"].ToString() == "F")
                        {
                            this.FillEmpLeaveProfile(strVal[0], "F");
                        }
                        else
                        {
                            this.FillEmpLeaveProfile(strVal[0], "M");
                        }

                        this.FillEmpLeaveDetails(strVal[0]);
                        this.GetLeavePeriod();

                        //Responsible Person Name & designation

                        //DataTable dtDivLevel = new DataTable();
                        DataTable dtResPerson = new DataTable();
                        //dtSecLevel = objLeaveMgr.SelectDivisionLevel(strVal[0].ToString());
                        //if (dtDivLevel.Rows.Count > 0)
                        //{
                        //    if (dtDivLevel.Rows[0]["SecLevel"].ToString() == "C")

                        dtResPerson = objLeaveMgr.SelectResponsePerson(Convert.ToInt32(strVal[1]), strVal[0].ToString());
                        //else
                        //    dtResPerson = objLeaveMgr.SelectResponsePerson(Convert.ToInt32(strVal[1]), strVal[0].ToString() , "B");

                        //if (dtResPerson.Rows.Count > 0)
                        //{
                        //    rptResponsePerson.DataSource = dtResPerson;
                        //    rptResponsePerson.DataBind();
                        //}
                        //}
                    }
                    if (strVal[3].Trim() == "L")
                    {
                        btnApprove.Visible = true;
                        btnRegret.Visible = true;
                        btnCancel.Visible = true;
                        btnRecommend.Visible = false;
                       // ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "document.getElementById('divApproveBy').Visible = false;", true);
                        divApproveBy.Visible = false;
                    }
                    if (strVal[3].Trim() == "A")
                    {
                        btnApprove.Visible = false;
                        btnRegret.Visible = false;
                        btnCancel.Visible = false;
                        btnRecommend.Visible = false;
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "document.getElementById('divApproveBy').Visible = false;", true);
                        divApproveBy.Visible = false;
                    }
                    else if (strVal[3].Trim() == "R")
                    {
                        btnApprove.Visible = false;
                        btnRegret.Visible = false;
                        btnRecommend.Visible = false;
                        //  ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "document.getElementById('divApproveBy').Visible = false;", true);
                        divApproveBy.Visible = false;
                    }
                    else if (strVal[3].Trim() == "M")
                    {
                        btnApprove.Visible = false;
                        btnRegret.Visible = true;
                        btnCancel.Visible = true;
                        btnRecommend.Visible = true;
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "document.getElementById('divApproveBy').Visible = true;", true);
                        divApproveBy.Visible = true;
                    }
                }
            }
        }
        private void CreateTable()
        {  //Int32 LvAppID, string EmpId, string AppStatus, string LeaveStart, string LeaveEnd  
            personTable = new DataTable();
            personTable.Columns.AddRange(new DataColumn[5]
                                {
                              new DataColumn("LvAppID", typeof(string)),
                              new DataColumn("EmpId", typeof(string)),
                              new DataColumn("AppStatus", typeof(string)),
                              new DataColumn("LeaveStart", typeof(string)),
                              new DataColumn("LeaveEnd", typeof(string))
                                });
            ViewState["dt"] = personTable;
        }
        private void FillEmpLeaveProfile(string EmpId, string Sex)
        {
            DataTable dtEmp = new DataTable();
            dtEmp.Rows.Clear();
            dtEmp.Dispose();

            dtEmp = objLeaveMgr.SelectEmpLeaveProfileEXCPL(EmpId, "0", Sex);

            if (dtEmp.Rows.Count > 0)
            {
                grLeaveStatus.DataSource = null;
                grLeaveStatus.DataBind();

                grLeaveStatus.DataSource = dtEmp;
                grLeaveStatus.DataBind();

                this.FormatLeaveStatusGridNumber();
                strStartLeavePeriod = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtEmp.Rows[0]["LeaveStartPeriod"].ToString(), false, Constant.strDateFormat), false,Constant.strDateFormat);
                strEndLeavePeriod = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtEmp.Rows[0]["LeaveEndPeriod"].ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
            }
        }
        protected void FormatLeaveStatusGridNumber()
        {
            int i = 0;
            foreach (GridViewRow gRow in grLeaveStatus.Rows)
            {
                gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1));
                gRow.Cells[2].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
                gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
                //gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
                //gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1) + Convert.ToDouble(grLeaveStatus.DataKeys[i].Values[9].ToString().Trim() == "" ? "0" : grLeaveStatus.DataKeys[i].Values[9].ToString().Trim()));
                gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1) + (Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1)) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)));

                if (Convert.ToDecimal(gRow.Cells[4].Text) < 0)
                {
                    gRow.Cells[4].Text = "0";
                }
                i++;
            }
        }
        private void FillEmpLeaveDetails(string EmpId)
        {
            DataTable dtEmpLvDtls = new DataTable();
            dtEmpLvDtls.Rows.Clear();
            dtEmpLvDtls.Dispose();

            dtEmpLvDtls = objLeaveMgr.SelectEmpLeaveDetails(EmpId, strStartLeavePeriod, strEndLeavePeriod);

            if (dtEmpLvDtls.Rows.Count > 0)
            {
                grLeaveDtls.DataSource = null;
                grLeaveDtls.DataBind();

                grLeaveDtls.DataSource = dtEmpLvDtls;
                grLeaveDtls.DataBind();
                this.FormatLeaveDetailsGridNumber();
            }
        }
        protected void FormatLeaveDetailsGridNumber()
        {
            foreach (GridViewRow gRow in grLeaveDtls.Rows)
            {
                gRow.Cells[1].Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(gRow.Cells[1].Text, false, Constant.strDateFormat), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(gRow.Cells[2].Text, false, Constant.strDateFormat), false, Constant.strDateFormat);
                gRow.Cells[3].Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(gRow.Cells[3].Text, false, Constant.strDateFormat), false, Constant.strDateFormat);
                if (gRow.Cells[5].Text.Trim() == "A")
                    gRow.Cells[5].Text = "Availed";
                else if (gRow.Cells[5].Text.Trim() == "R")
                    gRow.Cells[5].Text = "Requsted";
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[7].Text)) == false)
                    gRow.Cells[7].Text = Common.ReturnDateTimeInString(Common.DisplayDateTime(gRow.Cells[7].Text, false, Constant.strDateFormat), false, Constant.strDateFormat);
            }
        }
        public void GetLeavePeriod()
        {
            lblLeavePeriod.Text = "Leave Records from " + this.GetLeaveStartDate() + "  to   " + this.GetLeaveEndDate();
        }
        public string GetLeaveStartDate()
        {
            return Common.ReturnDateTimeInString(strStartLeavePeriod, false, Constant.strDateFormat);
        }

        public string GetLeaveEndDate()
        {
            return Common.ReturnDateTimeInString(strEndLeavePeriod, false, Constant.strDateFormat);
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
        private void AvailableLeave(string gridStatus, string strEmpID, string strLTypeID,string strDurationInDays)
        {
            DataTable dtLeaveProfile = new DataTable();
            if (gridStatus == "A")
            {
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLTypeID);
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                        hfLEnjoyed.Value = Convert.ToString(Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(strDurationInDays.Trim()));
                    else
                        hfLEnjoyed.Value = strDurationInDays.Trim();
                }
            }
            else if (gridStatus == "D")
            {
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLTypeID);
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                        hfLEnjoyed.Value = Convert.ToString(Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(strDurationInDays.Trim()));
                    else
                        hfLEnjoyed.Value = "0";
                }
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string strPreYrLv = "";
            personTable= ViewState["dt"] as DataTable;
            DataTable dtLeaveApp = new DataTable();

            dtLeaveApp=objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(personTable.Rows[0]["LvAppID"].ToString().Trim()), personTable.Rows[0]["EmpId"].ToString().Trim(), "", Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveStart"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveEnd"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), "");
            if (dtLeaveApp.Rows.Count > 0)
            {
                this.AvailableLeave("A", dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), dtLeaveApp.Rows[0]["LTypeId"].ToString().Trim(), dtLeaveApp.Rows[0]["LDurInDays"].ToString().Trim());
                this.GetLeaveDates(dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim(), "A", dtLeaveApp.Rows[0]["LeaveStart"].ToString().Trim(), dtLeaveApp.Rows[0]["LeaveEnd"].ToString().Trim());

                objLeaveMgr.UpdateLeaveAppMstForApprove(dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim(),
                   dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), "Y", "N", "A", hfLEnjoyed.Value.ToString(), hfLDates.Value.ToString(), dtLeaveApp.Rows[0]["LAbbrName"].ToString().Trim(),
                     dtLeaveApp.Rows[0]["LTypeId"].ToString().Trim(), dtLeaveApp.Rows[0]["LTReason"].ToString().Trim(),
                    Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat), strPreYrLv, dtLeaveApp.Rows[0]["LDurInDays"].ToString().Trim());

                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "ReLoadPreviousPage();", true);
                SiteMaster.ShowClientMessage(Page, "Leave has been approved successfully.", "success");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "CloseWindow(3200);", true);
            }
            else
            {
                SiteMaster.ShowClientMessage(Page, "No Leave Information to Approve.", "warn");
            }
        }
        protected void btnRegret_Click(object sender, EventArgs e)
        {
            personTable = ViewState["dt"] as DataTable;
            DataTable dtLeaveApp = new DataTable();

            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(personTable.Rows[0]["LvAppID"].ToString().Trim()), personTable.Rows[0]["EmpId"].ToString().Trim(), "", Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveStart"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveEnd"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), "");
            if (dtLeaveApp.Rows.Count > 0)
            {
                this.AvailableLeave("A", dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), dtLeaveApp.Rows[0]["LTypeId"].ToString().Trim(), dtLeaveApp.Rows[0]["LDurInDays"].ToString().Trim());
                this.GetLeaveDates(dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim(), "A", dtLeaveApp.Rows[0]["LeaveStart"].ToString().Trim(), dtLeaveApp.Rows[0]["LeaveEnd"].ToString().Trim());

                objLeaveMgr.UpdateLeaveAppMstForDeny(dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim(),
                dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), "Y", "N", "D",
                Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat));

                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "ReLoadPreviousPage();", true);
                SiteMaster.ShowClientMessage(Page, "Leave has been Regreted Successfully.", "success");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "CloseWindow(3200);", true);
            }
            else
            {
                SiteMaster.ShowClientMessage(Page, "No Leave Information to Regret.", "warn");
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            personTable = ViewState["dt"] as DataTable;
            DataTable dtLeaveApp = new DataTable();

            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(personTable.Rows[0]["LvAppID"].ToString().Trim()), personTable.Rows[0]["EmpId"].ToString().Trim(), "", Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveStart"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveEnd"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), "");
            if (dtLeaveApp.Rows.Count > 0)
            {
                this.AvailableLeave("A", dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), dtLeaveApp.Rows[0]["LTypeId"].ToString().Trim(), dtLeaveApp.Rows[0]["LDurInDays"].ToString().Trim());
                this.GetLeaveDates(dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim(), "A", dtLeaveApp.Rows[0]["LeaveStart"].ToString().Trim(), dtLeaveApp.Rows[0]["LeaveEnd"].ToString().Trim());

                objLeaveMgr.CancelLeaveApp(dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim(),
                    dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), "Y", "N", "C",
                    Session["USERID"].ToString(), Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat));

                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "ReLoadPreviousPage();", true);
                SiteMaster.ShowClientMessage(Page, "Leave has been Cancelled Successfully.", "success");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "CloseWindow(3200);", true);
            }
            else
            {
                SiteMaster.ShowClientMessage(Page, "No Leave Information to Cancel.", "warn");
            }
        }
        protected void txtApproveBy_TextChanged(object sender, EventArgs e)
        {
            if (txtApproveBy.Text.Trim() == string.Empty)
            {
                SiteMaster.ShowClientMessage(Page, "Please Enter Approver ID.", "error");
                return;
            }

            DataTable dtApproval = objEmpMgr.SelectEmpInfoHRAction(txtApproveBy.Text.Trim());
            if (dtApproval.Rows.Count == 0)
            {
                SiteMaster.ShowClientMessage(Page, "Approver ID is Invalid.", "error");
                txtApproveBy.Text = "";
                return;
            }
            else
            {
                txtApproveByName.Text = dtApproval.Rows[0]["FullName"].ToString().Trim();
                hdfSpervisorEmail.Value = dtApproval.Rows[0]["OfficeEmail"].ToString().Trim();
            }
        }
        protected void btnRecommend_Click(object sender, EventArgs e)
        {
            if (txtApproveBy.Text.Trim() == string.Empty)
            {
                SiteMaster.ShowClientMessage(Page, "Please Enter Approver ID.", "error");
                return;
            }
            DataTable dtApproval = objEmpMgr.SelectEmpInfoHRAction(txtApproveBy.Text.Trim());
            if (dtApproval.Rows.Count == 0)
            {
                SiteMaster.ShowClientMessage(Page, "Approver ID is Invalid.", "error");
                return;
            }
            personTable = ViewState["dt"] as DataTable;
            DataTable dtLeaveApp = new DataTable();

            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(personTable.Rows[0]["LvAppID"].ToString().Trim()), personTable.Rows[0]["EmpId"].ToString().Trim(), "", Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveStart"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), Common.ReturnDateTimeInString(Common.DisplayDateTime(personTable.Rows[0]["LeaveEnd"].ToString().Trim(), false, Constant.strDateFormat), false, Constant.strDateFormat), "");
            //objEmpInfoMgr.SelectEmpInfo(txtEmpID.Text.Trim());
            if (dtLeaveApp.Rows.Count > 0)
            {
                objLeaveMgr.UpdateLeaveAppMstForRecommendation(dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim(),
                dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), "R", txtApproveBy.Text.Trim(), Session["USERID"].ToString(),
                 Common.ReturnDateTimeInString(Common.DisplayDateTime(DateTime.Now.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat));

                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "ReLoadPreviousPage();", true);
                 SiteMaster.ShowClientMessage(Page, "Leave has been Recommended Successfully.", "success");
                this.SendMail(dtLeaveApp.Rows[0]["EmpId"].ToString().Trim(), dtLeaveApp.Rows[0]["LvAppID"].ToString().Trim()
                    , personTable.Rows[0]["LeaveStart"].ToString().Trim(), personTable.Rows[0]["LeaveEnd"].ToString().Trim(), txtApproveBy.Text.Trim()
                    , Session["EMPID"].ToString().Trim());
                ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "CloseWindow(3200);", true);
            }
            else
            {
                SiteMaster.ShowClientMessage(Page, "No Leave Information to Recommend.", "warn");
            }
        }

        private void SendMail(string empId,string leaveAppId,string leaveStrt,string leaveEnd,string approvedBy,string supervisorId)
        {
            //lblMsg.Text = "Lel3";
            string message = "";
            MailManagerSmtpClient objMail = new MailManagerSmtpClient();
            #region mail sending

            DateTime LvStDate = Convert.ToDateTime(leaveStrt);
            DateTime LvEnDate = new DateTime();
            if (leaveEnd != "")
                LvEnDate = Convert.ToDateTime(leaveEnd);
            string emp = Session["EMPID"].ToString();
            string user = Session["USERNAME"].ToString();
            string des = Session["DESIGNATION"].ToString();
            string loc = Session["LOCATION"].ToString();//now static
            if (string.IsNullOrEmpty(hdfSpervisorEmail.Value))
            {
                message = "Leave applied but mail did not send";
            }
            else
            {

                //Open this part for mail  
                //lblMsg.Text = "Level4";
                //hdfSpervisorEmail.Value = "alamgir@baseltd.com";
                message = objMail.RequestFromRecommendar(empId, leaveAppId,
                    Common.SetDate(LvStDate.ToShortDateString()), Common.SetDate(LvEnDate.ToShortDateString()),
                    Session["EMPID"].ToString(), Session["USERNAME"].ToString(),
                    Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                    Session["USERID"].ToString().Trim().ToUpper() == "SYSTEM" ? "Y" : "N", supervisorId,
                    hdfSpervisorEmail.Value.ToString());
            }
            SiteMaster.ShowClientMessage(Page, message, "info");
            #endregion
        }
    }
}