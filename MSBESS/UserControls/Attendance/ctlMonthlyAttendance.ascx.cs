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
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                Common.FillYearList(5, ddlYear);
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
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

        protected void grAttnAdj_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRow row = ((System.Data.DataRowView)e.Row.DataItem).Row;
                if (row["Status"].ToString().Trim() == "A")
                    e.Row.BackColor = System.Drawing.Color.Red;
                else if (row["Status"].ToString().Trim() == "P")
                    e.Row.BackColor = System.Drawing.Color.Green;
                else if (row["Status"].ToString().Trim() == "W")
                    e.Row.BackColor = System.Drawing.Color.Blue;
                else if (row["Status"].ToString().Trim() == "LT")
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                else if (row["Status"].ToString().Trim() == "X")
                    e.Row.BackColor = System.Drawing.Color.Pink;
            }
        }
    }
}