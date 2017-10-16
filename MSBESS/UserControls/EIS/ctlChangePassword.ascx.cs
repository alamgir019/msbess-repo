using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebAdmin.BLL;
using WebAdmin.DAL;

namespace WebAdmin.UserControls.EIS
{
    public partial class ctlChangePassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUserId.Text = HttpContext.Current.Session["USERID"].ToString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAndSave() == true)
            {
                this.SaveData("N");
            }
        }
        protected bool ValidateAndSave()
        {
            if (string.IsNullOrEmpty(txtUserId.Text) == false)
            {
                string password = txtOldPass.Text.ToString();
                string strInputPwd = Common.getHashValue(password);
                DataTable dtUser = new DataTable();
                UserManager objUserMgr = new UserManager();
                dtUser = objUserMgr.SelectUserInfo(txtUserId.Text.Trim(), "Y");

                if (dtUser.Rows.Count > 0)
                {
                    foreach (DataRow row in dtUser.Rows)
                    {
                        if (string.Compare(row["Password"].ToString().Trim(), strInputPwd) != 0)
                        {
                            SiteMaster.ShowClientMessage(Page, "Old Password is not valid. Please check the password.", "error");
                            txtOldPass.Focus();
                            return false;
                        }
                    }
                }

                if (string.Compare(Common.getHashValue(txtNewPass.Text.Trim()), Common.getHashValue(txtConfNewPass.Text.Trim())) != 0)
                {
                    SiteMaster.ShowClientMessage(Page, "New Password fields dont match with each other.", "error");
                    txtNewPass.Focus();
                    return false;
                }
            }
            return true;
        }

        private void SaveData(string IsDelete)
        {
            try
            {
                UserManager objUserMgr = new UserManager();
                UserCreation objUser = new UserCreation(txtUserId.Text.Trim(), Common.getHashValue(txtNewPass.Text.Trim()));

                objUserMgr.UpdatePassword(objUser);

                SiteMaster.ShowClientMessage(Page, "Password Updated Successfully.", "success");
                Common.EmptyTextBoxValues(this);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}