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
                
                //Common.FillDropDownList(objEmpInfoMgr.SelectReligionList(0), ddlReligionId, "ReligionName", "ReligionId", true, "Select");
                //Common.FillDropDownList(objEmpInfoMgr.SelectHomeDistrict(0), ddlPerDistrictID, "DistName", "DistId", true, "Select");
                //Common.FillDropDownList(objEmpInfoMgr.SelectCountry(0), ddlPerCountryID, "CountryName", "CountryID", true, "Select");
                //Common.FillDropDownList(objEmpInfoMgr.SelectBloodGroupList(0), ddlBloodGroupId, "BloodGroupName", "BloodGroupId", true, "Select");

                //// ROLE WISE FORM  DATA AUTO FILLUP
                //if(Session["ISADMIN"].ToString().Trim()=="N")
                //{
                //    txtEmpId.Text = Session["EMPID"].ToString().Trim();
                //    btnEmpSearch.Enabled = false;
                //    txtEmpId.ReadOnly = true;
                //    this.GetEmpSearchResult();
                //}
                    

            }
        }

        //protected void GetEmpSearchResult()
        //{
        //    if (txtEmpId.Text.Trim() == "")
        //    {
        //        Common.EmptyTextBoxValues(this);
        //        return;
        //    }

        //    DataTable dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(txtEmpId.Text.Trim());
        //    if (dtEmpInfo.Rows.Count == 0)
        //    {
        //        Common.EmptyTextBoxValues(this);
        //        return;
        //    }
        //    else
        //    {
        //        Common.PrepareEditView(dtEmpInfo.Rows[0], this.Controls);
        //    }
        //}
        //protected void btnEmpSearch_Click(object sender, EventArgs e)
        //{
        //    if (Session["ISADMIN"].ToString().Trim() == "Y")
        //    {
        //        this.GetEmpSearchResult();
        //    }
        //}
        //protected void btnSave_Click(object sender, EventArgs e)
        //{           
        //       this.SaveData("U");
        //}
        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    this.RefreshForm();
        //}
        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Pages/Default.aspx");
        //}
        //protected void RefreshForm()
        //{
        //    Common.EmptyTextBoxValues(this);
        //}
        //private void SaveData(string cmdType)
        //{
        //    dsEmployee objDS = new dsEmployee();

        //    DataTable dtMst = objDS.Tables["EmpInfo"];
            
        //    DataRow nRow = dtMst.NewRow();
        //    nRow = Common.SetSingleTableFormData(nRow, this.Controls, Session["USERID"].ToString().Trim(), cmdType);
        //    dtMst.Rows.Add(nRow);
        //    dtMst.AcceptChanges();
        //    try
        //    {
        //        objEmpInfoMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);
        //        SiteMaster.ShowClientMessage(Page, Common.GetMessage(cmdType), "success");                
        //        this.RefreshForm();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}
       
    }

    

}