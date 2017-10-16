using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.BLL;
using System.Data;

namespace WebAdmin.UserControls.EIS
{
    public partial class ctlEmpBloodGroupSearch : System.Web.UI.UserControl
    {
        EmpInfoManager objEmpInfo = new EmpInfoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common.FillDropDownList(objEmpInfo.SelectDistrict(0), ddlDistrict, "PostingDistName", "PostingDistId",true,"All");
                Common.FillDropDownList(objEmpInfo.SelectBloodGroupList(0), ddlBloodGroup, "BloodGroupName", "BloodGroupId", true,"All");
                Common.FillDropDownList(objEmpInfo.SelectEmpTypeList(0), ddlEmpType, "TypeName", "EmpTypeID", true, "All");
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            DataTable dtBloodGroup = objEmpInfo.GetEmployeeInfo(txtEmpId.Text.Trim(),ddlDistrict.SelectedValue.Trim(),ddlBloodGroup.SelectedValue.Trim(),ddlEmpStatus.SelectedValue.Trim(),ddlEmpType.SelectedValue.Trim(),"","");
            grBloodGroup.DataSource = dtBloodGroup;
            grBloodGroup.DataBind();
            this.FormatGridView();
        }
        private void FormatGridView()
        {
            foreach (GridViewRow gRow in grBloodGroup.Rows)
            {
                if(gRow.Cells[3].Text.Trim()!=string.Empty)
                gRow.Cells[3].Text = Common.DisplayDateTime(gRow.Cells[3].Text.Trim(), false, Constant.strDateFormat);
            }
        }
    }
}