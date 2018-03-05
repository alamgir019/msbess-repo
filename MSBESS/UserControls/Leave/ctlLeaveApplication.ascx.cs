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
using System.Drawing;
namespace WebAdmin.UserControls.Leave
{
    public partial class ctlLeaveApplication : System.Web.UI.UserControl
    {
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        LeaveManager objLeave = new LeaveManager();
        dsEmployee objDS = new dsEmployee();
        DataTable dtEmpInfo = new DataTable();
        static string strStartDate = "";
        static string strEndDate = "";
        static double dblTotWeekedDay = 0;
        static double dblTotHoliDay = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdfFiscalYrId.Value = Session["FISCALYRID"].ToString().Trim();
                hdfIsUpdate.Value = "N";

                // ROLE WISE FORM  DATA AUTO FILLUP
                if (Session["ISADMIN"].ToString().Trim() == "N")
                {
                    txtEmpID.Text = Session["EMPID"].ToString().Trim();
                    btnEmpSearch.Enabled = false;
                    txtEmpID.ReadOnly = true;
                    this.GetSearchResult();

                    //grSupervisee.DataSource = objEmpInfoMgr.SelectEmpInfo("");
                    grSupervisee.DataSource = objEmpInfoMgr.GetSuperVisiorWiseEmp(txtEmpID.Text.Trim(), Session["DIVISIONID"].ToString().Trim(), objDS.dtEmpList);
                    grSupervisee.DataBind();
                    if(grSupervisee.Rows.Count<=1)
                    {
                        divSupervisee.Visible = false;
                    }

                }
                else
                {
                    divSupervisee.Visible = false;
                }
            }
        }
        protected void GetSearchResult()
        {
            if (txtEmpID.Text.Trim() == "")
                return;

            dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(txtEmpID.Text.Trim());
            if (dtEmpInfo.Rows.Count == 0)
            {
                Control[] cntlArr = { txtFullName, txtSupervisor, txtLPackName };
                Common.EmptyTextBoxValues(cntlArr);
                return;
            }
            else
            {
                // FILL EMPLOYEE COMMON INFO
                Common.PrepareEditView(dtEmpInfo.Rows[0], this.Controls);
                dtpAppDate.Text = Common.DisplayDateTime(DateTime.Today.ToShortDateString(), false, Constant.strDateFormat);

                this.OpenRecord();

                // BIND LEAVE TYPE
                DataTable dtLeaveType = objLeave.SelectEmpWiseLeaveType(0, txtEmpID.Text.Trim(), hdfGender.Value.ToString());
                Common.FillDropDownList(dtLeaveType, ddlLTypeId, "LTypeTitle", "LTypeID", true, "Select");
            }
        }
        protected void btnEmpSearch_Click(object sender, EventArgs e)
        {
            if (Session["ISADMIN"].ToString().Trim() == "Y")
            {
                this.GetSearchResult();
            }
        }
        private void OpenRecord()
        {

            //if (string.IsNullOrEmpty(hdfLvPackStartDate.Value.Trim()) == false)
            //{
            //    strStartDate = hdfLvPackStartDate.Value.Trim();
            //    strEndDate = hdfLvPackEndDate.Value.Trim();
            //}
            //else
            //{
            //    SiteMaster.ShowClientMessage(Page, "Your Leave Package Is Not Available In HR Information !", "error");
            //    return;
            //}
            // BIND GRID OF LEAVE PROFILE
            DataTable dtEmpLvProfile = objLeave.SelectEmpLeaveProfileEXCPL(txtEmpID.Text.Trim(), "0", hdfGender.Value.ToString());
            if (dtEmpLvProfile.Rows.Count > 0)
            {
                grLeaveStatus.DataSource = dtEmpLvProfile;
                grLeaveStatus.DataBind();
                this.FormatLeaveStatusGridNumber();
                this.GetLeaveYearDates(dtEmpLvProfile);
            }
        }
        // Employee Leave Year From and To Date
        protected void GetLeaveYearDates(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0]["LeaveStartPeriod"].ToString()) == false)
                {
                    hdfLvPackStartDate.Value = dt.Rows[0]["LeaveStartPeriod"].ToString();
                    hdfLvPackEndDate.Value = dt.Rows[0]["LeaveEndPeriod"].ToString();
                }
                else
                {
                    hdfLvPackStartDate.Value = Common.ReturnDateTimeInString(dt.Rows[0]["JoiningDate"].ToString(), false, Constant.strDateFormat);
                    if (string.IsNullOrEmpty(dt.Rows[0]["SeparateDate"].ToString()) == false)
                        hdfLvPackEndDate.Value = Common.ReturnDateTimeInString(dt.Rows[0]["SeparateDate"].ToString(), false, Constant.strDateFormat);
                }
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
                gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
                gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1) + Convert.ToDouble(grLeaveStatus.DataKeys[i].Values[8].ToString().Trim() == "" ? "0" : grLeaveStatus.DataKeys[i].Values[8].ToString().Trim()));
                gRow.Cells[6].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)) + Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text))) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1));

                if (Convert.ToDecimal(gRow.Cells[6].Text) < 0)
                {
                    gRow.Cells[6].Text = "0";
                }
                i++;
            }
        }
        protected void ddlLTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLeaveTypeDependency();
            this.Calculate_Click();
            if(CheckMaxLeaveAllow()==true)
            {
                this.dtpLeaveStart.Enabled = false;
                this.dtpLeaveEnd.Enabled = false;
            }
           
            //this.PanelFrom.Visible = false;
            //this.PanelTo.Visible = false;
        }
        private void GetLeaveTypeDependency()
        {
            txtAvailable.Text = "";
            DataTable dtLType = new DataTable();
            dtLType = objLeave.SelectLeaveType(Convert.ToInt32(ddlLTypeId.SelectedValue));
            if (dtLType.Rows.Count > 0)
            {
                hdfLTypeNature.Value = dtLType.Rows[0]["LNature"].ToString();
                hdfIsOffdayCounted.Value = dtLType.Rows[0]["IsOffdayCounted"].ToString().Trim();
            }
            if ((ddlLTypeId.SelectedValue != "-1"))
            {
                try
                {
                    this.AvailableLeave();
                    
                }
                catch
                {
                    throw new Exception ("From AvailableLeave 1st");
                }
            }

            //************ Check This Line Required or Not         
           
            //try
            //{
            //    DateTime dtCurrMonth = Common.ReturnDateTime(DateTime.Today.ToString(), false, Constant.strDateFormat);
            //    int iCurrMonth = Convert.ToInt16(dtCurrMonth.Month);
            //    //SiteMaster.ShowClientMessage(Page, "End: GetLeaveTypeDependency()", "error");
            //    iCurrMonth = 0;
            //}
            //catch
            //{
            //    throw new Exception("From AvailableLeave 2nd");
            //}
           
        }
        protected void Calculate_Click()
        {
            if (hdfIsUpdate.Value == "Y" && hdfPreLTypeId.Value == ddlLTypeId.SelectedValue.ToString())
            {
                this.AvailableLeave();
                if (txtAvailable.Text != "" && txtLDurInDays.Text != "" && (hdfLTypeNature.Value != "5") && (hdfLTypeNature.Value != "6"))
                    txtAvailable.Text = Convert.ToString(Convert.ToDouble(txtAvailable.Text));
            }
            if (dtpLeaveStart.Text.Trim() == dtpLeaveEnd.Text.Trim())
            {
                DataTable dtWeekend = objLeave.SelectEmpWiseWeekend(txtEmpID.Text.Trim());
                if (Common.IsWeekendDay(Common.ReturnDateTime(dtpLeaveStart.Text.Trim(), false, Constant.strDateFormat), dtWeekend) == true)
                {
                    dtWeekend.Rows.Clear();
                    dtWeekend.Dispose();
                    SiteMaster.ShowClientMessage(Page, "Pass: IsWeekendDay()", "error");
                    this.Get_LeaveDate_With_Weekend_Holiday();
                    SiteMaster.ShowClientMessage(Page, "Pass: Get_LeaveDate_With_Weekend_Holiday()", "error");
                    return;
                }
            }

            if ((hdfIsOffdayCounted.Value.ToString() == "Y") || (hdfLAbbrName.Value.ToString() == "ML"))
                this.Get_LeaveDate_With_Weekend_Holiday();
            else
                this.Get_LeaveDate_WithOut_Weekend_Holiday("A");
        }
        private void AvailableLeave()
        {
            if (ddlLTypeId.SelectedValue != "-1")
            {
                if (txtEmpID.Text.Trim() != "")
                {
                    DataTable dtLeaveProfile = new DataTable();
                    dtLeaveProfile = objLeave.SelectEmpLeaveProfile(txtEmpID.Text.Trim(), ddlLTypeId.SelectedValue.ToString());
                    decimal intAvail = 0;
                    decimal LCarryOverd = 0;
                    decimal LEntitled = 0;
                    //decimal LCashed = 0;
                    decimal LEnjoyed = 0;
                    decimal LeaveElapsed = 0;
                    txtAvailable.Text = "";
                    if (dtLeaveProfile.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString()) == false)
                            LCarryOverd = LCarryOverd + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString());
                        else
                            LCarryOverd = 0;

                        if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["lvPrevYearCarry"].ToString()) == false)
                            LCarryOverd = LCarryOverd + Convert.ToDecimal(dtLeaveProfile.Rows[0]["lvPrevYearCarry"].ToString());

                        if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LEntitled"].ToString()) == false)
                            LEntitled = LEntitled + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LEntitled"].ToString());
                        else
                            LEntitled = 0;
                        if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                            hdfLEnjoyed.Value = dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString();
                        else
                            hdfLEnjoyed.Value = "0";
                        if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveElapsed"].ToString()) == false)
                            LeaveElapsed = LeaveElapsed + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveElapsed"].ToString());
                        else
                            LeaveElapsed = 0;

                        if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["lvOpening"].ToString()) == false)
                            LEnjoyed = LEnjoyed + Convert.ToDecimal(dtLeaveProfile.Rows[0]["lvOpening"].ToString());
                        else
                            LEnjoyed = 0;

                        hdfLAbbrName.Value = dtLeaveProfile.Rows[0]["LAbbrName"].ToString();

                        if (hdfLTypeNature.Value != "5" && hdfLTypeNature.Value != "6")
                        {
                            intAvail = (LCarryOverd + LEntitled) - (Convert.ToDecimal(hdfLEnjoyed.Value) + LeaveElapsed + LEnjoyed);

                            txtAvailable.Text = Convert.ToString(Math.Round(intAvail, 1));
                            if (Convert.ToDecimal(txtAvailable.Text) < 0)
                            {
                                txtAvailable.Text = "0";
                            }
                        }
                        else
                            intAvail = Convert.ToDecimal(hdfLEnjoyed.Value);
                    }
                }
            }
        }
        protected void Get_LeaveDate_With_Weekend_Holiday()
        {
            HiddenField hfLeaveDates = new HiddenField();
            HiddenField hfWeeekendDay = new HiddenField();

            double TotDay = 0;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            if (string.IsNullOrEmpty(dtpLeaveStart.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid From Date!", "error");
                return;
            }
            if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid To Date!", "error");
                return;
            }
            if (string.IsNullOrEmpty(dtpResumeDate.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid Office Resume Date!", "error");
                return;
            }

            if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == false && string.IsNullOrEmpty(dtpLeaveStart.Text) == false)
            {
                char[] splitter = { '/' };
                string[] arinfo = Common.str_split(dtpLeaveStart.Text.Trim(), splitter);
                if (arinfo.Length == 3)
                {
                    dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                    SiteMaster.ShowClientMessage(Page, "Pass: dtFrom()", "error");
                }
                arinfo = Common.str_split(dtpLeaveEnd.Text.Trim(), splitter);

                if (arinfo.Length == 3)
                {
                    dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                    SiteMaster.ShowClientMessage(Page, "Pass: dtTo()", "error");
                }

                TimeSpan Dur = dtTo.Subtract(dtFrom);

                TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
                if (TotDay < 0)
                {
                    SiteMaster.ShowClientMessage(Page, "From Date Cannot Be Greater Than To Date!", "error");
                    return;
                }
            }

            DataTable dtEmpWeekend = new DataTable();
            dtEmpWeekend = objLeave.SelectEmpWiseWeekend(txtEmpID.Text.Trim());
            DateTime LDate = dtFrom;
            int row;
            int LeaveDay = 0;
            hfLeaveDates.Value = "";
            for (row = 0; row < Convert.ToInt32(TotDay); row++)
            {
                if (dtEmpWeekend.Rows.Count > 0)
                {
                    string DayName = LDate.DayOfWeek.ToString();
                    switch (DayName)
                    {
                        case "Sunday":
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
                                else
                                    hfLeaveDates.Value = LDate.ToString();

                                if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "Y")
                                {
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Sunday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Sunday";
                                    dblTotWeekedDay++;
                                }
                                break;
                            }
                        case "Monday":
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
                                else
                                    hfLeaveDates.Value = LDate.ToString();

                                if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "Y")
                                {
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Monday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Monday";
                                    dblTotWeekedDay++;
                                }
                                break;
                            }
                        case "Tuesday":
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
                                else
                                    hfLeaveDates.Value = LDate.ToString();
                                if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "Y")
                                {
                                    LDate = LDate.AddDays(1);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Tuesday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Tuesday";
                                    dblTotWeekedDay++;
                                }
                                break;
                            }
                        case "Wednesday":
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
                                else
                                    hfLeaveDates.Value = LDate.ToString();
                                if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "Y")
                                {
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Wednesday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Wednesday";
                                    dblTotWeekedDay++;
                                }
                                break;
                            }
                        case "Thursday":
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
                                else
                                    hfLeaveDates.Value = LDate.ToString();
                                if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "Y")
                                {
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Thursday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Thursday";
                                    dblTotWeekedDay++;
                                }
                                break;
                            }
                        case "Friday":
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
                                else
                                    hfLeaveDates.Value = LDate.ToString();

                                if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "Y")
                                {
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Friday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Friday";
                                    dblTotWeekedDay++;
                                }
                                break;
                            }
                        case "Saturday":
                            {

                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
                                else
                                    hfLeaveDates.Value = LDate.ToString();

                                if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "Y")
                                {
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Saturday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Saturday";
                                    dblTotWeekedDay++;
                                }
                                break;
                            }
                            //LDate = LDate.AddDays(1);
                    }
                    LDate = LDate.AddDays(1);
                }
            }

            hdfLDates.Value = hfLeaveDates.Value;
            if (hfWeeekendDay.Value != "")
                SiteMaster.ShowClientMessage(Page, hfWeeekendDay.Value + " Is Weekend", "error");


            if (ddlIsHalfDay.SelectedValue != "0")
                TotDay = TotDay - 0.5;

            txtLDurInDays.Text = TotDay.ToString();
           
        }
        protected void Get_LeaveDate_WithOut_Weekend_Holiday(string strGridView)
        {
            HiddenField hfLeaveDates = new HiddenField();
            HiddenField hfWeeekendDay = new HiddenField();
            HiddenField hfHoliDay = new HiddenField();

            double TotDay = 0;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            hdfLDates.Value = "";
            if (string.IsNullOrEmpty(dtpLeaveStart.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid From Date!", "error");
                return;
            }
            if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid To Date!", "error");
                return;
            }

            if (string.IsNullOrEmpty(dtpResumeDate.Text) == true)
            {
                SiteMaster.ShowClientMessage(Page, "Please Insert Valid Office Resume Date!", "error");
                return;
            }
            if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == false && string.IsNullOrEmpty(dtpLeaveStart.Text) == false)
            {
                dtFrom = Common.ReturnDateTime(dtpLeaveStart.Text, false, Constant.strDateFormat);
                dtTo = Common.ReturnDateTime(dtpLeaveEnd.Text, false, Constant.strDateFormat);
                TimeSpan Dur = dtTo.Subtract(dtFrom);
                TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
                if (TotDay < 0)
                {
                    SiteMaster.ShowClientMessage(Page, "From Date Cannot Be Greater Than To Date!", "error");
                    return;
                }
            }

            DataTable dtEmpWeekend = new DataTable();
            dtEmpWeekend = objLeave.SelectEmpWiseWeekend(txtEmpID.Text.Trim());

            DataTable dtHoliDay = new DataTable();

            DateTime LDate = dtFrom;
            string strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
            int row;
            int LeaveDay = 0;
            dblTotWeekedDay = 0;
            dblTotHoliDay = 0;
            hfLeaveDates.Value = "";

            int i = 0;
            for (i = 0; i < Convert.ToInt32(TotDay); i++)
            {
                //Check for HoliDay
                dtHoliDay.Rows.Clear();
                dtHoliDay.Dispose();
                //dtHoliDay = objLeaveMgr.CheckLvDateBetweenHoliDate(Common.ReturnDateFormat_ddmmyyyy(LDate.ToString(), false),
                //    Common.ReturnDateFormat_ddmmyyyy(LDate.ToString(), true), DateTime.Now.Year.ToString());

                //dtHoliDay = objLeave.CheckLvDateWithHoliDate(Common.ReturnDateTimeInString(LDate.ToString(), false, Constant.strDateFormat),
                //    Common.ReturnDateTimeInString(LDate.ToString(), false, Constant.strDateFormat), DateTime.Now.Year.ToString());
                dtHoliDay = objLeave.CheckLvDateWithHoliDate(strLDate, strLDate, DateTime.Now.Year.ToString());

                if (dtHoliDay.Rows.Count > 0)
                {
                    string strHoliDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtHoliDay.Rows[0]["HoliDate"].ToString(),false,Constant.strDateFormat), false, Constant.strDateFormat);
                    if (strHoliDate != strLDate)
                        hfLeaveDates.Value = hfLeaveDates.Value + strLDate;
                    else
                    {
                        if (hfHoliDay.Value != "")
                            hfHoliDay.Value = hfHoliDay.Value + ", " + strHoliDate;
                        else
                            hfHoliDay.Value = strHoliDate;
                        dblTotHoliDay++;
                    }
                }

                //Check for weekend
                else if (dtEmpWeekend.Rows.Count > 0)
                {
                    string DayName = LDate.DayOfWeek.ToString();
                    switch (DayName)
                    {
                        case "Sunday":
                            {
                                if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "N")
                                {
                                    LeaveDay = LeaveDay + 1;
                                    if (hfLeaveDates.Value != "")
                                        hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
                                    else
                                        hfLeaveDates.Value = strLDate;
                                    break;
                                }
                                else
                                {
                                    LDate = LDate.AddDays(1);
                                    strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(),false,Constant.strDateFormat), false, Constant.strDateFormat);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Sunday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Sunday";
                                    dblTotWeekedDay++;
                                    continue;
                                }
                            }
                        case "Monday":
                            {
                                if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "N")
                                {
                                    LeaveDay = LeaveDay + 1;
                                    if (hfLeaveDates.Value != "")
                                        hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
                                    else
                                        hfLeaveDates.Value = strLDate;
                                    break;
                                }
                                else
                                {
                                    LDate = LDate.AddDays(1);
                                    strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Monday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Monday";
                                    dblTotWeekedDay++;
                                    continue;
                                }
                            }
                        case "Tuesday":
                            {
                                if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "N")
                                {
                                    LeaveDay = LeaveDay + 1;
                                    if (hfLeaveDates.Value != "")
                                        hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
                                    else
                                        hfLeaveDates.Value = strLDate;
                                    break;
                                }
                                else
                                {
                                    LDate = LDate.AddDays(1);
                                    strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Tuesday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Tuesday";
                                    dblTotWeekedDay++;
                                    continue;
                                }
                            }
                        case "Wednesday":
                            {
                                if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "N")
                                {
                                    LeaveDay = LeaveDay + 1;
                                    if (hfLeaveDates.Value != "")
                                        hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
                                    else
                                        hfLeaveDates.Value = strLDate;
                                    break;
                                }
                                else
                                {
                                    LDate = LDate.AddDays(1);
                                    strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Wednesday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Wednesday";
                                    dblTotWeekedDay++;
                                    continue;
                                }
                            }
                        case "Thursday":
                            {
                                if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "N")
                                {
                                    LeaveDay = LeaveDay + 1;
                                    if (hfLeaveDates.Value != "")
                                        hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
                                    else
                                        hfLeaveDates.Value = strLDate;
                                    break;
                                }
                                else
                                {
                                    LDate = LDate.AddDays(1);
                                    strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Thursday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Thursday";
                                    dblTotWeekedDay++;
                                    continue;
                                }
                            }
                        case "Friday":
                            {
                                if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "N")
                                {
                                    LeaveDay = LeaveDay + 1;
                                    if (hfLeaveDates.Value != "")
                                        hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
                                    else
                                        hfLeaveDates.Value = strLDate;
                                    break;
                                }
                                else
                                {
                                    LDate = LDate.AddDays(1);
                                    strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Friday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Friday";
                                    dblTotWeekedDay++;
                                    continue;
                                }
                            }
                        case "Saturday":
                            {
                                if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "N")
                                {
                                    LeaveDay = LeaveDay + 1;
                                    if (hfLeaveDates.Value != "")
                                        hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
                                    else
                                        hfLeaveDates.Value = strLDate;
                                    break;
                                }
                                else
                                {
                                    LDate = LDate.AddDays(1);
                                    strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
                                    if (hfWeeekendDay.Value == "")
                                        hfWeeekendDay.Value = "Saturday";
                                    else
                                        hfWeeekendDay.Value = hfWeeekendDay.Value + ", Saturday";
                                    dblTotWeekedDay++;
                                    continue;
                                }
                            }
                    }
                }
                LDate = LDate.AddDays(1);
                strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
            }

            hdfLDates.Value = hfLeaveDates.Value;
            if (hfWeeekendDay.Value != "")
                SiteMaster.ShowClientMessage(Page, hfWeeekendDay.Value + " Is Weekend", "error");
            //else
            // lblMsg2.Text = "";

            if (hfHoliDay.Value != "")
                SiteMaster.ShowClientMessage(Page, hfHoliDay.Value + " Is Holiday", "error");
            //else
            //    lblMsg3.Text = "";

            if (ddlIsHalfDay.SelectedValue != "0")
                TotDay = TotDay - 0.5;

            TotDay = TotDay - dblTotWeekedDay - dblTotHoliDay;
            txtLDurInDays.Text = TotDay.ToString();
            // lblLDurInDays.Visible = true;
            hfWeeekendDay.Value = "";
            hfHoliDay.Value = "";
           
        }
        private bool CheckMaxLeaveAllow()
        {
            //Checking Maximum leave taken days
            DataTable dtLType = objLeave.SelectLeaveType(Convert.ToInt32(ddlLTypeId.SelectedValue));
            if (dtLType.Rows.Count > 0)
            {
                if (Int16.Parse(dtLType.Rows[0]["LeaveTTL"].ToString().Trim()) > 0 && Int16.Parse(dtLType.Rows[0]["LeaveTTL"].ToString().Trim()) < Int16.Parse(txtLDurInDays.Text.Trim()))
                {
                    SiteMaster.ShowClientMessage(Page, "Maximum " + dtLType.Rows[0]["LeaveTTL"].ToString().Trim() + " consecutive days allowed.", "error");
                    txtLDurInDays.Text = "";
                    txtAvailable.Text = "";
                    ddlLTypeId.SelectedIndex = 0;
                    return false;
                }
            }
            return true;
        }
        protected bool ValidateAndSave()
        {
            try
            {
                
                if (hdfIsUpdate.Value == "N")
                {
                    hdfLvAppID.Value = Common.getMaxId("LeaveAppMst", "LvAppID");
                }
                

                if (ddlLTypeId.SelectedIndex == 0)
                {
                    SiteMaster.ShowClientMessage(Page, "Please Select The Leave Type!", "error");
                    ddlLTypeId.Focus();
                    return false;
                }
                //lblMsg.Text = "test13";

                if (hdfLTypeNature.Value != "5" && hdfLTypeNature.Value != "6")
                {
                    if (txtAvailable.Text.Trim() == "0")
                    {
                        SiteMaster.ShowClientMessage(Page, "No leave Is Available For The Leave Type!", "error");
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtAvailable.Text.Trim()) == false && string.IsNullOrEmpty(txtLDurInDays.Text.Trim()) == false)
                    {
                        if (Convert.ToDouble(txtAvailable.Text) < Convert.ToDouble(txtLDurInDays.Text))
                        {
                            SiteMaster.ShowClientMessage(Page, "Leave Cannot Be Taken More Than Available Leave!", "error");
                            return false;
                        }
                    }
                }
                //lblMsg.Text = "test12";

                if (string.IsNullOrEmpty(dtpLeaveStart.Text) == true)
                {
                    SiteMaster.ShowClientMessage(Page, "Please Enter The Leave From Date!", "error");
                    return false;
                }
                //lblMsg.Text = "test11";

                if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == true)
                {
                    SiteMaster.ShowClientMessage(Page, "Please Enter The Leave To Date!", "error");
                    return false;
                }
                //lblMsg.Text = "test10";

                if ((txtLDurInDays.Text.Trim() == string.Empty) || (txtLDurInDays.Text.Trim() == "0"))
                {
                    dtpLeaveStart.Enabled = true;
                    dtpLeaveEnd.Enabled = true;
                    SiteMaster.ShowClientMessage(Page, "Please Enter Valid Leave Date From and Date To!", "error");
                    return false;
                }
                        //lblMsg.Text = "test text";
                if (Common.CheckStartEndDate(dtpResumeDate.Text.Trim(), dtpLeaveEnd.Text.Trim()) == false)
                {
                    SiteMaster.ShowClientMessage(Page, "Resume Date Cannot Before Than Leave To Date!", "error");
                    return false;
                }
                        //lblMsg.Text = "test9";
                //Leave Taken Barrier      
                //DateTime dtLvStart = Common.ReturnDateTime(dtpLeaveStart.Text.Trim(), false, Constant.strDateFormat);
                //dtLvStart = dtLvStart.AddDays(-1);
                //string strLvDate = dtLvStart.ToString();
                //DataTable dtPreDayLv = objLeave.PrevDayLeave(txtEmpID.Text.Trim(), strLvDate);
                //if (dtPreDayLv.Rows.Count > 0)
                //{
                //    DataTable dtLvTkBar = objLeave.CheckLvTakenBarrier();
                //    foreach (DataRow dRow in dtLvTkBar.Rows)
                //    {
                //        if (dtPreDayLv.Rows[0]["LTypeId"].ToString() == dRow["PLTypeId"].ToString())
                //        {
                //            if (ddlLTypeId.SelectedValue.ToString() == dRow["NLTypeId"].ToString())
                //            {
                //                SiteMaster.ShowClientMessage(Page, ddlLTypeId.SelectedItem.Text + " Can't Take. B'coz Previous Day " + dRow["LTypeTitle"].ToString() + " Has Already Taken!", "error");
                //                return false;
                //            }
                //        }
                //    }
                //}
                //lblMsg.Text = "test8";

                //Employees leave already existed in this leave dates
                DataTable dtLvExisted = new DataTable();
                string[] arinfo = new string[10];
                char[] splitter = { ',' };
                int i = 0;
                int j = 0;
                        //lblMsg.Text = "test7";
                if (string.IsNullOrEmpty(hdfLDates.Value.ToString()) == false)
                    arinfo = Common.str_split(hdfLDates.Value, splitter);
                else
                    arinfo = Common.str_split(hdfPreLDates.Value, splitter);
                //lblMsg.Text = "test6";

                for (i = 0; i < arinfo.Length; i++)
                {
                    if (string.IsNullOrEmpty(arinfo[i]) == false)
                    {
                        dtLvExisted = objLeave.SelectEmpLeaveDateDetails(Convert.ToInt32(hdfLvAppID.Value),
                            txtEmpID.Text.Trim(), arinfo[i].ToString());
                        if (dtLvExisted.Rows.Count > 0)
                        {
                            for (j = 0; j < dtLvExisted.Rows.Count; j++)
                            {
                                DateTime dtLevDate;
                                DateTime dtAllDate;
                                dtLevDate = Convert.ToDateTime(dtLvExisted.Rows[j]["LevDate"].ToString());
                                dtAllDate = Convert.ToDateTime(arinfo[i].ToString());
                                TimeSpan DateDiff = dtLevDate.Subtract(dtAllDate);
                                string strTotDay = Common.ReturnTotalDay(DateDiff.ToString());
                                if (strTotDay == "00:00:00")
                                {
                                    SiteMaster.ShowClientMessage(Page, txtEmpID.Text.Trim() + " Already Applied For Leave For The Date " + Common.ReturnDateTimeInString(arinfo[i].ToString(), false, Constant.strDateFormat) + " !", "error");
                                    return false;
                                }
                            }
                        }
                    }
                }
                //lblMsg.Text = "test5";
                dtLvExisted.Rows.Clear();
                dtLvExisted.Dispose();

                //Check Leave start date is in between leave period
                DataTable dtLvPeriod = objLeave.CheckLvDateBetweenLeavePeriod(hdfLeavePakId.Value.ToString(), Common.ReturnDateTimeInString(dtpLeaveStart.Text.Trim(), false, Constant.strDateFormat));
                if (dtLvPeriod.Rows.Count > 0)
                {   
                //lblMsg.Text = "test1rtrt:"+dtLvPeriod.Rows.Count;
                    SiteMaster.ShowClientMessage(Page, "Please Renew Leave Period of " + txtEmpID.Text.Trim() + " Leave Package !", "error");
                    return false;
                }
               
                //// Check for Leave End Period 
                //if (Convert.ToDateTime(Common.ReturnDateTimeInString(dtpLeaveEnd.Text.Trim(), false, Constant.strDateFormat)) > Convert.ToDateTime(hdfLvPackEndDate.Value.Trim()))
                //{
                //    SiteMaster.ShowClientMessage(Page, "Your Leave Year Will Be Ended " + Common.ReturnDateTimeInString(hdfLvPackEndDate.Value.Trim(), false, Constant.strDateFormat) + ". Any Leave Beyond The Date Cannot Be Taken !", "error");
                //lblMsg.Text = "test41";
                //    return false;
                //}
                //Leave Taken Barrier
                //DateTime dtPreLvdate = Convert.ToDateTime(Common.DisplayDateTime(dtpLeaveStart.Text.Trim(), false, Constant.strDateFormat)).AddDays(-1);
                //DataTable dtPreDayLvDtl = objLeave.SelectEmpLeaveDateDetails(0, txtEmpID.Text.Trim(), dtPreLvdate.ToString());
                //lblMsg.Text = "test2";

                //if (dtPreDayLv.Rows.Count > 0)
                //{
                //    DataTable dtLvTkBar = objLeave.CheckLvTakenBarrier();
                //    foreach (DataRow dRow in dtLvTkBar.Rows)
                //    {
                //        if (Convert.ToInt16(dtPreDayLv.Rows[0]["LTypeId"].ToString()) == Convert.ToInt16(dRow["PLTypeId"].ToString()))
                //        {
                //            if (ddlLTypeId.SelectedValue.ToString() == dRow["NLTypeId"].ToString())
                //            {
                //                SiteMaster.ShowClientMessage(Page, txtEmpID.Text.Trim() + " can not take " + ddlLTypeId.SelectedItem.Text + " on " + dtpLeaveStart.Text.Trim() + ". Because previous day " + dRow["LTypeTitle"].ToString() + " has already taken.", "error");
                //                return false;
                //            }
                //        }
                //    }
                //}
                //lblMsg.Text = "test1";
                //return false;
                return true ;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void RefreshForm()
        {
            Common.EmptyTextBoxValues(this);
            dtpLeaveStart.Enabled = true;
            dtpLeaveEnd.Enabled = true;
            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();
        }
        private void RefreshControls()
        {
            Control[] cntlArr = { hdfLvAppID, hdfLvPackStartDate, hdfLvPackEndDate, hdfLTypeNature, hdfIsUpdate, hdfPreLTypeId,
                                   hdfIsOffdayCounted, hdfLAbbrName, hdfLEnjoyed, hdfLDates,hdfPreLDates,hdfLeavePakId,hdfPreLEnjoyed,
                                    hdfFiscalYrId,dtpLeaveStart,dtpLeaveEnd,ddlIsHalfDay,dtpResumeDate,ddlLTypeId,txtAvailable,
                                    txtLDurInDays,txtLTReason,txtAddrAtLeave,txtPhoneNo};

            Common.EmptyTextBoxValues(cntlArr);

            dtpLeaveStart.Enabled = true;
            dtpLeaveEnd.Enabled = true;
            this.OpenRecord();
        }
        private void SaveData(string cmdType)
        {
            dsEmployee objDS = new dsEmployee();

            double EDay = 0;
            if (cmdType == "I")
            {
                hdfLvAppID.Value = Common.getMaxId("LeaveAppMst", "LvAppID");
                EDay = Math.Round(Convert.ToDouble(hdfLEnjoyed.Value)) + Convert.ToDouble(txtLDurInDays.Text.Trim());
                hdfLEnjoyed.Value = Convert.ToString(EDay);
            }
            else
            {
                if (ddlLTypeId.SelectedValue == hdfPreLTypeId.Value)
                {
                    EDay = Math.Round(Convert.ToDouble(hdfLEnjoyed.Value) - Convert.ToDouble(hdfPreLEnjoyed.Value)) + Convert.ToDouble(txtLDurInDays.Text.Trim());
                    hdfLEnjoyed.Value = Convert.ToString(EDay);
                }
                else
                {
                    EDay = Math.Round(Convert.ToDouble(hdfLEnjoyed.Value)) + Convert.ToDouble(txtLDurInDays.Text.Trim());
                    hdfLEnjoyed.Value = Convert.ToString(EDay);

                    hdfPreLEnjoyed.Value = hdfPreLEnjoyed.Value;
                }
            }
            hdfAppStatus.Value = "P";

            List<DataRow> lstRow = new List<DataRow>();

            // MASTER TABLE
            DataTable dtMst = objDS.Tables["LeaveAppMst"];
            DataRow mRow = dtMst.NewRow();
            mRow = Common.SetSingleTableFormData(mRow, this.Controls, Session["USERID"].ToString().Trim(), cmdType);
            lstRow.Add(mRow);
            // END OF MASTER TABLE
               
            // DETAILS TABLE
            DataTable dtDet = objDS.Tables["LeaveAppDet"];
            string[] arinfo = hdfLDates.Value.ToString().Split(',');
            foreach (string info in arinfo)
            {
                DataRow dRow = dtDet.NewRow();
                dRow["LvAppID"] = Convert.ToDecimal(hdfLvAppID.Value);
                dRow["EmpID"] = txtEmpID.Text.Trim();
                //dRow["LevDate"] = Common.ReturnDateTime(info, false, Constant.strDateFormat);
                dRow["LevDate"] = DateTime.Parse(info);
                dRow["LTypeId"] = Convert.ToDecimal(ddlLTypeId.SelectedValue.Trim());
                dRow["Duration"] = 1;
                dRow["InsertedBy"] = Session["USERID"].ToString().Trim();
                dRow["InsertedDate"] = DateTime.Now;
                lstRow.Add(dRow);
            }
            // END OF DETAILS TABLE
            try
            {
                //lblMsg.Text = "Level2";
                objLeave.SaveData(lstRow, cmdType == "D" ? "U" : cmdType);
                SiteMaster.ShowClientMessage(Page, Common.GetMessage(cmdType), "success");
                
                this.SendMail();
                this.RefreshControls();
                
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
                throw new Exception(ex.Message);
            }
        }
        private void SendMail()
        {
            //lblMsg.Text = "Lel3";
            string message = "";
            MailManagerSmtpClient objMail = new MailManagerSmtpClient();
            #region mail sending

            DateTime LvStDate = Convert.ToDateTime(dtpLeaveStart.Text);
            DateTime LvEnDate = new DateTime();
            if (dtpLeaveEnd.Text != "")
                LvEnDate = Convert.ToDateTime(dtpLeaveEnd.Text);
            string emp = Session["EMPID"].ToString();
            string user = Session["USERNAME"].ToString();
            string des = Session["DESIGNATION"].ToString();
            string loc = Session["LOCATION"].ToString();//now static
            if (string.IsNullOrEmpty(hdfSupervisorEmail.Value))
            {
                message = "Leave applied but mail did not send";
            }
            else
            {
                //Open this part for mail  
                //lblMsg.Text = "Level4";
                message = objMail.RequestForApproval(txtEmpID.Text.Trim(), hdfLvAppID.Value.ToString(),
                    Common.SetDate(LvStDate.ToShortDateString()), dtpLeaveEnd.Text != "" ? Common.SetDate(LvEnDate.ToShortDateString()) : "",
                    Session["EMPID"].ToString(), Session["USERNAME"].ToString(),
                    Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                    Session["USERID"].ToString().Trim().ToUpper() == "SYSTEM" ? "Y" : "N", hdfSupervisorId.Value.ToString(),
                    hdfSupervisorEmail.Value.ToString());
            }
            SiteMaster.ShowClientMessage(Page, message, "info");
            ////Open New Window
            //StringBuilder sb = new StringBuilder();
            //string strURL = "LeaveApplicationRpt.aspx?params=" + txtEmpID.Text.Trim() + "," + hdfLvAppID.Value.ToString() + ", R";
            //sb.Append("<script>");
            ////sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
            //sb.Append("window.open('" + strURL + "', '', '');");
            //sb.Append("</script>");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
            //                         sb.ToString(), false);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
            ////ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
            #endregion
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
                btnEmpSearch.Enabled = false;
                txtEmpID.ReadOnly = true;
                this.GetSearchResult();
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }

        //protected void grSupervisee_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow row = grSupervisee.SelectedRow;
        //    txtEmpID.Text = row.Cells[0].Text.Trim();
        //    this.GetSearchResult();
        //}
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    const string onMouseOverStyle = "this.className='GridViewMouseOver';";
        //    const string onMouseOutStyle = "this.className='{0}';";

        //    foreach (GridViewRow gvr in grSupervisee.Rows)
        //    {
        //        gvr.Attributes["onmouseover"] = onMouseOverStyle;
        //        gvr.Attributes["onmouseout"] = String.Format(
        //            onMouseOutStyle,
        //            this.GetRowStyleCssClass(gvr.RowState));
        //        gvr.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(
        //            grSupervisee,
        //            String.Concat("Select$", gvr.RowIndex),
        //            true);
        //    }

        //    base.Render(writer);
        //}
        private string GetRowStyleCssClass(DataControlRowState state)
        {
            if ((state & DataControlRowState.Edit) > 0)
            {
                return grSupervisee.EditRowStyle.CssClass;
            }
            else if ((state & DataControlRowState.Selected) > 0)
            {
                return grSupervisee.SelectedRowStyle.CssClass;
            }
            else if ((state & DataControlRowState.Alternate) > 0)
            {
                return grSupervisee.AlternatingRowStyle.CssClass;
            }
            else
            {
                return grSupervisee.RowStyle.CssClass;
            }
        }
        protected void grSupervisee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;
            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            _gridView.SelectedIndex = _selectedIndex;

            switch (_commandName)
            {
                case ("Select"):
                    {
                        txtEmpID.Text = grSupervisee.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                        this.GetSearchResult();
                        break;
                    }
            }
        }
    }
}