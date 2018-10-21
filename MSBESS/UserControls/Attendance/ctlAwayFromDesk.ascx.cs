using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebAdmin.App_Data;
using WebAdmin.BLL;


namespace WebAdmin.UserControls.Attendance
{
    public partial class ctlAwayFromDesk : System.Web.UI.UserControl
    {
        MasterTablesManager objMasMgr = new MasterTablesManager();
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        dsEmployee objDS = new dsEmployee();
        AttendanceManager objAM = new AttendanceManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.OpenRecord();
            }
        }
        private void OpenRecord()
        {
            lblDateTime.Text = "Now Date: " + DateTime.Now.ToShortDateString() + "     Time: " + DateTime.Now.ToShortTimeString();
            DataTable dtLog = objAM.getDeskAwayLog(Session["EMPID"].ToString().Trim(),"","");
            if (dtLog.Rows.Count > 0)
            {
                hdfSINO.Value = dtLog.Rows[0]["SLNO"].ToString().Trim();
                txtReason.Text = dtLog.Rows[0]["Reason"].ToString().Trim();
                hdfOutTime.Value = dtLog.Rows[0]["OutTime"].ToString().Trim();
                btnSave.Text = "Save Desk InTime";
            }
            else
            {
                hdfSINO.Value = "";
                txtReason.Text = "";
                btnSave.Text = "Save Desk OutTime";
            }
            DataTable dtAwayUsers = objAM.getDeskAwayLog("", "intime", DateTime.Now.ToShortDateString());
            grDeskAway.DataSource = dtAwayUsers;
            grDeskAway.DataBind();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "$('#myModal').modal('show');", true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromDate = Common.ReturnDateTimeInString(dtpFromDate.Text, false, Constant.strDateFormat);
            string toDate = Common.ReturnDateTimeInString(dtpToDate.Text, false, Constant.strDateFormat);
            DataTable dtAwayUsers = objAM.getDeskAwayReport(fromDate, toDate, Session["EMPID"].ToString().Trim());
            grDeskAway.DataSource = dtAwayUsers;
            grDeskAway.DataBind();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "$('#myModal').modal('show');", true);
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            dtpFromDate.Text = "";
            dtpToDate.Text = "";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(string), Guid.NewGuid().ToString(), "$('#myModal').modal('show');", true);
        }
        private void SaveData()
        {
            string cmdType;
            dsEmployee objDS = new dsEmployee();

            DataTable dtMst = objDS.Tables["EmpAwayDeskLog"];
            DataRow nRow = dtMst.NewRow();

            if (hdfSINO.Value==string.Empty)
            {
                cmdType = "I";
                hdfSINO.Value = Common.getMaxIdVar("EmpAwayDeskLog", "SLNo");
                nRow["OutTime"] = DateTime.Now.ToShortTimeString();
            }
            else
            {
                cmdType = "U";
                nRow["InTime"] =DateTime.Now.ToShortTimeString();
                nRow["OutTime"] = hdfOutTime.Value.ToString().Trim();
            }            
            
            nRow["SLNo"] =Int64.Parse(hdfSINO.Value);
            nRow["EMPID"] = Session["EMPID"].ToString().Trim();
            nRow["LogDate"] = DateTime.Now.ToShortDateString();
            
            
            nRow["Reason"] = txtReason.Text.Trim();
            dtMst.Rows.Add(nRow);
            dtMst.AcceptChanges();
            try
            {
                objEmpInfoMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);
                SiteMaster.ShowClientMessage(Page, Common.GetMessage(cmdType), "success");
                this.OpenRecord();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }
    }
}