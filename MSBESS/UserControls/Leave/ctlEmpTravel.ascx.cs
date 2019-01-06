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

        //private void GetLeaveTypeDependency()
        //{
        //    txtAvailable.Text = "";
        //    DataTable dtLType = new DataTable();
        //    dtLType = objLeave.SelectLeaveType(Convert.ToInt32(ddlLTypeId.SelectedValue));
        //    if (dtLType.Rows.Count > 0)
        //    {
        //        hdfLTypeNature.Value = dtLType.Rows[0]["LNature"].ToString();
        //        hdfIsOffdayCounted.Value = dtLType.Rows[0]["IsOffdayCounted"].ToString().Trim();
        //    }
        //    if ((ddlLTypeId.SelectedValue != "-1"))
        //    {
        //        try
        //        {
        //            this.AvailableLeave();

        //        }
        //        catch
        //        {
        //            throw new Exception ("From AvailableLeave 1st");
        //        }
        //    }
        //}
        //protected void Calculate_Click()
        //{
        //    if (hdfIsUpdate.Value == "Y" && hdfPreLTypeId.Value == ddlLTypeId.SelectedValue.ToString())
        //    {
        //        this.AvailableLeave();
        //        if (txtAvailable.Text != "" && txtLDurInDays.Text != "" && (hdfLTypeNature.Value != "5") && (hdfLTypeNature.Value != "6"))
        //            txtAvailable.Text = Convert.ToString(Convert.ToDouble(txtAvailable.Text));
        //    }
        //    if (dtpLeaveStart.Text.Trim() == dtpLeaveEnd.Text.Trim())
        //    {
        //        DataTable dtWeekend = objLeave.SelectEmpWiseWeekend(txtEmpID.Text.Trim());
        //        if (Common.IsWeekendDay(Common.ReturnDateTime(dtpLeaveStart.Text.Trim(), false, Constant.strDateFormat), dtWeekend) == true)
        //        {
        //            dtWeekend.Rows.Clear();
        //            dtWeekend.Dispose();
        //            SiteMaster.ShowClientMessage(Page, "Pass: IsWeekendDay()", "error");
        //            this.Get_LeaveDate_With_Weekend_Holiday();
        //            SiteMaster.ShowClientMessage(Page, "Pass: Get_LeaveDate_With_Weekend_Holiday()", "error");
        //            return;
        //        }
        //    }

        //    if ((hdfIsOffdayCounted.Value.ToString() == "Y") || (hdfLAbbrName.Value.ToString() == "ML"))
        //        this.Get_LeaveDate_With_Weekend_Holiday();
        //    else
        //        this.Get_LeaveDate_WithOut_Weekend_Holiday("A");
        //}
        //private void AvailableLeave()
        //{
        //    if (ddlLTypeId.SelectedValue != "-1")
        //    {
        //        if (txtEmpID.Text.Trim() != "")
        //        {
        //            DataTable dtLeaveProfile = new DataTable();
        //            dtLeaveProfile = objLeave.SelectEmpLeaveProfile(txtEmpID.Text.Trim(), ddlLTypeId.SelectedValue.ToString());
        //            decimal intAvail = 0;
        //            decimal LCarryOverd = 0;
        //            decimal LEntitled = 0;
        //            decimal LEnjoyed = 0;
        //            decimal LeaveElapsed = 0;
        //            txtAvailable.Text = "";
        //            if (dtLeaveProfile.Rows.Count > 0)
        //            {
        //                if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString()) == false)
        //                    LCarryOverd = LCarryOverd + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString());
        //                else
        //                    LCarryOverd = 0;

        //                if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["lvPrevYearCarry"].ToString()) == false)
        //                    LCarryOverd = LCarryOverd + Convert.ToDecimal(dtLeaveProfile.Rows[0]["lvPrevYearCarry"].ToString());

        //                if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LEntitled"].ToString()) == false)
        //                    LEntitled = LEntitled + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LEntitled"].ToString());
        //                else
        //                    LEntitled = 0;
        //                if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
        //                    hdfLEnjoyed.Value = dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString();
        //                else
        //                    hdfLEnjoyed.Value = "0";
        //                if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveElapsed"].ToString()) == false)
        //                    LeaveElapsed = LeaveElapsed + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveElapsed"].ToString());
        //                else
        //                    LeaveElapsed = 0;

        //                if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["lvOpening"].ToString()) == false)
        //                    LEnjoyed = LEnjoyed + Convert.ToDecimal(dtLeaveProfile.Rows[0]["lvOpening"].ToString());
        //                else
        //                    LEnjoyed = 0;

        //                hdfLAbbrName.Value = dtLeaveProfile.Rows[0]["LAbbrName"].ToString();

        //                if (hdfLTypeNature.Value != "5" && hdfLTypeNature.Value != "6")
        //                {
        //                    intAvail = (LCarryOverd + LEntitled) - (Convert.ToDecimal(hdfLEnjoyed.Value) + LeaveElapsed + LEnjoyed);

        //                    txtAvailable.Text = Convert.ToString(Math.Round(intAvail, 1));
        //                    if (Convert.ToDecimal(txtAvailable.Text) < 0)
        //                    {
        //                        txtAvailable.Text = "0";
        //                    }
        //                }
        //                else
        //                    intAvail = Convert.ToDecimal(hdfLEnjoyed.Value);
        //            }
        //        }
        //    }
        //}
        //protected void Get_LeaveDate_With_Weekend_Holiday()
        //{
        //    HiddenField hfLeaveDates = new HiddenField();
        //    HiddenField hfWeeekendDay = new HiddenField();

        //    double TotDay = 0;
        //    DateTime dtFrom = new DateTime();
        //    DateTime dtTo = new DateTime();

        //    if (string.IsNullOrEmpty(dtpLeaveStart.Text) == true)
        //    {
        //        SiteMaster.ShowClientMessage(Page, "Please Insert Valid From Date!", "error");
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == true)
        //    {
        //        SiteMaster.ShowClientMessage(Page, "Please Insert Valid To Date!", "error");
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(dtpResumeDate.Text) == true)
        //    {
        //        SiteMaster.ShowClientMessage(Page, "Please Insert Valid Office Resume Date!", "error");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == false && string.IsNullOrEmpty(dtpLeaveStart.Text) == false)
        //    {
        //        char[] splitter = { '/' };
        //        string[] arinfo = Common.str_split(dtpLeaveStart.Text.Trim(), splitter);
        //        if (arinfo.Length == 3)
        //        {
        //            dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
        //            arinfo = null;
        //            SiteMaster.ShowClientMessage(Page, "Pass: dtFrom()", "error");
        //        }
        //        arinfo = Common.str_split(dtpLeaveEnd.Text.Trim(), splitter);

        //        if (arinfo.Length == 3)
        //        {
        //            dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
        //            arinfo = null;
        //            SiteMaster.ShowClientMessage(Page, "Pass: dtTo()", "error");
        //        }

        //        TimeSpan Dur = dtTo.Subtract(dtFrom);

        //        TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        //        if (TotDay < 0)
        //        {
        //            SiteMaster.ShowClientMessage(Page, "From Date Cannot Be Greater Than To Date!", "error");
        //            return;
        //        }
        //    }

        //    DataTable dtEmpWeekend = new DataTable();
        //    dtEmpWeekend = objLeave.SelectEmpWiseWeekend(txtEmpID.Text.Trim());
        //    DateTime LDate = dtFrom;
        //    int row;
        //    int LeaveDay = 0;
        //    hfLeaveDates.Value = "";
        //    for (row = 0; row < Convert.ToInt32(TotDay); row++)
        //    {
        //        if (dtEmpWeekend.Rows.Count > 0)
        //        {
        //            string DayName = LDate.DayOfWeek.ToString();
        //            switch (DayName)
        //            {
        //                case "Sunday":
        //                    {
        //                        LeaveDay = LeaveDay + 1;
        //                        if (hfLeaveDates.Value != "")
        //                            hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
        //                        else
        //                            hfLeaveDates.Value = LDate.ToString();

        //                        if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "Y")
        //                        {
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Sunday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Sunday";
        //                            dblTotWeekedDay++;
        //                        }
        //                        break;
        //                    }
        //                case "Monday":
        //                    {
        //                        LeaveDay = LeaveDay + 1;
        //                        if (hfLeaveDates.Value != "")
        //                            hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
        //                        else
        //                            hfLeaveDates.Value = LDate.ToString();

        //                        if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "Y")
        //                        {
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Monday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Monday";
        //                            dblTotWeekedDay++;
        //                        }
        //                        break;
        //                    }
        //                case "Tuesday":
        //                    {
        //                        LeaveDay = LeaveDay + 1;
        //                        if (hfLeaveDates.Value != "")
        //                            hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
        //                        else
        //                            hfLeaveDates.Value = LDate.ToString();
        //                        if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "Y")
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Tuesday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Tuesday";
        //                            dblTotWeekedDay++;
        //                        }
        //                        break;
        //                    }
        //                case "Wednesday":
        //                    {
        //                        LeaveDay = LeaveDay + 1;
        //                        if (hfLeaveDates.Value != "")
        //                            hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
        //                        else
        //                            hfLeaveDates.Value = LDate.ToString();
        //                        if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "Y")
        //                        {
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Wednesday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Wednesday";
        //                            dblTotWeekedDay++;
        //                        }
        //                        break;
        //                    }
        //                case "Thursday":
        //                    {
        //                        LeaveDay = LeaveDay + 1;
        //                        if (hfLeaveDates.Value != "")
        //                            hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
        //                        else
        //                            hfLeaveDates.Value = LDate.ToString();
        //                        if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "Y")
        //                        {
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Thursday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Thursday";
        //                            dblTotWeekedDay++;
        //                        }
        //                        break;
        //                    }
        //                case "Friday":
        //                    {
        //                        LeaveDay = LeaveDay + 1;
        //                        if (hfLeaveDates.Value != "")
        //                            hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
        //                        else
        //                            hfLeaveDates.Value = LDate.ToString();

        //                        if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "Y")
        //                        {
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Friday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Friday";
        //                            dblTotWeekedDay++;
        //                        }
        //                        break;
        //                    }
        //                case "Saturday":
        //                    {

        //                        if (hfLeaveDates.Value != "")
        //                            hfLeaveDates.Value = hfLeaveDates.Value + "," + LDate.ToString();
        //                        else
        //                            hfLeaveDates.Value = LDate.ToString();

        //                        if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "Y")
        //                        {
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Saturday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Saturday";
        //                            dblTotWeekedDay++;
        //                        }
        //                        break;
        //                    }
        //            }
        //            LDate = LDate.AddDays(1);
        //        }
        //    }

        //    hdfLDates.Value = hfLeaveDates.Value;
        //    if (hfWeeekendDay.Value != "")
        //        SiteMaster.ShowClientMessage(Page, hfWeeekendDay.Value + " Is Weekend", "error");


        //    if (ddlIsHalfDay.SelectedValue != "0")
        //        TotDay = TotDay - 0.5;

        //    txtLDurInDays.Text = TotDay.ToString();

        //}
        //protected void Get_LeaveDate_WithOut_Weekend_Holiday(string strGridView)
        //{
        //    HiddenField hfLeaveDates = new HiddenField();
        //    HiddenField hfWeeekendDay = new HiddenField();
        //    HiddenField hfHoliDay = new HiddenField();

        //    double TotDay = 0;
        //    DateTime dtFrom = new DateTime();
        //    DateTime dtTo = new DateTime();
        //    hdfLDates.Value = "";
        //    if (string.IsNullOrEmpty(dtpLeaveStart.Text) == true)
        //    {
        //        SiteMaster.ShowClientMessage(Page, "Please Insert Valid From Date!", "error");
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == true)
        //    {
        //        SiteMaster.ShowClientMessage(Page, "Please Insert Valid To Date!", "error");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(dtpResumeDate.Text) == true)
        //    {
        //        SiteMaster.ShowClientMessage(Page, "Please Insert Valid Office Resume Date!", "error");
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(dtpLeaveEnd.Text) == false && string.IsNullOrEmpty(dtpLeaveStart.Text) == false)
        //    {
        //        dtFrom = Common.ReturnDateTime(dtpLeaveStart.Text, false, Constant.strDateFormat);
        //        dtTo = Common.ReturnDateTime(dtpLeaveEnd.Text, false, Constant.strDateFormat);
        //        TimeSpan Dur = dtTo.Subtract(dtFrom);
        //        TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        //        if (TotDay < 0)
        //        {
        //            SiteMaster.ShowClientMessage(Page, "From Date Cannot Be Greater Than To Date!", "error");
        //            return;
        //        }
        //    }

        //    DataTable dtEmpWeekend = new DataTable();
        //    dtEmpWeekend = objLeave.SelectEmpWiseWeekend(txtEmpID.Text.Trim());

        //    DataTable dtHoliDay = new DataTable();

        //    DateTime LDate = dtFrom;
        //    string strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //    int LeaveDay = 0;
        //    dblTotWeekedDay = 0;
        //    dblTotHoliDay = 0;
        //    hfLeaveDates.Value = "";

        //    int i = 0;
        //    for (i = 0; i < Convert.ToInt32(TotDay); i++)
        //    {
        //        dtHoliDay.Rows.Clear();
        //        dtHoliDay.Dispose();
        //        dtHoliDay = objLeave.CheckLvDateWithHoliDate(strLDate, strLDate, DateTime.Now.Year.ToString());

        //        if (dtHoliDay.Rows.Count > 0)
        //        {
        //            string strHoliDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(dtHoliDay.Rows[0]["HoliDate"].ToString(),false,Constant.strDateFormat), false, Constant.strDateFormat);
        //            if (strHoliDate != strLDate)
        //                hfLeaveDates.Value = hfLeaveDates.Value + strLDate;
        //            else
        //            {
        //                if (hfHoliDay.Value != "")
        //                    hfHoliDay.Value = hfHoliDay.Value + ", " + strHoliDate;
        //                else
        //                    hfHoliDay.Value = strHoliDate;
        //                dblTotHoliDay++;
        //            }
        //        }
        //        else if (dtEmpWeekend.Rows.Count > 0)
        //        {
        //            string DayName = LDate.DayOfWeek.ToString();
        //            switch (DayName)
        //            {
        //                case "Sunday":
        //                    {
        //                        if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "N")
        //                        {
        //                            LeaveDay = LeaveDay + 1;
        //                            if (hfLeaveDates.Value != "")
        //                                hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
        //                            else
        //                                hfLeaveDates.Value = strLDate;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(),false,Constant.strDateFormat), false, Constant.strDateFormat);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Sunday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Sunday";
        //                            dblTotWeekedDay++;
        //                            continue;
        //                        }
        //                    }
        //                case "Monday":
        //                    {
        //                        if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "N")
        //                        {
        //                            LeaveDay = LeaveDay + 1;
        //                            if (hfLeaveDates.Value != "")
        //                                hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
        //                            else
        //                                hfLeaveDates.Value = strLDate;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Monday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Monday";
        //                            dblTotWeekedDay++;
        //                            continue;
        //                        }
        //                    }
        //                case "Tuesday":
        //                    {
        //                        if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "N")
        //                        {
        //                            LeaveDay = LeaveDay + 1;
        //                            if (hfLeaveDates.Value != "")
        //                                hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
        //                            else
        //                                hfLeaveDates.Value = strLDate;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Tuesday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Tuesday";
        //                            dblTotWeekedDay++;
        //                            continue;
        //                        }
        //                    }
        //                case "Wednesday":
        //                    {
        //                        if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "N")
        //                        {
        //                            LeaveDay = LeaveDay + 1;
        //                            if (hfLeaveDates.Value != "")
        //                                hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
        //                            else
        //                                hfLeaveDates.Value = strLDate;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Wednesday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Wednesday";
        //                            dblTotWeekedDay++;
        //                            continue;
        //                        }
        //                    }
        //                case "Thursday":
        //                    {
        //                        if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "N")
        //                        {
        //                            LeaveDay = LeaveDay + 1;
        //                            if (hfLeaveDates.Value != "")
        //                                hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
        //                            else
        //                                hfLeaveDates.Value = strLDate;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Thursday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Thursday";
        //                            dblTotWeekedDay++;
        //                            continue;
        //                        }
        //                    }
        //                case "Friday":
        //                    {
        //                        if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "N")
        //                        {
        //                            LeaveDay = LeaveDay + 1;
        //                            if (hfLeaveDates.Value != "")
        //                                hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
        //                            else
        //                                hfLeaveDates.Value = strLDate;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Friday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Friday";
        //                            dblTotWeekedDay++;
        //                            continue;
        //                        }
        //                    }
        //                case "Saturday":
        //                    {
        //                        if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "N")
        //                        {
        //                            LeaveDay = LeaveDay + 1;
        //                            if (hfLeaveDates.Value != "")
        //                                hfLeaveDates.Value = hfLeaveDates.Value + "," + strLDate;
        //                            else
        //                                hfLeaveDates.Value = strLDate;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            LDate = LDate.AddDays(1);
        //                            strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //                            if (hfWeeekendDay.Value == "")
        //                                hfWeeekendDay.Value = "Saturday";
        //                            else
        //                                hfWeeekendDay.Value = hfWeeekendDay.Value + ", Saturday";
        //                            dblTotWeekedDay++;
        //                            continue;
        //                        }
        //                    }
        //            }
        //        }
        //        LDate = LDate.AddDays(1);
        //        strLDate = Common.ReturnDateTimeInString(Common.DisplayDateTime(LDate.ToString(), false, Constant.strDateFormat), false, Constant.strDateFormat);
        //    }

        //    hdfLDates.Value = hfLeaveDates.Value;
        //    if (hfWeeekendDay.Value != "")
        //        SiteMaster.ShowClientMessage(Page, hfWeeekendDay.Value + " Is Weekend", "error");
        //    if (hfHoliDay.Value != "")
        //        SiteMaster.ShowClientMessage(Page, hfHoliDay.Value + " Is Holiday", "error");
        //    if (ddlIsHalfDay.SelectedValue != "0")
        //        TotDay = TotDay - 0.5;

        //    TotDay = TotDay - dblTotWeekedDay - dblTotHoliDay;
        //    txtLDurInDays.Text = TotDay.ToString();
        //    hfWeeekendDay.Value = "";
        //    hfHoliDay.Value = "";

        //}
        //private bool CheckMaxLeaveAllow()
        //{
        //    DataTable dtLType = objLeave.SelectLeaveType(Convert.ToInt32(ddlLTypeId.SelectedValue));
        //    if (dtLType.Rows.Count > 0)
        //    {
        //        if (float.Parse(dtLType.Rows[0]["LeaveTTL"].ToString().Trim()) > 0 && float.Parse(dtLType.Rows[0]["LeaveTTL"].ToString().Trim()) < float.Parse(txtLDurInDays.Text.Trim()))
        //        {
        //            SiteMaster.ShowClientMessage(Page, "Maximum " + dtLType.Rows[0]["LeaveTTL"].ToString().Trim() + " consecutive days allowed.", "error");
        //            txtLDurInDays.Text = "";
        //            txtAvailable.Text = "";
        //            ddlLTypeId.SelectedIndex = 0;
        //            return false;
        //        }
        //    }
        //    return true;
        //}
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
                objTravel.TravelStatus = "R";
                objTravel.InsertedBy = Session["USERID"].ToString();
                objTravel.InsertedDate = Common.SetDate(DateTime.Now.ToString());
                objTravel.ProjectId = ddlProject.SelectedValue;
                objTravelMgr.InsertEmpTravel(objTravel, hdfIsUpdate.Value.ToString(), "N");
                this.SendMail(txtTotalDays.Text);
                this.GenerateReport();
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

        private void GenerateReport()
        {

            string ReportPath = "";
            ReportDocument ReportDoc = new ReportDocument();
            StringBuilder sb = new StringBuilder();
            string TravelId = hdfTvAppID.Value;
            string EmpId = txtEmpID.Text;
            string TravelStatus = "R";
            string fileName = Session["USERID"].ToString() + "_" + "TravelApp" + ".pdf";
            ReportPath = Server.MapPath("~/CrystalReports/rptTravelApplication.rpt");
            ReportDoc.Load(ReportPath);

            EmpTravelManager objTravelManager = new EmpTravelManager();
            DataTable travelData = objTravelManager.SelectEmpTravelRpt(TravelId, EmpId, TravelStatus);

            ReportDoc.SetDataSource(travelData);
            this.ExPortReport(ReportDoc, fileName);
            this.OpenWindow(fileName, sb);
        }

        private void OpenWindow(string fileName, StringBuilder sb)
        {
            sb.Append("<script>");
            sb.Append("window.open('/CrystalReports/VirtualReport/" + fileName + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                     sb.ToString(), false);
        }
        private void ExPortReport(ReportDocument ReportDoc, string rptPath)
        {
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/CrystalReports/VirtualReport/" + rptPath);
            CrExportOptions = ReportDoc.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            ReportDoc.Export();
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
        private void SendMail(string minHour)
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

            mailManager.strSubject = "Request to Verify Travel Recomendation";
            
            mailManager.strBody = "Please verify the following Application: "
                          + " \n "
                           + " Total Days : " + minHour + ","
                           + " \n\n "
                           + "With thanks "
                          + " \n\n ";

            DataTable empUserFrom = new DataTable();
            DataTable empUserTo = new DataTable();
            empUserFrom = empManager.SelectEmpInfo(Session["EMPLOYEEID"].ToString());
            if (empUserFrom.Rows.Count > 0)
            {
                mailManager.strFromAddr = empUserFrom.Rows[0]["OfficeEmail"].ToString();
                mailManager.strBody += empUserFrom.Rows[0]["FullName"].ToString() + " \n ";
            }
            empUserTo = empManager.SelectEmpInfo(empUserFrom.Rows[0]["SupervisorId"].ToString());
            if (empUserTo.Rows.Count > 0)
            {
                mailManager.strToAddr = empUserTo.Rows[0]["OfficeEmail"].ToString();
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