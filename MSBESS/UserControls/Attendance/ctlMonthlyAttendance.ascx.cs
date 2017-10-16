using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Data;

namespace WebAdmin.UserControls.Attendance
{
    public partial class ctlMonthlyAttendance : System.Web.UI.UserControl
    {
        ReportManager objRM = new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common.FillMonthList(ddlMonth);
                Common.FillYearList(5, ddlYear);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            DataTable dtAttnAdj = objRM.GetMonthlyAtendanceData(Session["EMPID"].ToString().Trim(),ddlMonth.SelectedValue.ToString().Trim(),
                ddlYear.SelectedValue.ToString().Trim());

            grAttnAdj.DataSource = dtAttnAdj;
            grAttnAdj.DataBind();
            //foreach (GridViewRow gRow in grAttnAdj.Rows)
            //{
            //    gRow.Cells[4].Text = Common.ReturnDateTimeInString(gRow.Cells[4].Text,false,Constant.strDateFormat);
            //}
        }
    }
}