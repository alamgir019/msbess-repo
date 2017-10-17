using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.Pages
{
    public partial class EmpFileViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // string strParams = Request.QueryString["params"];
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(Server.MapPath(Session["FILEPATH"].ToString()));

            if (buffer != null)
            {
                if (Session["extension"].ToString()==".PDF")
                {
                    Response.ContentType = "application/pdf";
                }
                else if (Session["extension"].ToString() == ".PNG")
                {
                    Response.ContentType = "image/png";
                }
                else if (Session["extension"].ToString() == ".JPEG" || Session["extension"].ToString() == ".JPG")
                {
                    Response.ContentType = "image/jpeg";
                }
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
        }
    }
}