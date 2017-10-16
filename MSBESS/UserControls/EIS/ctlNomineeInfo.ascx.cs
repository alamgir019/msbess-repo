using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.App_Data;
using WebAdmin.BLL;


namespace WebAdmin.UserControls.EIS
{
    public partial class ctlNomineeInfo : System.Web.UI.UserControl
    {
        MasterTablesManager objMasMgr = new MasterTablesManager();
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        dsEmployee objDS = new dsEmployee();

        DataTable dtEmpInfo = new DataTable();
        DataTable dtNominee = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Common.FillDropDownList(objMasMgr.SelectRelationList(0), ddlRelation, "RelationName", "RelationId", true, "Select");
                this.EntryMode(false);
                // ROLE WISE FORM  DATA AUTO FILLUP
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
                txtNomineeName.Text = "";
                txtDOB.Text = "";
                txtRemarks.Text = "";
                ddlRelation.SelectedIndex = -1;
                ddlGender.SelectedIndex = 0;
                txtAddress.Text = "";
                txtShare.Text = "";
                cblNomTypeList.ClearSelection();
                ViewState["SelectShareAmt"] = 0;
            }
        }
        protected void RefreshForm()
        {
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            grList.DataSource = null;
            grList.DataBind();
            ViewState["SelectShareAmt"] = 0;
        }
        private void OpenRecord()
        {
            grList.DataSource = objEmpInfoMgr.SelectNominee(txtEmpId.Text.Trim(), "");
            grList.DataBind();
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
        protected void btnEmpSearch_Click(object sender, EventArgs e)
        {
            if (Session["ISADMIN"].ToString().Trim() == "Y")
            {
                this.GetSearchResult();
            }
        }

        protected bool ValidateAndSave(string cmdType)
        {
            double totalShare = 0;
            //For Death beneficiary checking. Total share amount <=100%.
            if (cblNomTypeList.Items.FindByValue("D").Selected == true)
            {
                if ((txtShare.Text.Trim() == string.Empty || double.Parse(txtShare.Text.Trim()) <= 0))
                {
                    SiteMaster.ShowClientMessage(Page, "Please enter valid share amount.", "error");
                    return false;
                }
                else
                {
                    for (int i = 0; i < grList.Rows.Count; i++)
                    {
                        totalShare += double.Parse(grList.DataKeys[i].Values[8].ToString().Trim());
                    }
                    if (cmdType == "U")
                    {
                        totalShare -= double.Parse(ViewState["SelectShareAmt"].ToString().Trim());
                    }
                    if (totalShare + double.Parse(txtShare.Text.Trim()) > 100)
                    {
                        txtShare.Text = (100 - totalShare).ToString();
                        SiteMaster.ShowClientMessage(Page, "Total share amount is over limit.", "error");
                        return false;
                    }
                   
                }
            }
           
            return true;
        }
        private void SaveData(string cmdType)
        {

            if (this.ValidateAndSave(cmdType) == false)
                return;

            
            dsEmployee objDS = new dsEmployee();

            if (cmdType == "I")
            {
                hfID.Value = Common.getMaxId("Nominee", "NomineeId");
            }

            DataTable dtMst = objDS.Tables["Nominee"];
            DataRow nRow = dtMst.NewRow();        
           
            nRow["NomineeId"] = Common.RoundDecimal(hfID.Value, 0);
            nRow["EmpID"] = txtEmpId.Text.Trim();
            nRow["NomineeName"] = txtNomineeName.Text.Trim();
            nRow["RelationId"] = Convert.ToDecimal(ddlRelation.SelectedValue.ToString().Trim());
            //nRow["DOB"] =Common.ReturnDateTime(Common.DisplayDateTime(txtDOB.Text.Trim(),false, Constant.strDateFormat), false, Constant.strDateFormat);
            nRow["DOB"] = Common.ReturnDateTime(txtDOB.Text.Trim(), false, Constant.strDateFormat);
            nRow["Gender"] = ddlGender.SelectedValue.ToString().Trim();
            nRow["IsMedical"] = "N";
            nRow["IsDeath"] = "N";
            nRow["Share"] = 0;

            if (cblNomTypeList.Items.FindByValue("M").Selected == true)
            {
                nRow["IsMedical"] = "Y";
            }
           
            if (cblNomTypeList.Items.FindByValue("D").Selected == true)
            {
                nRow["IsDeath"] = "Y";
                nRow["Share"] = txtShare.Text.Trim();
            }
                   
            nRow["Address"] = txtAddress.Text.Trim();
            nRow["Remarks"] = txtRemarks.Text.Trim();

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
            }

            nRow["IsDeleted"] = (cmdType == "D" ? "Y" : "N");
           
            dtMst.Rows.Add(nRow);
            dtMst.AcceptChanges();

            try
            {
                objEmpInfoMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);

                SiteMaster.ShowClientMessage(Page,Common.GetMessage(cmdType), "success");
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
                        hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                        txtNomineeName.Text = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                        ddlRelation.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                        ddlGender.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                        txtDOB.Text = grList.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();

                        if (grList.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim()=="Y")
                            cblNomTypeList.Items.FindByValue("M").Selected=true;
                        else
                            cblNomTypeList.Items.FindByValue("M").Selected = false;

                        if (grList.DataKeys[_gridView.SelectedIndex].Values[7].ToString().Trim() == "Y")
                            cblNomTypeList.Items.FindByValue("D").Selected = true;
                        else
                            cblNomTypeList.Items.FindByValue("D").Selected = false;

                        txtShare.Text =grList.DataKeys[_gridView.SelectedIndex].Values[8].ToString().Trim();
                        ViewState["SelectShareAmt"] = double.Parse(grList.DataKeys[_gridView.SelectedIndex].Values[8].ToString().Trim());
                        txtAddress.Text = grList.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim();
                        txtRemarks.Text = grList.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim();                      
                        this.EntryMode(true);
                    }
                    break;
                case ("DeleteClick"):
                    {
                        hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                        txtNomineeName.Text = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                        ddlRelation.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                        ddlGender.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                        txtDOB.Text = grList.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                        txtRemarks.Text = grList.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                        this.SaveData("D");
                    }
                    break;
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshForm();
            // ROLE WISE FORM  DATA AUTO FILLUP
            if (Session["ISADMIN"].ToString().Trim() == "N")
            {
                txtEmpId.Text = Session["EMPID"].ToString().Trim();
                btnEmpSearch.Enabled = false;
                txtEmpId.ReadOnly = true;
                this.GetSearchResult();
            }
            else
            {
                this.OpenRecord();
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }
    }
}