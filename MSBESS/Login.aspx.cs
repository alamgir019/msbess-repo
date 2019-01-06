using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebAdmin.BLL;
using System.IO;
using System.Text;
using WebAdmin.App_Data;

namespace WebAdmin
{
    public partial class Login : System.Web.UI.Page
    {
        LoginManager objLogin = new LoginManager();
        AttendanceManager objAM = new AttendanceManager();
        dsEmployee objDS = new dsEmployee();
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["USERID"] = "";
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strFiscalYear = "";
            string strFiscalStartDate = "";
            string userid = txtUserID.Text.ToString().Trim();
            string strInputPwd = Common.getHashValue(txtPassword.Text.ToString().Trim());

            if (userid == string.Empty || txtPassword.Text.ToString().Trim()==string.Empty)
            {
                this.ShowClientMessage(Page, "Please Enter User ID and Password!", "error");
                return;
            }
            DataTable dtUser = objLogin.SelectUserInfo(userid, "Y");

            // Payroll Fiscal Year
            DataTable dtPayOpt = objLogin.SelectpaySlipOption("OC03");
            if (dtPayOpt.Rows.Count > 0)
            {
                strFiscalYear = dtPayOpt.Rows[0]["OPTVALUE"].ToString().Trim();
                strFiscalStartDate = dtPayOpt.Rows[0]["PAYROLLVALIDFROM"].ToString().Trim();
            }

            if (dtUser.Rows.Count > 0)
            {
                Session["LOGINID"] = Common.getMaxId("UserInOutHistory", "LogInId");
                foreach (DataRow row in dtUser.Rows)
                {
                    //if (strInputPwd != "")
                    //{
                    if (string.Compare(row["Password"].ToString().Trim(), strInputPwd) == 0)
                    {
                        if (strInputPwd != "0")
                        {
                            Session["USERID"] = userid.ToString();
                            Session["USERNAME"] = row["FullName"].ToString();
                            Session["EMPID"] = row["EMPID"].ToString().Trim();
                            Session["EMAILID"] = row["OfficeEmail"].ToString();
                            Session["DIVISIONID"]= row["DivisionId"].ToString();
                            Session["OFFICE"] = row["DivisionName"].ToString();
                            Session["TEAM"] = row["DEPTNAME"].ToString();
                            Session["TEAMID"] = row["DEPTID"].ToString();
                            Session["ISADMIN"] = row["IsAdmin"].ToString().Trim();
                            Session["DESIGNATION"] = row["JobTitleName"].ToString().Trim();
                            Session["DESIGID"] = row["JobTitleId"].ToString().Trim();
                            Session["LOCATION"] = "Dhaka";
                            Session["JOINDATE"] = Common.DisplayDateTime(row["JoiningDate"].ToString().Trim(),false,Constant.strDateFormat);
                            Session["FISCALYRID"] = strFiscalYear;
                            Session["FISCALSTARTDATE"] = strFiscalStartDate;
                            Session["IsCountryDirector"] = row["IsCountryDirector"] == null ? "" : row["IsCountryDirector"].ToString();
                            //if (Common.CheckNullString(row["EmpImage"].ToString().Trim()) != "")
                            //{
                            //    MemoryStream ms = new MemoryStream((byte[])row["EmpImage"]);
                            //    Byte[] imgByte = ms.ToArray();

                            //    string base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);
                            //    Session["PHOTO"] = "data:image/png;base64," + base64String;
                            //}
                            //else
                            //{
                            //    Session["PHOTO"] = "~/Content/images/NoImageSmall.jpg";
                            //}

                            this.InsertUserInOutHistory("S");
                            this.CheckAwayDeskLog();
                            //DataTable dtTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "1", "T103");
                            //if (dtTaskPermission.Rows.Count > 0)
                            //    Response.Redirect("File/Home.aspx");
                            //else
                            //    Response.Redirect("Default.aspx");
                            this.ShowClientMessage(Page, "Login Successful!", "success");
                            Response.Redirect("Pages/Default.aspx");
                        }
                        else
                        {
                            this.ResetSessionParam();
                            Response.Redirect("~/Login.aspx");

                            this.InsertUserInOutHistory("U");

                            this.FillOptionValue();
                        }
                    }
                    else
                    {
                        Session["USERID"] = txtUserID.Text.Trim();
                        this.InsertUserInOutHistory("U");
                        this.ShowClientMessage(Page, "row"+row["Password"].ToString().Trim()+"input:"+ strInputPwd,"error");//"Invalid User ID or Password!", "error");
                    }
                }
            }
            else
            {
                this.ResetSessionParam();
                Session["USERID"] = txtUserID.Text.Trim();
                this.InsertUserInOutHistory("U");
                Response.Redirect("~/Login.aspx");
                this.ShowClientMessage(Page, "You have been Logout!", "info");
            }
        }

        protected void ResetSessionParam()
        {
            Session["USERID"] = "";
            Session["USERNAME"] = "";
            Session["EMPID"] = "";
            Session["EMAILID"] = "";
            Session["COUNTRYDIRECTOR"] = "";
            Session["OFFICE"] = "";
            Session["PROGRAM"] = "";
            Session["OFFICEID"] = "";
            Session["PROGRAMID"] = "";
            Session["TEAM"] = "";
            Session["TEAMID"] = "";
            Session["ISADMIN"] = "";
            Session["ISSHIFTINCHR"] = "";
            Session["DESIGNATION"] = "";
            Session["LOCATION"] = "";
            // Payroll
            Session["FISCALYRID"] = "";
            Session["USERID"] = txtUserID.Text.Trim();
        }

        protected void InsertUserInOutHistory(string status)
        {
           // objLogin.InsertUserInOutHistory(Session["LOGINID"].ToString(), Session["USERID"].ToString().Trim(), Common.ReturnDateTimeInString(DateTime.Now.ToString(), true, Constant.strDateFormat),
             //                     Common.ReturnDateTimeInString(DateTime.Now.ToString(), true, Constant.strDateFormat), status, "N");
        }
        protected void FillOptionValue()
        {
            DataTable dtOpt = new DataTable();
            dtOpt = objLogin.SelectOptionBag("");
            if (dtOpt.Rows.Count > 0)
            {
                foreach (DataRow Row in dtOpt.Rows)
                {
                    if (Row["OptId"].ToString() == "OC01".ToString())
                        Session["OptRetAge"] = Row["OptValue"].ToString();
                    else if (Row["OptId"].ToString() == "OC02")
                        Session["OptBasicPercent"] = Convert.ToInt16(Row["OptValue"]);
                }
            }
        }
        public void ShowClientMessage(Page page, string sMsg, string msgType)
        {        
            ScriptManager.RegisterClientScriptBlock(page, typeof(string), Guid.NewGuid().ToString(), " $.notify('" + sMsg + "','" + msgType + "');", true);
        }
        private void CheckAwayDeskLog()
        {
            DataTable dtLog = objAM.getDeskAwayLog(Session["EMPID"].ToString().Trim(),"","");
            if (dtLog.Rows.Count > 0)
            {        
                string cmdType;
                dsEmployee objDS = new dsEmployee();

                DataTable dtMst = objDS.Tables["EmpAwayDeskLog"];
                DataRow nRow = dtMst.NewRow();

                nRow["SLNo"] = Int64.Parse(dtLog.Rows[0]["SLNO"].ToString().Trim());
                nRow["EMPID"] = Session["EMPID"].ToString().Trim();
                nRow["LogDate"] = DateTime.Now.ToShortDateString();
                nRow["Reason"] = dtLog.Rows[0]["Reason"].ToString().Trim();

                nRow["OutTime"] = dtLog.Rows[0]["OutTime"].ToString().Trim();
                cmdType = "U";
                nRow["InTime"] = DateTime.Now.ToShortTimeString();

                dtMst.Rows.Add(nRow);
                dtMst.AcceptChanges();
                try
                {
                    objEmpInfoMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}