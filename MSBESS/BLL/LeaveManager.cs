using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebAdmin.DAL;
using System.Data.SqlClient;

namespace WebAdmin.BLL
{
    public class LeaveManager
    {
        DataAccess objDAL = new DataAccess();

        public void SaveData(List<DataRow> lstRow, string CmdType)
        {
            this.objDAL.SaveDataTable(lstRow, CmdType);
        }

        public DataTable SelectEmpLeaveProfileEXCPL(string EmpId, string LTypeID, string Sex)
        {
            SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfileEXCPL");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
            p_LTypeID.Direction = ParameterDirection.Input;
            p_LTypeID.Value = Convert.ToInt32(LTypeID);

            SqlParameter p_Sex = command.Parameters.Add("Gender", SqlDbType.Char);
            p_Sex.Direction = ParameterDirection.Input;
            p_Sex.Value = Sex;

            objDAL.CreateDSFromProc(command, "EmpLeaveProfileEXCPL");
            return objDAL.ds.Tables["EmpLeaveProfileEXCPL"];
        }

        public DataTable SelectEmpLeaveProfileEXCPL_History(string EmpId, string LTypeID, string LeaveYear)
        {
            SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfileEXCPL_History");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
            p_LTypeID.Direction = ParameterDirection.Input;
            p_LTypeID.Value = Convert.ToInt32(LTypeID);

            SqlParameter p_LeaveYear = command.Parameters.Add("LeaveYear", SqlDbType.DateTime);
            p_LeaveYear.Direction = ParameterDirection.Input;
            p_LeaveYear.Value = LeaveYear;

            objDAL.CreateDSFromProc(command, "EmpLeaveProfileEXCPLHistory");
            return objDAL.ds.Tables["EmpLeaveProfileEXCPLHistory"];
        }

        public DataTable SelectEmpLeaveProfile(string EmpId, string LTypeID)
        {
            SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfile");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
            p_LTypeID.Direction = ParameterDirection.Input;
            p_LTypeID.Value = Convert.ToInt32(LTypeID);

            objDAL.CreateDSFromProc(command, "EmpLeaveProfile");
            return objDAL.ds.Tables["EmpLeaveProfile"];
        }


        public DataTable SelectEmpWiseLeaveType(Int32 LPakId, string EmpId, string Sex)
        {
            SqlCommand command = new SqlCommand("proc_Select_EmpWiseLeavePak");
            SqlParameter p_LPakId = command.Parameters.Add("LeavePakId", SqlDbType.BigInt);
            p_LPakId.Direction = ParameterDirection.Input;
            p_LPakId.Value = LPakId;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_Sex = command.Parameters.Add("Gender", SqlDbType.Char);
            p_Sex.Direction = ParameterDirection.Input;
            p_Sex.Value = Sex;

            objDAL.CreateDSFromProc(command, "LeaveType");
            return objDAL.ds.Tables["LeaveType"];
        }

        public DataTable SelectLeaveType(Int32 LTypeID)
        {
            if (objDAL.ds.Tables["LeaveType"] != null)
            {
                objDAL.ds.Tables["LeaveType"].Rows.Clear();
                objDAL.ds.Tables["LeaveType"].Dispose();
            }
            SqlCommand command = new SqlCommand("proc_Select_LeaveType");
            SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
            p_LTypeID.Direction = ParameterDirection.Input;
            p_LTypeID.Value = LTypeID;
            objDAL.CreateDSFromProc(command, "LeaveType");
            return objDAL.ds.Tables["LeaveType"];
        }

        public DataTable SelectEmpWiseWeekend(string EmpId)
        {
            SqlCommand command = new SqlCommand("proc_Select_Emp_Ws_Weekend");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            objDAL.CreateDSFromProc(command, "EmpInfo");
            return objDAL.ds.Tables["EmpInfo"];
        }
        public DataTable CheckLvDateWithHoliDate(string strLvStartDate, string strLvEndDate, string strYear)
        {
            string strSQL = "SELECT * FROM HolidaysDetls WHERE HoliDate BETWEEN '" + strLvStartDate
                + "' AND '" + strLvEndDate + "' AND HoliDayYear='" + strYear + "'";

            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_LvStartDate = command.Parameters.Add("StartHoliDate", SqlDbType.DateTime);
            p_LvStartDate.Direction = ParameterDirection.Input;
            p_LvStartDate.Value =strLvStartDate;

            SqlParameter p_LvEndDate = command.Parameters.Add("EndHoliDate", SqlDbType.DateTime);
            p_LvEndDate.Direction = ParameterDirection.Input;
            p_LvEndDate.Value = strLvEndDate;

            SqlParameter p_LvYear = command.Parameters.Add("HoliDayYear", SqlDbType.Char);
            p_LvYear.Direction = ParameterDirection.Input;
            p_LvYear.Value = strYear;

            objDAL.CreateDT(command, "HolidayDetls");
            return objDAL.ds.Tables["HolidayDetls"];
        }
        public DataTable PrevDayLeave(string strEmpId, string strLeaveStart)
        {
            string strSQL = "SELECT LTypeId FROM LeaveAppDet WHERE LevDate=@LevDate AND EmpId=@EmpId";

            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = strEmpId;

            SqlParameter p_LvStartDate = command.Parameters.Add("LevDate", SqlDbType.DateTime);
            p_LvStartDate.Direction = ParameterDirection.Input;
            p_LvStartDate.Value = strLeaveStart;

            return objDAL.CreateDT(command, "PrevDayLeave");
        }
        public DataTable CheckLvTakenBarrier()
        {
            string strSQL = "SELECT LTB.*,LT.LTypeTitle FROM LeaveTakenBarrier LTB, LeaveTypeList LT WHERE LTB.PLTypeId=LT.LTypeId";

            SqlCommand command = new SqlCommand(strSQL);

            objDAL.CreateDT(command, "CheckLvTakenBarrier");
            return objDAL.ds.Tables["CheckLvTakenBarrier"];
        }
        public DataTable SelectEmpLeaveDateDetails(Int32 LvAppID, string EmpId, string LevDate)
        {
            SqlCommand command = new SqlCommand("proc_Select_LeaveAppDet");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = LvAppID;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_LevDate = command.Parameters.Add("LevDate", SqlDbType.DateTime);
            p_LevDate.Direction = ParameterDirection.Input;
            p_LevDate.Value =LevDate;

            objDAL.CreateDSFromProc(command, "LeaveAppDet2");
            return objDAL.ds.Tables["LeaveAppDet2"];
        }
        public DataTable CheckLvDateBetweenLeavePeriod(string strLvPakId, string strLvStartDate)
        {
            string strSQL = "SELECT * FROM LeavePeriod WHERE '" + strLvStartDate + "' NOT BETWEEN"
                + " LeaveStartPeriod AND LeaveEndPeriod AND LeavePakId=" + strLvPakId;

            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_LvPakId = command.Parameters.Add("LvPakId", SqlDbType.BigInt);
            p_LvPakId.Direction = ParameterDirection.Input;
            p_LvPakId.Value = strLvPakId;

            SqlParameter p_LvStartDate = command.Parameters.Add("LeaveStartPeriod", SqlDbType.VarChar);
            p_LvStartDate.Direction = ParameterDirection.Input;
            p_LvStartDate.Value = strLvStartDate;

            objDAL.CreateDT(command, "ChkLeavePeriod");
            return objDAL.ds.Tables["ChkLeavePeriod"];
        }

        public DataTable SelectRequestLeaveAppMst(Int32 LvAppID, string EmpId, string AppStatus,
       string LeaveStart, string LeaveEnd, string reportingTo)
        {
            if (objDAL.ds.Tables["LeavAppMstReq"] != null)
            {
                objDAL.ds.Tables["LeavAppMstReq"].Rows.Clear();
                objDAL.ds.Tables["LeavAppMstReq"].Dispose();
            }

            SqlCommand command = new SqlCommand("proc_Select_LeaveAppMst");
            SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = LvAppID;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
            p_AppStatus.Direction = ParameterDirection.Input;
            p_AppStatus.Value = AppStatus;

            SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
            p_LeaveStart.Direction = ParameterDirection.Input;
            p_LeaveStart.Value = LeaveStart;

            SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
            p_LeaveEnd.Direction = ParameterDirection.Input;
            p_LeaveEnd.Value = LeaveEnd;

            SqlParameter p_reportingTo = command.Parameters.Add("SupervisorId", SqlDbType.VarChar);
            p_reportingTo.Direction = ParameterDirection.Input;
            p_reportingTo.Value = reportingTo;

            objDAL.CreateDSFromProc(command, "LeavAppMstReq");
            return objDAL.ds.Tables["LeavAppMstReq"];
        }

        public DataTable SelectRequestLeaveAppMst(Int64 LvAppID, string EmpId, string AppStatus)
        {
            SqlCommand command = new SqlCommand("proc_Select_LeavAppMst");
            SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = LvAppID;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
            p_AppStatus.Direction = ParameterDirection.Input;
            p_AppStatus.Value = AppStatus;

            if (AppStatus == "A")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstApprove");
                return objDAL.ds.Tables["LeavAppMstApprove"];
            }
            else if (AppStatus == "R")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstReq");
                return objDAL.ds.Tables["LeavAppMstReq"];
            }
            else if (AppStatus == "D")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstDeny");
                return objDAL.ds.Tables["LeavAppMstDeny"];
            }
            else if (AppStatus == "RD")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstReqDeny");
                return objDAL.ds.Tables["LeavAppMstReqDeny"];
            }
            else
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstReqDeny");
                return objDAL.ds.Tables["LeavAppMstReqDeny"];
            }
        }

        public DataTable SelectLeaveAppMstRpt(Int32 LvAppID, string EmpId, string AppStatus, string LeaveStart, string LeaveEnd)
        {
            SqlCommand command = new SqlCommand("proc_Select_LeaveAppMstRpt");
            SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = LvAppID;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
            p_AppStatus.Direction = ParameterDirection.Input;
            p_AppStatus.Value = AppStatus;

            SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
            p_LeaveStart.Direction = ParameterDirection.Input;
            p_LeaveStart.Value = LeaveStart;

            SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
            p_LeaveEnd.Direction = ParameterDirection.Input;
            p_LeaveEnd.Value = LeaveEnd;

            if (AppStatus == "A")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstApprove");
                return objDAL.ds.Tables["LeavAppMstApprove"];
            }
            else if (AppStatus == "R")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstReq");
                return objDAL.ds.Tables["LeavAppMstReq"];
            }
            else if (AppStatus == "D")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstDeny");
                return objDAL.ds.Tables["LeavAppMstDeny"];
            }
            else if (AppStatus == "RD")
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstReqDeny");
                return objDAL.ds.Tables["LeavAppMstReqDeny"];
            }
            else
            {
                objDAL.CreateDSFromProc(command, "LeavAppMstReqDeny");
                return objDAL.ds.Tables["LeavAppMstReqDeny"];
            }
        }
        public DataTable SelectResponsePerson(Int32 LvAppID, string EmpId)
        {
            SqlCommand command = new SqlCommand("proc_Select_LeaveResponsivePerson");
            SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = LvAppID;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            objDAL.CreateDSFromProc(command, "LeaveResponsivePerson");
            return objDAL.ds.Tables["LeaveResponsivePerson"];
        }
        public DataTable SelectEmpLeaveDetails(string EmpId, string LeaveStart, string LeaveEnd)
        {
            SqlCommand command = new SqlCommand("proc_Select_Emp_Ws_Leave_Det");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = EmpId;

            SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
            p_LeaveStart.Direction = ParameterDirection.Input;
            p_LeaveStart.Value = LeaveStart;

            SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
            p_LeaveEnd.Direction = ParameterDirection.Input;
            p_LeaveEnd.Value = LeaveEnd;

            objDAL.CreateDSFromProc(command, "LeaveAppMst");
            return objDAL.ds.Tables["LeaveAppMst"];
        }
        public DataTable GetLeaveDates(string strLvAppId)
        {
            string strSQL = "SELECT LevDate FROM LeaveAppDet WHERE LvAppId=" + strLvAppId;

            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_LvAppId = command.Parameters.Add("LvAppId", SqlDbType.BigInt);
            p_LvAppId.Direction = ParameterDirection.Input;
            p_LvAppId.Value = Convert.ToInt32(strLvAppId);

            objDAL.CreateDT(command, "LevAppDetDate");
            return objDAL.ds.Tables["LevAppDetDate"];
        }
        public void UpdateLeaveAppMstForApprove(string strLvAppId, string strEmpId, string IsUpdate, string IsDelete,
       string AppStatus, string LeaveEnjoyed, string LeaveDates, string LeaveAbbrName, string LTypeId,
       string LTReason, string strInsBy, string strInsDate, string strPreYrLv, string strDuration)
        {

            int i = 0;
            char[] splitter = { ',' };

            string[] arinfo2 = new string[10];
            arinfo2 = Common.str_split(LeaveDates, splitter);

            SqlCommand[] cmd;
            cmd = new SqlCommand[3 + (arinfo2.Length)];

            //Update Leave Application Mst For Approve
            cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
            cmd[0].CommandType = CommandType.StoredProcedure;

            SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = Convert.ToInt32(strLvAppId);

            SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
            p_AppStatus.Direction = ParameterDirection.Input;
            p_AppStatus.Value = AppStatus;

            SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.Char);
            p_RecommendBy.Direction = ParameterDirection.Input;
            p_RecommendBy.Value = strInsBy;

            SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.Char);
            p_ApprovedBy.Direction = ParameterDirection.Input;
            p_ApprovedBy.Value = strInsBy;

            SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = "Y";

            //Update Leave Profile
            cmd[1] = UpdateLeaveProfile(strPreYrLv, strEmpId, LTypeId, LeaveEnjoyed, strInsBy, strInsDate);

            ////Insert Or Update Leave Flag & Status in Attandance Table 
            int row = 2;
            int j = row;

            row = j;

            decimal dclTotDuration = Convert.ToDecimal(strDuration);
            decimal dclDuration = 0;
            for (j = row; j < arinfo2.Length + row; j++)
            {
                if (dclTotDuration >= 1)
                    dclDuration = 1;
                else
                    dclDuration = dclTotDuration;

                cmd[j] = UpdateAttendanceForLeave(strEmpId, arinfo2[j - row], LeaveAbbrName, LTReason, dclDuration.ToString(), strInsBy, strInsDate);

                dclTotDuration = dclTotDuration - dclDuration;
            }

            try
            {
                objDAL.MakeTransaction(cmd);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cmd = null;
            }
        }
        public SqlCommand UpdateLeaveProfile(string strPreYrLv, string strEmpId, string strLTypeId, string strLvEnjoyed, string strInsBy, string strInsDate)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Update_EmpLeaveProfile");

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_Ltype = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
            p_Ltype.Direction = ParameterDirection.Input;
            p_Ltype.Value = Convert.ToInt32(strLTypeId);

            SqlParameter p_LeaveEnjoyed1 = cmd.Parameters.Add("LeaveEnjoyed", SqlDbType.Decimal);
            p_LeaveEnjoyed1.Direction = ParameterDirection.Input;
            p_LeaveEnjoyed1.Value = Convert.ToDecimal(strLvEnjoyed);

            SqlParameter p_InsertedBy2 = cmd.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
            p_InsertedBy2.Direction = ParameterDirection.Input;
            p_InsertedBy2.Value = strInsBy;

            SqlParameter p_InsertedDate2 = cmd.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
            p_InsertedDate2.Direction = ParameterDirection.Input;
            p_InsertedDate2.Value = strInsDate;

            return cmd;
        }
        public SqlCommand UpdateAttendanceForLeave(string strEmpId, string strAttndDate, string strLeaveAbbrName,
               string LTReason, string strDuration, string strInsBy, string strInsDate)
        {
            SqlCommand cmd = new SqlCommand("proc_Update_Attandance_LeaveStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_AttndDate = cmd.Parameters.Add("AttndDate", SqlDbType.DateTime);
            p_AttndDate.Direction = ParameterDirection.Input;
            p_AttndDate.Value = strAttndDate;

            SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.Char);
            p_Status.Direction = ParameterDirection.Input;
            if (strLeaveAbbrName != "SL")
                p_Status.Value = "V";
            else
                p_Status.Value = strLeaveAbbrName;

            SqlParameter p_LeaveFlag = cmd.Parameters.Add("LeaveFlag", SqlDbType.Char);
            p_LeaveFlag.Direction = ParameterDirection.Input;
            p_LeaveFlag.Value = strLeaveAbbrName;

            SqlParameter p_Remarks = cmd.Parameters.Add("Remarks", SqlDbType.VarChar);
            p_Remarks.Direction = ParameterDirection.Input;
            p_Remarks.Value = LTReason;

            SqlParameter p_LateTimeAmt = cmd.Parameters.Add("LateTimeAmt", SqlDbType.BigInt);
            p_LateTimeAmt.Direction = ParameterDirection.Input;
            if (strDuration == "1")
                p_LateTimeAmt.Value = 8;
            else
                p_LateTimeAmt.Value = 4;

            SqlParameter p_InsertedBy2 = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy2.Direction = ParameterDirection.Input;
            p_InsertedBy2.Value = strInsBy;

            SqlParameter p_InsertedDate5 = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate5.Direction = ParameterDirection.Input;
            p_InsertedDate5.Value = strInsDate;

            return cmd;
        }
        public void UpdateLeaveAppMstForDeny(string strLvAppId, string strEmpId, string IsUpdate, string IsDelete,
      string AppStatus, string strInsBy, string strInsDate)
        {

            SqlCommand[] cmd;
            cmd = new SqlCommand[1];

            cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
            cmd[0].CommandType = CommandType.StoredProcedure;

            SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = strLvAppId;

            SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
            p_AppStatus.Direction = ParameterDirection.Input;
            p_AppStatus.Value = AppStatus;

            SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.VarChar);
            p_RecommendBy.Direction = ParameterDirection.Input;
            p_RecommendBy.Value = strInsBy;

            SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.VarChar);
            p_ApprovedBy.Direction = ParameterDirection.Input;
            p_ApprovedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = "Y";
            try
            {
                objDAL.MakeTransaction(cmd);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cmd = null;
            }
        }
        public void CancelLeaveApp(string strLvAppId, string strEmpId, string IsUpdate, string IsDelete,
       string AppStatus, string strInsBy, string strInsDate)
        {

            SqlCommand[] cmd;
            cmd = new SqlCommand[1];

            cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
            cmd[0].CommandType = CommandType.StoredProcedure;

            SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = strLvAppId;

            SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
            p_AppStatus.Direction = ParameterDirection.Input;
            p_AppStatus.Value = AppStatus;

            SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.VarChar);
            p_RecommendBy.Direction = ParameterDirection.Input;
            p_RecommendBy.Value = strInsBy;

            SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.VarChar);
            p_ApprovedBy.Direction = ParameterDirection.Input;
            p_ApprovedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = "Y";
            try
            {
                objDAL.MakeTransaction(cmd);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cmd = null;
            }
        }
        public void UpdateLeaveAppMstForRecommendation(string strLvAppId, string strEmpId, string AppStatus, string strApprovedBy,
               string strInsBy, string strInsDate)
        {
            SqlCommand[] cmd;
            cmd = new SqlCommand[1];

            //Update Leave Application Mst For Recommendation
            cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
            cmd[0].CommandType = CommandType.StoredProcedure;

            SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
            p_LvAppID.Direction = ParameterDirection.Input;
            p_LvAppID.Value = Convert.ToInt32(strLvAppId);

            SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.Char);
            p_RecommendBy.Direction = ParameterDirection.Input;
            p_RecommendBy.Value = strInsBy;

            SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.Char);
            p_ApprovedBy.Direction = ParameterDirection.Input;
            p_ApprovedBy.Value = strApprovedBy;

            SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
            p_AppStatus.Direction = ParameterDirection.Input;
            p_AppStatus.Value = AppStatus;

            SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = "Y";

            try
            {
                objDAL.MakeTransaction(cmd);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cmd = null;
            }
        }
        public DataTable SelectEmpInfoForLeave(string EmpID)
        {
            if (objDAL.ds.Tables["EmpInfo"] != null)
            {
                objDAL.ds.Tables["EmpInfo"].Rows.Clear();
                objDAL.ds.Tables["EmpInfo"].Dispose();
            }

            SqlCommand command = new SqlCommand("proc_Select_EmpInfo");
            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = EmpID;

            objDAL.CreateDSFromProc(command, "EmpInfo");
            return objDAL.ds.Tables["EmpInfo"];
        }
    }
}