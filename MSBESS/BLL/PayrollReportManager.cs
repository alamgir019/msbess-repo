using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebAdmin.DAL;
namespace WebAdmin.BLL
{
    public class PayrollReportManager
    {
        DataAccess objDAL = new DataAccess();
        public DataTable GetPayslipMonthlyGrossAndBenefits(string strEmpId, string strMonth, string strYear, string strSalType)
        {
            if (objDAL.ds.Tables["GetPayslipMonthlyGrossAndBenefits"] != null)
            {
                objDAL.ds.Tables["GetPayslipMonthlyGrossAndBenefits"].Rows.Clear();
                objDAL.ds.Tables["GetPayslipMonthlyGrossAndBenefits"].Dispose();
            }

            SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PayslipMonthlyGrossAndBenefits");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = strEmpId;

            SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
            p_SALARYTYPE.Direction = ParameterDirection.Input;
            p_SALARYTYPE.Value = strSalType;

            objDAL.CreateDSFromProc(cmd, "GetPayslipMonthlyGrossAndBenefits");
            return objDAL.ds.Tables["GetPayslipMonthlyGrossAndBenefits"];
        }
        public string GetPayrollRemarksForPayslip(string strEmpId, string strMonth, string strYear, string strSalType)
        {

            string strSQL = "SELECT REMARKS FROM PAYSLIPMST WHERE EMPID=@EMPID AND VMONTH=@VMONTH AND VYEAR=@VYEAR "
                            + " AND SALARYTYPE=@SALARYTYPE AND ISPRINTTOPAYSLIP='Y'";
            SqlCommand cmd = new SqlCommand(strSQL);

            SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = strEmpId;

            SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
            p_SALARYTYPE.Direction = ParameterDirection.Input;
            p_SALARYTYPE.Value = strSalType;

            return objDAL.GetScalarVal(cmd);

        }
    }
}