using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Threading;
using System.Data;
using System.Text;
using CrystalDecisions.Shared;

namespace WebAdmin.UserControls.Travel
{
    public partial class ctlTravelStatusList : System.Web.UI.UserControl
    {
        //LeaveManager objLeaveMgr = new LeaveManager();
        EmpTravelManager objTravelMgr = new EmpTravelManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null)
                {
                    Thread.Sleep(1000);
                    Response.Redirect("~/Login.aspx");
                }
                this.OpenRecord();
            }
        }
        private void OpenRecord()
        {
            grTravelStatusList.DataSource = null;
            grTravelStatusList.DataBind();

            string strStartDate = DateTime.Now.Year.ToString();
            string strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);

            strStartDate = strStartDate + "-" + "01" + "-" + "01";
            strEndDate = strEndDate + "-" + "12" + "-" + "31";
            DataTable dtLeaveDeny = new DataTable();
            DataTable dtTravel = new DataTable();
            if (Session["ISADMIN"].ToString() == "N")
            {
                //dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, Session["EMPID"].ToString().Trim(), "PRDCA", strStartDate, strEndDate, "");
                dtTravel = objTravelMgr.GetTravelApp(0, Session["EMPID"].ToString().Trim(), "PRDCA", strStartDate, strEndDate, "");
            }
            else
            {
                //dtLeaveDeny = objLeaveMgr.SelectRequestLeaveAppMst(0, "N", "PR", strStartDate, strEndDate, "");
                dtTravel = objTravelMgr.GetTravelApp(0, "N", "PR", strStartDate, strEndDate, "");
            }

            grTravelStatusList.DataSource = dtTravel;
            grTravelStatusList.DataBind();
            this.FormatDenyGridDate();
            dtTravel.Rows.Clear();
            dtTravel.Dispose();
        }
        protected void FormatDenyGridDate()
        {
            int SlNo = 0;
            foreach (GridViewRow gRow in grTravelStatusList.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[0].Text = SlNo.ToString();
                gRow.Cells[1].Text = grTravelStatusList.DataKeys[SlNo - 1].Values[11].ToString() + " [" + gRow.Cells[1].Text.ToUpper() + "]" +                    
                    "<br/> Applied On : " + Common.DisplayDateTime(grTravelStatusList.DataKeys[SlNo - 1].Values[3].ToString(), false, Constant.strDateFormat);
                gRow.Cells[2].Text = Common.DisplayDateTime(grTravelStatusList.DataKeys[SlNo - 1].Values[4].ToString(), false, Constant.strDateFormat) +
                  " To " + Common.DisplayDateTime(grTravelStatusList.DataKeys[SlNo - 1].Values[5].ToString(), false, Constant.strDateFormat) +
                  "<br/> Duration : " + Convert.ToString(Math.Round(Convert.ToDouble(grTravelStatusList.DataKeys[SlNo - 1].Values[6].ToString()), 1));
            }
            SlNo = 0;
        }
        protected void grTravelStatusList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GenerateReport(grTravelStatusList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim(), grTravelStatusList.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim(), grTravelStatusList.DataKeys[_gridView.SelectedIndex].Values[13].ToString().Trim());
                    break;
            }
            this.OpenRecord();
        }
        private void GenerateReport(string travelId,string empId,string appStatus)
        {
            string ReportPath = "";
            CrystalDecisions.CrystalReports.Engine.ReportDocument ReportDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            StringBuilder sb = new StringBuilder();
            string fileName = Session["USERID"].ToString() + "_" + "TravelApp" + ".pdf";
            ReportPath = Server.MapPath("~/CrystalReports/rptTravelApplication.rpt");
            ReportDoc.Load(ReportPath);

            DataTable travelData = objTravelMgr.SelectEmpTravelRpt(travelId, empId, appStatus);

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
        private void ExPortReport(CrystalDecisions.CrystalReports.Engine.ReportDocument ReportDoc, string rptPath)
        {
            CrystalDecisions.Shared.ExportOptions CrExportOptions;
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
    }
}