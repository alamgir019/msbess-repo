using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.App_Data;
using WebAdmin.BLL;
using System.Data;

namespace WebAdmin.UserControls.EIS
{
    public partial class ctlEmpPresentStatus : System.Web.UI.UserControl
    {
        MasterTablesManager objMasMgr = new MasterTablesManager();
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();       
        dsEmployee objDS = new dsEmployee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // ROLE WISE FORM  DATA AUTO FILLUP
                if (Session["ISADMIN"].ToString().Trim() == "N")
                {
                    txtEmpId.Text = Session["EMPID"].ToString().Trim();
                    txtEmpId.ReadOnly = true;
                    dtpStatusDate.Text = DateTime.Now.ToShortDateString();
                    dtpStatusDate.ReadOnly = true;
                    this.GetEmpSearchResult();
                    this.GetEmpPresentStatus();
                }
            }
        }

        protected void GetEmpPresentStatus()
        {
            DataTable dtEmpPresentStatus = objEmpInfoMgr.SelectEmpPresentStatus(txtEmpId.Text.Trim());
            if (dtEmpPresentStatus.Rows.Count == 0)
            {
                hdfIsUpdate.Value = "N";
            }
            else
            {
                hdfIsUpdate.Value = "Y";
                Common.PrepareEditView(dtEmpPresentStatus.Rows[0], this.Controls);
                hdfStatus.Value = "1";
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
                Common.PrepareEditView(dtEmpInfo.Rows[0], this.Controls);
            }
        }       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hdfIsUpdate.Value == "N")
                this.SaveData("I");
            else
                this.SaveData("U");
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/EmpPresentStatus.aspx");
            //this.RefreshForm();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }
        protected void RefreshForm()
        {
            Common.EmptyTextBoxValues(this);
        }
        private void SaveData(string cmdType)
        {
            dsEmployee objDS = new dsEmployee();

            DataTable dtMst = objDS.Tables["EmpPresentStatus"];

            DataRow nRow = dtMst.NewRow();
            nRow = Common.SetSingleTableFormData(nRow, this.Controls, Session["USERID"].ToString().Trim(), cmdType);
            if (cmdType == "I")
            {
                hdfStatusId.Value = Common.getMaxId("EmpPresentStatus", "StatusId");
            }
            nRow["StatusId"] = Convert.ToDecimal(hdfStatusId.Value);
            dtMst.Rows.Add(nRow);
            dtMst.AcceptChanges();
            try
            {
                objEmpInfoMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);
                //SiteMaster.ShowClientMessage(Page, Common.GetMessage(cmdType), "success");
                //Response.Redirect("~/Pages/EmpPresentStatus.aspx");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

    

}