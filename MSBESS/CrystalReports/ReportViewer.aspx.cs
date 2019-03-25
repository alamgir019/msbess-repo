using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;

namespace WebAdmin.Pages
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rep= Request["rep"];
            if (!string.IsNullOrEmpty(rep))
            {
                Session["REPORTID"] = Request["rep"];
            }
        }
    }
}