using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.App_Data;
using WebAdmin.BLL;
using System.Data;
using WebAdmin.DAL.DAO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace WebAdmin.UserControls.Leave
{
    public partial class ctlEmpTravel : UserControl
    {
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        MasterTablesManager objMaster = new MasterTablesManager();
        EmpTravelManager objTravelMgr = new EmpTravelManager();
        //dsEmployee objDS = new dsEmployee();
        DataTable dtEmpInfo = new DataTable();
        //static string strStartDate = "";
        //static string strEndDate = "";
        //static double dblTotWeekedDay = 0;
        //static double dblTotHoliDay = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //hdfFiscalYrId.Value = Session["FISCALYRID"].ToString().Trim();
                hdfIsUpdate.Value = "N";

                //if (Session["ISADMIN"].ToString().Trim() == "N")
                //{
                txtEmpID.Text = Session["EMPID"].ToString().Trim();
                //btnEmpSearch.Enabled = false;
                txtEmpID.ReadOnly = true;
                this.GetSearchResult();

                //grSupervisee.DataSource = objEmpInfoMgr.GetSuperVisiorWiseEmp(txtEmpID.Text.Trim(), Session["DIVISIONID"].ToString().Trim(), objDS.dtEmpList);
                //grSupervisee.DataBind();
                //if (grSupervisee.Rows.Count <= 1)
                //{
                //    divSupervisee.Visible = false;
                //}

                //}
                //else
                //{
                //    divSupervisee.Visible = false;
                //}
            }
        }
        protected void GetSearchResult()
        {
            if (txtEmpID.Text.Trim() == "")
                return;

            dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(txtEmpID.Text.Trim());
            if (dtEmpInfo.Rows.Count == 0)
            {
                Control[] cntlArr = { txtFullName, txtSupervisor };
                Common.EmptyTextBoxValues(cntlArr);
                return;
            }
            else
            {
                Common.PrepareEditView(dtEmpInfo.Rows[0], this.Controls);
                dtpAppDate.Text = Common.DisplayDateTime(DateTime.Today.ToShortDateString(), false, Constant.strDateFormat);

                //this.OpenRecord();
            }

            DataTable dtTravelMode = objTravelMgr.SelectTravelMode(0);
            Common.FillDropDownList(dtTravelMode, ddlTravelMode, "TravelModeTitle", "TravelModeID", true, "Select");

            DataTable dtProject = objMaster.SelectProjectList(0);
            Common.FillDropDownList(dtProject, ddlProject, "ProjectName", "ProjectId", true, "Select");
        }
        //protected void btnEmpSearch_Click(object sender, EventArgs e)
        //{
        //    if (Session["ISADMIN"].ToString().Trim() == "Y")
        //    {
        //        this.GetSearchResult();
        //    }
        //}
        //private void OpenRecord()
        //{
        //    DataTable dtEmpLvProfile = objLeave.SelectEmpLeaveProfileEXCPL(txtEmpID.Text.Trim(), "0", hdfGender.Value.ToString());
        //    if (dtEmpLvProfile.Rows.Count > 0)
        //    {
        //        grLeaveStatus.DataSource = dtEmpLvProfile;
        //        grLeaveStatus.DataBind();
        //        this.FormatLeaveStatusGridNumber();
        //        this.GetLeaveYearDates(dtEmpLvProfile);
        //    }
        //}
        //protected void GetLeaveYearDates(DataTable dt)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (string.IsNullOrEmpty(dt.Rows[0]["LeaveStartPeriod"].ToString()) == false)
        //        {
        //            hdfLvPackStartDate.Value = dt.Rows[0]["LeaveStartPeriod"].ToString();
        //            hdfLvPackEndDate.Value = dt.Rows[0]["LeaveEndPeriod"].ToString();
        //        }
        //        else
        //        {
        //            hdfLvPackStartDate.Value = Common.ReturnDateTimeInString(dt.Rows[0]["JoiningDate"].ToString(), false, Constant.strDateFormat);
        //            if (string.IsNullOrEmpty(dt.Rows[0]["SeparateDate"].ToString()) == false)
        //                hdfLvPackEndDate.Value = Common.ReturnDateTimeInString(dt.Rows[0]["SeparateDate"].ToString(), false, Constant.strDateFormat);
        //        }
        //    }
        //}
        //protected void FormatLeaveStatusGridNumber()
        //{
        //    int i = 0;
        //    foreach (GridViewRow gRow in grLeaveStatus.Rows)
        //    {
        //        gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1));
        //        gRow.Cells[2].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
        //        gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
        //        gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
        //        gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1) + Convert.ToDouble(grLeaveStatus.DataKeys[i].Values[8].ToString().Trim() == "" ? "0" : grLeaveStatus.DataKeys[i].Values[8].ToString().Trim()));
        //        gRow.Cells[6].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)) + Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text))) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1));

        //        if (Convert.ToDecimal(gRow.Cells[6].Text) < 0)
        //        {
        //            gRow.Cells[6].Text = "0";
        //        }
        //        i++;
        //    }
        //}
        protected void ddlTravelMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            double TotDay = 0;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            if (string.IsNullOrEmpty(dtpTravelStart.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid From Date!", "error");
                return;
            }
            if (string.IsNullOrEmpty(dtpTravelEnd.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid To Date!", "error");
                return;
            }
            if (string.IsNullOrEmpty(dtpResumeDate.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid Office Resume Date!", "error");
                return;
            }

            if (string.IsNullOrEmpty(dtpTravelEnd.Text) == false && string.IsNullOrEmpty(dtpTravelStart.Text) == false)
            {
                char[] splitter = { '/' };
                string[] arinfo = Common.str_split(dtpTravelStart.Text.Trim(), splitter);
                if (arinfo.Length == 3)
                {
                    dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                    //SiteMaster.ShowClientMessage(Page, "Pass: dtFrom()", "error");
                }
                arinfo = Common.str_split(dtpTravelEnd.Text.Trim(), splitter);

                if (arinfo.Length == 3)
                {
                    dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                    //SiteMaster.ShowClientMessage(Page, "Pass: dtTo()", "error");
                }

                TimeSpan Dur = dtTo.Subtract(dtFrom);

                TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
                if (TotDay < 0)
                {
                    SiteMaster.ShowClientMessage(Page, "From Date Cannot Be Greater Than To Date!", "error");
                    return;
                }
            }
            txtTotalDays.Text = TotDay.ToString();
        }
        
        protected bool ValidateAndSave()
        {
            try
            {
                if (hdfIsUpdate.Value == "N")
                {
                    hdfTvAppID.Value = Common.getMaxId("EmpTravel", "TravelId");
                }
                if (String.IsNullOrEmpty(txtTotalDays.Text) || Convert.ToInt32(txtTotalDays.Text)<=0)
                {
                    lblMsg.Text = "Total Days should be greater than zero.";
                    return false;
                }
                else if(ddlProject.SelectedIndex<=0)
                {
                    lblMsg.Text = "Please select project";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                lblMsg.Text = "";
                throw (ex);
            }
        }
        private void RefreshForm()
        {
            Common.EmptyTextBoxValues(this);
        }
        //private void RefreshControls()
        //{
        //    Control[] cntlArr = { hdfLvAppID, hdfLvPackStartDate, hdfLvPackEndDate, hdfLTypeNature, hdfIsUpdate, hdfPreLTypeId,
        //                           hdfIsOffdayCounted, hdfLAbbrName, hdfLEnjoyed, hdfLDates,hdfPreLDates,hdfLeavePakId,hdfPreLEnjoyed,
        //                            hdfFiscalYrId,dtpLeaveStart,dtpLeaveEnd,ddlIsHalfDay,dtpResumeDate,ddlLTypeId,txtAvailable,
        //                            txtLDurInDays,txtLTReason,txtAddrAtLeave,txtPhoneNo};

        //    Common.EmptyTextBoxValues(cntlArr);

        //    dtpLeaveStart.Enabled = true;
        //    dtpLeaveEnd.Enabled = true;
        //    this.OpenRecord();
        //}
        private void SaveData(string cmdType)
        {
            try
            {
                clsEmpTravel objTravel = new clsEmpTravel();
                objTravel.TravelId = hdfTvAppID.Value;
                objTravel.EmpId = txtEmpID.Text;
                objTravel.AppDate = dtpAppDate.Text;
                objTravel.VisitTo = txtVisitTo.Text;
                objTravel.Purpose = txtPurpose.Text.Trim();
                objTravel.TravelMode = ddlTravelMode.SelectedValue.ToString();
                objTravel.DepartureDate = dtpTravelStart.Text;
                objTravel.ReturnDate = dtpTravelEnd.Text;
                objTravel.OfficeJoinDate = dtpResumeDate.Text;
                objTravel.TotalDays = txtTotalDays.Text.Trim();
                objTravel.TravelInstruction = txtInstruction.Text.Trim();
                objTravel.TravelStatus = "P";
                objTravel.InsertedBy = Session["USERID"].ToString();
                objTravel.InsertedDate = Common.SetDate(DateTime.Now.ToString());
                objTravel.ProjectId = ddlProject.SelectedValue;
                DataTable dtSupervisor = objEmpInfoMgr.SelectEmpInfoSbuWise(hdfSupervisorId.Value.ToString(), "-1");
                if (dtSupervisor.Rows.Count > 0)
                {
                    string intStaff = dtSupervisor.Rows[0]["IsCountryDirector"] == null ? "" : dtSupervisor.Rows[0]["IsCountryDirector"].ToString();
                    if (intStaff == "Y") //country director
                        objTravel.TravelStatus = "R";
                }               
                objTravelMgr.InsertEmpTravel(objTravel, hdfIsUpdate.Value.ToString(), "N");
                this.SendMail(txtTotalDays.Text);
                //this.GenerateReport();
                //lblMsg.Text = objMail.RequestForTravelApproval(Session["EMPID"].ToString(), Session["USERNAME"].ToString(),
                //   Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                //   Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", hfSupervisor.Value.ToString(),
                //   hfSupervisorEmail.Value.ToString(), grConcernPerson, objTravel, ddlTravelType.SelectedItem.Text,
                //   ddlCurrency.SelectedItem.Text, ddlTravelMode.SelectedItem.Text, Session["COUNTRYDIRECTOR"].ToString().Trim(), txtInstruction.Text.Trim());

                //lblMsg.Text = "Travel application Posted and Mailed to Supervisor";

                this.EntryMode(false);
                //this.OpenRecord();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        
        protected void EntryMode(bool IsUpdate)
        {
            if (IsUpdate == true)
            {
                hdfIsUpdate.Value = "Y";
            }
            else
            {
                txtVisitTo.Text = "";
                txtPurpose.Text = "";
                dtpTravelStart.Text = "";
                dtpTravelEnd.Text = "";
                dtpResumeDate.Text = "";
                txtTotalDays.Text = "";
                ddlTravelMode.SelectedIndex = 0;
                txtInstruction.Text = "";

                hdfIsUpdate.Value = "N";
            }
        }
        private void SendMail(string totalDays)
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

            mailManager.strSubject = "Request to Verify Travel Recommendation";
            
                           //+ " Total Days : " + totalDays + ","
            mailManager.strBody = "Please verify the Application "
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
            empUserTo = empManager.SelectEmpInfo(empUserFrom.Rows[0]["SupervisorId"].ToString());
            if (empUserTo.Rows.Count > 0)
            {
                mailManager.strToAddr = empUserTo.Rows[0]["OfficeEmail"].ToString();
                //mailManager.strToAddr = "alamgir@baseltd.com";
            }
            string strVPath = "http://10.0.1.70:82/LogIn";
            mailManager.strBody += " \n ======================================\n"
                          + " Click here to login for recommendation: " + strVPath;
            mailManager.SendMSBMail();
        }
    
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAndSave() == true)
            {
                if (hdfIsUpdate.Value == "N")
                    this.SaveData("I");
                else
                    this.SaveData("U");

            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshForm();
            // ROLE WISE FORM  DATA AUTO FILLUP
            if (Session["ISADMIN"].ToString().Trim() == "N")
            {
                txtEmpID.Text = Session["EMPID"].ToString().Trim();
                txtEmpID.ReadOnly = true;
                this.GetSearchResult();
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }

        protected void txtTotalDays_TextChanged(object sender, EventArgs e)
        {

        }

        //private string GetRowStyleCssClass(DataControlRowState state)
        //{
        //    if ((state & DataControlRowState.Edit) > 0)
        //    {
        //        return grSupervisee.EditRowStyle.CssClass;
        //    }
        //    else if ((state & DataControlRowState.Selected) > 0)
        //    {
        //        return grSupervisee.SelectedRowStyle.CssClass;
        //    }
        //    else if ((state & DataControlRowState.Alternate) > 0)
        //    {
        //        return grSupervisee.AlternatingRowStyle.CssClass;
        //    }
        //    else
        //    {
        //        return grSupervisee.RowStyle.CssClass;
        //    }
        //}
        //protected void grSupervisee_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    GridView _gridView = (GridView)sender;
        //    // Get the selected index and the command name
        //    int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        //    string _commandName = e.CommandName;
        //    _gridView.SelectedIndex = _selectedIndex;

        //    switch (_commandName)
        //    {
        //        case ("Select"):
        //            {
        //                txtEmpID.Text = grSupervisee.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
        //                this.GetSearchResult();
        //                break;
        //            }
        //    }
        //}
    }
}