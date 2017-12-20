using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebAdmin.DAL;
namespace WebAdmin.BLL
{
    public class ReportManager
    {
        DataAccess objDAL = new DataAccess();

        #region Attendance

        public DataTable Get_Attandance(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
            string DivisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_AttendanceReport");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateTimeInString(fromDate, false, Constant.strDateFormat);

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateTimeInString(toDate, false, Constant.strDateFormat);

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = DivisionId;

            SqlParameter p_SbuId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SbuId.Direction = ParameterDirection.Input;
            p_SbuId.Value = SBUId;

            SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
            p_DeptID.Direction = ParameterDirection.Input;
            p_DeptID.Value = Convert.ToInt32(DeptId);

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = AttnPolicyId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDAL.CreateDSFromProc(command, "tblAttandance");
            return objDAL.ds.Tables["tblAttandance"];
        }
        public DataTable GetMonthlyAtendanceData(string strEmpId, string strMonth, string strYear)
        {
            SqlCommand command = new SqlCommand("proc_Get_Monthly_Attendance");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = strEmpId;

            SqlParameter p_Month = command.Parameters.Add("Month", SqlDbType.VarChar);
            p_Month.Direction = ParameterDirection.Input;
            p_Month.Value = strMonth;

            SqlParameter p_Year = command.Parameters.Add("Year", SqlDbType.VarChar);
            p_Year.Direction = ParameterDirection.Input;
            p_Year.Value = strYear;            

            objDAL.CreateDSFromProc(command, "AttnAdjust");
            return objDAL.ds.Tables["AttnAdjust"];
        }
        public DataTable Get_MonthlyAttnd(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
         string divisionId, string SBUId, string DeptId, string empId, string shift, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_AttndEmpWise");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateTimeInString(fromDate, false, Constant.strDateFormat);

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateTimeInString(toDate, false, Constant.strDateFormat);

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_SBUID = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUID.Direction = ParameterDirection.Input;
            p_SBUID.Value = SBUId;

            SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
            p_DeptID.Direction = ParameterDirection.Input;
            p_DeptID.Value = Convert.ToInt32(DeptId);

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = shift;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDAL.CreateDSFromProc(command, "tblMonthlyAttnd");
            return objDAL.ds.Tables["tblMonthlyAttnd"];
        }
        #endregion

        #region Payroll

        public DataTable Get_PayslipMonthlyAll(string FisYear, string VMonth, string VYear, string EmpID, string sDesig, string SalSubLocId)
        {
            SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PayslipMonthlyAll");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
            p_Year.Direction = ParameterDirection.Input;
            p_Year.Value = Convert.ToInt32(FisYear);

            SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
            p_VMonth.Direction = ParameterDirection.Input;
            p_VMonth.Value = Convert.ToInt32(VMonth);

            SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
            p_VYear.Direction = ParameterDirection.Input;
            p_VYear.Value = Convert.ToInt32(VYear);

            SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = EmpID;

            SqlParameter p_SalSourceID = cmd.Parameters.Add("DesigId", SqlDbType.BigInt);
            p_SalSourceID.Direction = ParameterDirection.Input;
            p_SalSourceID.Value = Convert.ToInt32(sDesig);


            SqlParameter p_SalDiv = cmd.Parameters.Add("ClinicId", SqlDbType.Char);
            p_SalDiv.Direction = ParameterDirection.Input;
            p_SalDiv.Value = SalSubLocId;


            objDAL.CreateDSFromProc(cmd, "dtSalarySSourandEmpWise");
            return objDAL.ds.Tables["dtSalarySSourandEmpWise"];
        }

        // Get_AnnualReport
        public DataTable Get_AnnualReport(string FisYear, string SalDiv, string EmpID, string sHeadtype, string sEmpTypeID)
        {
            SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_AnnualReport");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
            p_Year.Direction = ParameterDirection.Input;
            p_Year.Value = Convert.ToInt32(FisYear);

            SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
            p_SalDiv.Direction = ParameterDirection.Input;
            p_SalDiv.Value = SalDiv;

            SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = EmpID;

            SqlParameter p_Headtype = cmd.Parameters.Add("Headtype", SqlDbType.Char);
            p_Headtype.Direction = ParameterDirection.Input;
            p_Headtype.Value = sHeadtype;

            SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EType.Direction = ParameterDirection.Input;
            p_EType.Value = Convert.ToInt32(sEmpTypeID);

            objDAL.CreateDSFromProc(cmd, "dtAnnualReport");
            return objDAL.ds.Tables["dtAnnualReport"];
        }
        #endregion
    }
}