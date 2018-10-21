using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAdmin.DAL;

namespace WebAdmin.BLL
{

    public class EmpInfoManager
    {
        DataAccess objDAL = new DataAccess();

        #region Common
        public void SaveData(DataTable dtData, string CmdType)
        {
            this.objDAL.SaveDataTable(dtData, CmdType);
        }

        public void UpdatePhoto(Byte[] photo,string empid)
        {
            string strSQL = "UPDATE EmpInfo SET EmpImage=@EmpImage WHERE EmpID=@EmpID ";
            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_EmpImage = command.Parameters.Add("EmpImage", SqlDbType.Image);
            p_EmpImage.Direction = ParameterDirection.Input;
            p_EmpImage.Value = photo;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empid;
            try
            {
                objDAL.ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                command = null;
            }
        }
        public DataTable SelectEmpInfo(string EmpId)
        {
            if (objDAL.ds.Tables["SelectEmpInfo"] != null)
            {
                objDAL.ds.Tables["SelectEmpInfo"].Rows.Clear();
                objDAL.ds.Tables["SelectEmpInfo"].Dispose();
            }

            string strSQL = "SELECT * FROM vwEmpInfoMst WHERE 1=1 ";
            string strCond = "";
            if (EmpId != "")
                strCond = strCond + " AND EmpId=@EmpId ";
           

            string strOrd = " ORDER BY FullName ";

            strSQL = strSQL + strCond + strOrd;

            SqlCommand command = new SqlCommand(strSQL);
            if (EmpId != "")
            {
                SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = EmpId;
            }
            

            return objDAL.CreateDT(command, "SelectEmpInfo");
        }

        public DataTable SelectEmpPresentStatus(string EmpId)
        {
            if (objDAL.ds.Tables["EmpPresentStatus"] != null)
            {
                objDAL.ds.Tables["EmpPresentStatus"].Rows.Clear();
                objDAL.ds.Tables["EmpPresentStatus"].Dispose();
            }

            string strSQL = "SELECT * FROM EmpPresentStatus WHERE 1=1 and status=0 and StatusDate=@StatusDate ";
            string strCond = "";
            if (EmpId != "")
                strCond = strCond + " AND EmpId=@EmpId ";



            strSQL = strSQL + strCond;

            SqlCommand command = new SqlCommand(strSQL);
            if (EmpId != "")
            {
                SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = EmpId;
            }
            SqlParameter p_StatusDate = command.Parameters.Add("StatusDate", SqlDbType.DateTime);
            p_StatusDate.Direction = ParameterDirection.Input;
            p_StatusDate.IsNullable = true;
            p_StatusDate.Value = DateTime.Now.ToShortDateString();

            return objDAL.CreateDT(command, "EmpPresentStatus");
        }
        public DataTable GetSuperVisiorWiseEmp(string strEmpID, string strDivID,DataTable dtEmpList)
        {
            int i = 0;
            string sql = "";
            DataRow[] mRows;
            DataRow[] cRows;
           

            sql = "SELECT EMPID,FULLNAME,SUPERVISORID,OFFICEEMAIL,ISCOUNTRYDIRECTOR  FROM EMPINFO WHERE EMPSTATUS='A' ORDER BY EMPID";
            DataTable dtEmp = objDAL.CreateDT(sql, "SPVWiseEmp");

            DataRow[] spvRow = Common.FindInDataTable(dtEmp, "EMPID='" + strEmpID.Trim() + "'");
            if (spvRow.Count() == 0)
                return null;
            dtEmpList.ImportRow(spvRow[0]);

            mRows = Common.FindInDataTable(dtEmp, "SUPERVISORID='" + strEmpID.Trim() + "'");
            foreach (DataRow row in mRows)
            {
                if (row["ISCOUNTRYDIRECTOR"].ToString().Trim() == "Y")
                {
                    dtEmpList.ImportRow(row);
                }
                else
                {
                    dtEmpList.ImportRow(row);
                    GetChildEmployee(dtEmp, row, dtEmpList);
                }
                i++;
            }


            dtEmpList.AcceptChanges();
            dtEmp = null;
            DataView dv = dtEmpList.DefaultView;
            dv.Sort = "EmpID";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
        }
        public void GetChildEmployee(DataTable dtEMp, DataRow row,DataTable dtEmpList)
        {
            DataRow[] cRows;
            cRows = null;
            cRows = Common.FindInDataTable(dtEMp, "SUPERVISORID='" + row["EMPID"].ToString().Trim() + "'");
            foreach (DataRow rowc in cRows)
            {
                if (row["ISCOUNTRYDIRECTOR"].ToString().Trim() != "Y")
                {
                    dtEmpList.ImportRow(rowc);
                    GetChildEmployee(dtEMp, rowc, dtEmpList);
                }
            }
            return;
        }
        public DataTable SelectFiscalYear(Int32 FiscalYrId, string strType)
        {
            string sql = "";

            if (strType == "F")
                sql = "SELECT * FROM FiscalYearList WHERE IsCurrFiscalYr='Y' AND IsFYTax='N' AND  IsFYPF='N' AND IsFYMed='N' ";
            else if (strType == "T")
                sql = "SELECT * FROM FiscalYearList WHERE IsFYTax='Y' ORDER BY FiscalYrId DESC";
            else if (strType == "P")
                sql = "SELECT * FROM FiscalYearList WHERE IsFYPF='Y' ORDER BY FiscalYrId DESC";
            else if (strType == "M")
                sql = "SELECT * FROM FiscalYearList WHERE IsFYMed='Y' ORDER BY FiscalYrId DESC";
            else if (strType == "FA")
                sql = "SELECT * FROM FiscalYearList WHERE IsFYTax='N' AND  IsFYPF='N' AND IsFYMed='N' ORDER BY IsCurrFiscalYr DESC,FiscalYrId DESC";
            else if (strType == "LPF")
                sql = "SELECT top 1 FiscalYrId,FiscalYrTitle FROM FiscalYearList WHERE IsFYPF='Y' ORDER BY FiscalYrId DESC";

            DataTable dtFiscalyear = objDAL.CreateDT(sql, "dtFiscalYearList");
            
            return dtFiscalyear;
        }



        #endregion

        public DataTable SelectEmpInfoHRAction(string EmpID)
        {
            SqlCommand command = new SqlCommand("proc_Select_EmpInfo_HRAction");
            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = EmpID;
            objDAL.CreateDSFromProc(command, "tblEmpInfoHrAction");
            return objDAL.ds.Tables["tblEmpInfoHrAction"];
        }
        #region Nominee Info
        public DataTable SelectNominee(string EmpId, string NomineeType)
        {
            SqlCommand command = new SqlCommand("proc_Select_Nominee");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_NomineeType = command.Parameters.Add("NomineeType", SqlDbType.Char);
            p_NomineeType.Direction = ParameterDirection.Input;
            p_NomineeType.Value = NomineeType;

            objDAL.CreateDSFromProc(command, "tblNominee");
            return objDAL.ds.Tables["tblNominee"];
        }
        #endregion
        #region Family Info
        public DataTable SelectFamilyInfo(string EmpId, int FmID)
        {
            if(objDAL.ds.Tables["SelectFamilyInfo"] !=null)
            {
                objDAL.ds.Tables["SelectFamilyInfo"].Rows.Clear();
                objDAL.ds.Tables["SelectFamilyInfo"].Dispose();
            }

            string strSQL = "SELECT ROW_NUMBER() OVER (ORDER BY FmName) AS SL,* FROM vwEmpFamilyInfo WHERE IsDeleted='N' ";
            string strCond = "";
            if (EmpId != "")
                strCond = strCond + " AND EmpId=@EmpId ";
            if (FmID != 0)
                strCond = strCond + " AND FmID=@FmID ";

            string strOrd = " ORDER BY FmName";

            strSQL = strSQL + strCond + strOrd;

            SqlCommand command = new SqlCommand(strSQL);
            if (EmpId != "")
            {
                SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = EmpId;
            }
            if (FmID != 0)
            {
                SqlParameter p_FmID = command.Parameters.Add("FmID", SqlDbType.Int);
                p_FmID.Direction = ParameterDirection.Input;
                p_FmID.Value = FmID;
            }

            return objDAL.CreateDT(command, "SelectFamilyInfo");
        }
        #endregion

        #region Employee Info
        public DataTable SelectHomeDistrict(Int32 DistrictID)
        {
            SqlCommand command = new SqlCommand("proc_Select_HomeDistrict");

            SqlParameter p_DistrictID = command.Parameters.Add("DistrictID", SqlDbType.Int);
            p_DistrictID.Direction = ParameterDirection.Input;
            p_DistrictID.Value = DistrictID;

            objDAL.CreateDSFromProc(command, "District");
            return objDAL.ds.Tables["District"];
        }
        public DataTable SelectCountry(Int32 CountryID)
        {
            SqlCommand command = new SqlCommand("proc_Select_Country");
            SqlParameter p_CountryID = command.Parameters.Add("CountryID", SqlDbType.Int);
            p_CountryID.Direction = ParameterDirection.Input;
            p_CountryID.Value = CountryID;
            objDAL.CreateDSFromProc(command, "Country");
            return objDAL.ds.Tables["Country"];
        }
        public DataTable SelectBloodGroupList(Int32 BloodGroupId)
        {
            SqlCommand command = new SqlCommand("proc_Select_BloodGroupList");

            SqlParameter p_DesgID = command.Parameters.Add("BloodGroupId", SqlDbType.Int);
            p_DesgID.Direction = ParameterDirection.Input;
            p_DesgID.Value = BloodGroupId;

            objDAL.CreateDSFromProc(command, "BloodGroupList");
            return objDAL.ds.Tables["BloodGroupList"];
        }
        public DataTable SelectReligionList(Int32 ReligionId)
        {
            SqlCommand command = new SqlCommand("proc_Select_ReligionList");

            SqlParameter p_DesgID = command.Parameters.Add("ReligionId", SqlDbType.BigInt);
            p_DesgID.Direction = ParameterDirection.Input;
            p_DesgID.Value = ReligionId;

            objDAL.CreateDSFromProc(command, "ReligionList");
            return objDAL.ds.Tables["ReligionList"];
        }
        public DataTable SelectEmpTypeList(Int32 EmpTypeID)
        {
            SqlCommand command = new SqlCommand("proc_Select_EmpType");

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = EmpTypeID;

            objDAL.CreateDSFromProc(command, "EmpTypeList");
            return objDAL.ds.Tables["EmpTypeList"];
        }
        public DataTable SelectDistrict(Int32 DistrictID)
        {
            if (objDAL.ds.Tables["District"] != null)
            {
                objDAL.ds.Tables["District"].Rows.Clear();
                objDAL.ds.Tables["District"].Dispose();
            }

            SqlCommand command = new SqlCommand("proc_Select_District");

            SqlParameter p_DistrictID = command.Parameters.Add("DistrictID", SqlDbType.BigInt);
            p_DistrictID.Direction = ParameterDirection.Input;
            p_DistrictID.Value = DistrictID;

            objDAL.CreateDSFromProc(command, "District");
            return objDAL.ds.Tables["District"];
        }
        public DataTable GetEmployeeInfo(string empId, string postingDistId, string bloodGroupId, string empStatus, string empTypeId,string fromDate,string toDate)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmployeeInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_PostingDistId = command.Parameters.Add("PostingDistId", SqlDbType.Int);
            p_PostingDistId.Direction = ParameterDirection.Input;
            p_PostingDistId.Value = Convert.ToInt32(postingDistId);

            SqlParameter p_BloodGroupId = command.Parameters.Add("BloodGroupId", SqlDbType.Int);
            p_BloodGroupId.Direction = ParameterDirection.Input;
            p_BloodGroupId.Value = Convert.ToInt32(bloodGroupId);

            SqlParameter p_IsActive = command.Parameters.Add("EmpStatus", SqlDbType.Char);
            p_IsActive.Direction = ParameterDirection.Input;
            p_IsActive.Value = empStatus;

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.Int);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat(toDate, true);
            else
                p_ToDate.Value = "";

            if (objDAL.ds.Tables["tblEmployeeInfo"] != null)
            {
                objDAL.ds.Tables["tblEmployeeInfo"].Rows.Clear();
            }
            objDAL.CreateDSFromProc(command, "tblEmployeeInfo");
            return objDAL.ds.Tables["tblEmployeeInfo"];
        }

        public DataTable SelectEmpInfoSbuWise(string EmpID, string sbuID)
        {
            SqlCommand command = new SqlCommand("proc_Select_EmpInfo_tab1sbuwise_ForAction");
            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = EmpID;

            SqlParameter p_SbuId = command.Parameters.Add("SbuId", SqlDbType.BigInt);
            p_SbuId.Direction = ParameterDirection.Input;
            p_SbuId.Value = sbuID;
            if (objDAL.ds.Tables["tblEmployeeInfo"]!=null)
            {
                objDAL.ds.Tables["tblEmployeeInfo"].Clear();
            }
            objDAL.CreateDSFromProc(command, "tblEmployeeInfo");
            return objDAL.ds.Tables["tblEmployeeInfo"];
        }
        #endregion
        #region Payroll
        public DataTable SelectEmpPayslipPersonalInfo(string strEmpID)
        {
            string strSQL = "SELECT E.EmpId,E.FullName AS FULLNAME,J.JobTitleName,e.BankAccNo,Desi.DesigName,pd.DivisionName," +
                        "PDis.ProjectName,DeptL.DeptName,sl.SalLocName,g.GradeName" +
                        " FROM EmpInfo E left join JobTitle J on E.JobTitleId=J.JobTitleId" +
                        " left join Designation Desi on E.DesigId=Desi.DesigId" +
                        " left join DivisionList pd on E.DivisionId=pd.DivisionId" +
                        " left join ProjectList PDis on E.ProjectID=PDis.ProjectID" +
                        " left join DepartmentList DeptL on E.DeptId=DeptL.DeptId " +
                        " left join SalaryLocation  sl on E.SalLocId=sl.SalLocId" +
                        " left join GradeList g on E.GradeId=g.GradeId" +
                        " WHERE E.DesigId=Desi.DesigId AND EMPID=@EMPID";

            SqlCommand command = new SqlCommand(strSQL);
            command.CommandType = CommandType.Text;

            SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = strEmpID;

            objDAL.CreateDT(command, "SelectEmpPayslipPersonalInfo");
            return objDAL.ds.Tables["SelectEmpPayslipPersonalInfo"];
        }
        #endregion
    }
}