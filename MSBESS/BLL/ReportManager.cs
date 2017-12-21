using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebAdmin.DAL;
using WebAdmin.App_Data;

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

        #region
        public DataTable Get_ITAssessment(string FisYear, string VMonth, string EmpID)
        {
            SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_ITAssessment");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_Year = cmd.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
            p_Year.Direction = ParameterDirection.Input;
            p_Year.Value = Convert.ToInt32(FisYear);

            SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.Char);
            p_VMonth.Direction = ParameterDirection.Input;
            p_VMonth.Value = VMonth;

            SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = EmpID;

            objDAL.CreateDSFromProc(cmd, "dtITAssessment");
            DataTable dtITAssessment = new DataTable();
            dtITAssessment = objDAL.ds.Tables["dtITAssessment"];

            dsReports objdsPay = new dsReports();
            string sEmpId = "";
            DataRow FinalRow;
            if (dtITAssessment.Rows.Count > 0)
            {
                foreach (DataRow dRow in dtITAssessment.Rows)
                {
                    FinalRow = objdsPay.dtITComputation.NewRow();

                    if (dRow["EmpId"].ToString().Trim() != sEmpId)
                    {
                        FinalRow["AssYear"] = dRow["AssYear"].ToString();
                        FinalRow["EmpId"] = dRow["EmpId"].ToString();
                        sEmpId = dRow["EmpId"].ToString().Trim();
                        FinalRow["FullName"] = dRow["FullName"].ToString();
                        FinalRow["SalLocName"] = dRow["SalLocName"].ToString();
                        FinalRow["BasicSalary"] = dRow["BasicSalary"].ToString();
                        FinalRow["YBasicSalary"] = Common.ReturnZeroForNull(dRow["YBasicSalary"].ToString());
                        FinalRow["YHouseRent"] = Common.ReturnZeroForNull(dRow["YHouseRent"].ToString());
                        FinalRow["YMedicalAllowance"] = Common.ReturnZeroForNull(dRow["YMedicalAllowance"].ToString());
                        FinalRow["YTransportAllowance"] = Common.ReturnZeroForNull(dRow["YTransportAllowance"].ToString());
                        FinalRow["YFestivalBonus"] = Common.ReturnZeroForNull(dRow["YFestivalBonus"].ToString());
                        FinalRow["YPFDeduction"] = Common.ReturnZeroForNull(dRow["YPFDeduction"].ToString());
                        FinalRow["YOverTime"] = Common.ReturnZeroForNull(dRow["YOverTime"].ToString());
                        FinalRow["NetTax"] = Common.ReturnZeroForNull(dRow["NetTax"].ToString());
                        FinalRow["TTI_2"] = Common.ReturnZeroForNull(dRow["TTI_2"].ToString());
                        #region Tax Liability
                        decimal dclRemainTaxLiable = 0;
                        if (Convert.ToDecimal(Common.ReturnZeroForNull(dRow["TTI_2"].ToString())) > 250000)
                        {
                            FinalRow["TaxLiableP0"] = "250000";
                            dclRemainTaxLiable = Convert.ToDecimal(Common.ReturnZeroForNull(dRow["TTI_2"].ToString())) - 250000;
                        }
                        else
                        {
                            FinalRow["TaxLiableP0"] = dclRemainTaxLiable;
                            dclRemainTaxLiable = 0;
                        }

                        if (dclRemainTaxLiable > 400000)
                        {
                            FinalRow["TaxLiableP10"] = "400000";
                            dclRemainTaxLiable = dclRemainTaxLiable - 400000;
                        }
                        else
                        {
                            FinalRow["TaxLiableP10"] = dclRemainTaxLiable;
                            dclRemainTaxLiable = 0;
                        }

                        if (dclRemainTaxLiable > 500000)
                        {
                            FinalRow["TaxLiableP15"] = "500000";
                            dclRemainTaxLiable = dclRemainTaxLiable - 500000;
                        }
                        else
                        {
                            FinalRow["TaxLiableP15"] = dclRemainTaxLiable;
                            dclRemainTaxLiable = 0;
                        }

                        if (dclRemainTaxLiable > 600000)
                        {
                            FinalRow["TaxLiableP20"] = "600000";
                            dclRemainTaxLiable = dclRemainTaxLiable - 600000;
                        }
                        else
                        {
                            FinalRow["TaxLiableP20"] = dclRemainTaxLiable;
                            dclRemainTaxLiable = 0;
                        }

                        if (dclRemainTaxLiable > 3000000)
                        {
                            FinalRow["TaxLiableP25"] = "3000000";
                            dclRemainTaxLiable = dclRemainTaxLiable - 3000000;
                        }
                        else
                        {
                            FinalRow["TaxLiableP25"] = dclRemainTaxLiable;
                            dclRemainTaxLiable = 0;
                        }

                        if (dclRemainTaxLiable > 3000000)
                        {
                            FinalRow["TaxLiableP30"] = "3000000";
                            dclRemainTaxLiable = dclRemainTaxLiable - 3000000;
                        }
                        else
                        {
                            FinalRow["TaxLiableP30"] = dclRemainTaxLiable;
                            dclRemainTaxLiable = 0;
                        }
                        #endregion
                        FinalRow["P10"] = Common.ReturnZeroForNull(dRow["P10"].ToString());
                        FinalRow["P15"] = Common.ReturnZeroForNull(dRow["P15"].ToString());
                        FinalRow["P20"] = Common.ReturnZeroForNull(dRow["P20"].ToString());
                        FinalRow["P25"] = Common.ReturnZeroForNull(dRow["P25"].ToString());
                        FinalRow["P30"] = Common.ReturnZeroForNull(dRow["P30"].ToString());
                        FinalRow["G_Tax"] = Common.ReturnZeroForNull(dRow["G_Tax"].ToString());
                        FinalRow["Rebate"] = Common.ReturnZeroForNull(dRow["Rebate"].ToString());// ((((Convert.ToDecimal(FinalRow["TTI_2"]) * 30) / 100) * 15) / 100);
                        FinalRow["MonthlyTax"] = Common.ReturnZeroForNull(dRow["MonthlyTax"].ToString());
                        FinalRow["ITDeposited"] = Common.ReturnZeroForNull(dRow["ITDeposited"].ToString());
                        FinalRow["VMonthNo"] = Common.ReturnZeroForNull(dRow["VMonthNo"].ToString());
                        objdsPay.dtITComputation.Rows.Add(FinalRow);
                        objdsPay.dtITComputation.AcceptChanges();
                    }
                }
            }
            return objdsPay.dtITComputation;
        }
        #endregion
    }
}