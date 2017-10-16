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
    }
}