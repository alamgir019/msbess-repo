using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Data;
using System.IO;

namespace WebAdmin.UserControls.EIS
{
    public partial class ctlPhotoUpload : System.Web.UI.UserControl
    {
        MasterTablesManager objMasMgr = new MasterTablesManager();
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // ROLE WISE FORM  DATA AUTO FILLUP
                if (Session["ISADMIN"].ToString().Trim() == "N")
                {
                    txtEmpId.Text = Session["EMPID"].ToString().Trim();
                    btnEmpSearch.Enabled = false;
                    txtEmpId.ReadOnly = true;
                    this.GetEmpSearchResult();
                }
            }
        }
        protected void GetEmpSearchResult()
        {
            if (txtEmpId.Text.Trim() == "")
            {
                Common.EmptyTextBoxValues(this);
                return;
            }

            DataTable dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(txtEmpId.Text.Trim());
            if (dtEmpInfo.Rows.Count == 0)
            {
                Common.EmptyTextBoxValues(this);
                return;
            }
            else
            {
                if (dtEmpInfo.Rows[0]["EmpImage"].ToString().Trim() != string.Empty)
                {
                    MemoryStream ms = new MemoryStream((byte[])dtEmpInfo.Rows[0]["EmpImage"]);
                    Byte[] imgByte = ms.ToArray();

                    string base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);
                    imgEmpImage.ImageUrl = "data:image/png;base64," + base64String;
                    ms = null;
                    imgByte = null;
                }            
              

                txtFullName.Text = dtEmpInfo.Rows[0]["FullName"].ToString().Trim();
            }
        }
        protected void btnEmpSearch_Click(object sender, EventArgs e)
        {
            if (Session["ISADMIN"].ToString().Trim() == "Y")
            {
                this.GetEmpSearchResult();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(Session["PHOTO"]!=null)
            {
                string empid = "";
                if (Session["ISADMIN"].ToString().Trim() == "N")
                {
                    empid = Session["EMPID"].ToString().Trim();
                }
                else
                {
                    empid = txtEmpId.Text.Trim();
                }
                objEmpInfoMgr.UpdatePhoto((Byte[])Session["PHOTO"], empid);
                SiteMaster.ShowClientMessage(Page, "Image Updated Successfully!", "success");
                this.GetEmpSearchResult();
            }
            else
            {
                SiteMaster.ShowClientMessage(Page, "Please Browse and Select a File to Update!", "error");
            }
        
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshForm();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }
        protected void RefreshForm()
        {
            Common.EmptyTextBoxValues(this);
            Session["PHOTO"] = null;
            Session["PHOTO"] = "";
            txtEmpId.Text = Session["EMPID"].ToString().Trim();
            btnEmpSearch.Enabled = false;
            txtEmpId.ReadOnly = true;
            this.GetEmpSearchResult();
        }
       
        protected void AsyncFileUpload1_OnUploadedComplete(object sender, EventArgs e)
        {
            Session["PHOTO"] = null;
            Session["PHOTO"] = "";
            HttpPostedFile File = AsyncFileUpload1.PostedFile;
            Byte[] imgByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgByte, 0, File.ContentLength);
            Session["PHOTO"] = imgByte;
        }
    }
}