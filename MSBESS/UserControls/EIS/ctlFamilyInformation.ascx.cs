using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.App_Data;
using WebAdmin.BLL;
using WebAdmin.DAL;

namespace WebAdmin.UserControls.EIS
{    public partial class ctlFamilyInformation : System.Web.UI.UserControl
    {
        MasterTablesManager objMasMgr = new MasterTablesManager();
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        DataAccess objDAL = new DataAccess();
        dsEmployee objDS = new dsEmployee();

        DataTable dtEmpInfo = new DataTable();
        DataTable dtNominee = new DataTable();             
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Common.FillDropDownList(objMasMgr.SelectRelationList(0), ddlRelation, "RelationName", "RelationId", true, "Select");
                this.EntryMode(false);

                if (Session["ISADMIN"].ToString().Trim() == "N")
                {
                    txtEmpId.Text = Session["EMPID"].ToString().Trim();
                    btnEmpSearch.Enabled = false;
                    txtEmpId.ReadOnly = true;
                    this.GetSearchResult();
                }
            }
        }
        protected void EntryMode(bool IsUpdate)
        {
            if (IsUpdate == true)
            {
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
                hfID.Value = "";
                txtName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                ddlRelation.SelectedIndex = -1;
            }
        }
        protected void RefreshForm()
        {
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            grList.DataSource = null;
            grList.DataBind();
        }
        private void OpenRecord()
        {
            grList.DataSource = objEmpInfoMgr.SelectFamilyInfo(txtEmpId.Text.Trim(), 0);
            grList.DataBind();
        }
        protected void btnEmpSearch_Click(object sender, EventArgs e)
        {
            if (Session["ISADMIN"].ToString().Trim() == "Y")
            {
                this.GetSearchResult();
            }
        }

        protected void GetSearchResult()
        {
            if (txtEmpId.Text.Trim() == "")
                return;

            dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(txtEmpId.Text.Trim());
            if (dtEmpInfo.Rows.Count == 0)
            {
                txtEmpName.Text = "";
                txtJobTitle.Text = "";
                txtCompany.Text = "";
                txtProject.Text = "";
                txtDepartment.Text = "";
                return;
            }
            else
            {
                txtEmpName.Text = dtEmpInfo.Rows[0]["FullName"].ToString();
                txtJobTitle.Text = dtEmpInfo.Rows[0]["DesigName"].ToString().Trim();
                txtCompany.Text = dtEmpInfo.Rows[0]["DivisionName"].ToString().Trim();
                txtProject.Text = dtEmpInfo.Rows[0]["ProjectName"].ToString().Trim();
                txtDepartment.Text = dtEmpInfo.Rows[0]["DeptName"].ToString().Trim();
           
                this.OpenRecord();
            }
        }
        private void SaveData(string cmdType)
        {
            dsEmployee objDS = new dsEmployee();
            if (cmdType == "I")
            {
                hfID.Value = Common.getMaxId("EmpFamilyInfo", "FmID");
            }

            DataTable dtMst = objDS.Tables["EmpFamilyInfo"];
            DataRow nRow = dtMst.NewRow();
            string msg = "saved";

            nRow["EmpID"] = txtEmpId.Text.Trim();
            nRow["FmID"] = Common.RoundDecimal(hfID.Value, 0);
            nRow["FmName"] =txtName.Text.Trim();
            nRow["FmAddr"] = txtAddress.Text.Trim();
            nRow["FmPhone"] = txtPhone.Text.Trim();
            nRow["RelationId"] =Convert.ToDecimal(ddlRelation.SelectedValue.ToString().Trim());

            if (cmdType == "I")
            {                
                nRow["InsertedBy"] = Session["USERID"].ToString().Trim();
                nRow["InsertedDate"] = DateTime.Now;                
            }
            else if (cmdType == "U")
            {
                nRow["UpdatedBy"] = Session["USERID"].ToString().Trim();
                nRow["UpdatedDate"] = DateTime.Now;
                nRow["LastUpdatedFrom"] = Session["USERID"].ToString().Trim();
                msg = "updated";
            }

            if (cmdType == "D")
            {
                nRow["IsDeleted"] = "Y";
                msg = "deleted";
            }
            else
            {
                nRow["IsDeleted"] = "N";
            }

            dtMst.Rows.Add(nRow);
            dtMst.AcceptChanges();

            try
            {
                objDAL.SaveDataTable(dtMst, cmdType == "D" ? "U" : cmdType);

                SiteMaster.ShowClientMessage(Page, "Data "+msg+" successfully!", "success");

                //SiteMaster.ShowToastr(Page, msg, msg);
                this.EntryMode(false);
                this.OpenRecord();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
          protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfID.Value == "")
            {
                this.SaveData("I");
            }
            else
            {
                this.SaveData("U");
            }
        }
        protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;
            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            _gridView.SelectedIndex = _selectedIndex;

            switch (_commandName)
            {
                case ("EditClick"):
                    {
                        hfID.Value= grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                        txtName.Text= grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                        ddlRelation.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                        txtAddress.Text= grList.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                        txtPhone.Text = grList.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                        
                        this.EntryMode(true);
                    }
                    break;
                case ("DeleteClick"):
                    {
                        hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                        txtName.Text = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                        txtAddress.Text = grList.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                        txtPhone.Text = grList.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                        ddlRelation.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                        this.SaveData("D");
                    }
                    break;
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshForm();
            this.OpenRecord();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }
       
    }
}