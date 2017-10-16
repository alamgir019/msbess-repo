using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebAdmin.DAL;

namespace WebAdmin.BLL
{
    public class LoginManager
    {
        DataAccess objDAL = new DataAccess();
        public DataTable SelectUserInfo(string UserId, string AccountDisabled)
        {
            SqlCommand command = new SqlCommand("proc_Get_User_Info_For_Login");

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            p_UserId.Value = UserId;

            SqlParameter p_AccountDisabled = command.Parameters.Add("AccountDisabled", SqlDbType.Char);
            p_AccountDisabled.Direction = ParameterDirection.Input;
            p_AccountDisabled.Value = AccountDisabled;

            objDAL.CreateDSFromProc(command, "userInfo");
            return objDAL.ds.Tables["userInfo"];
        }

        public DataTable SelectpaySlipOption(string StrOptId)
        {
            SqlCommand command = new SqlCommand("proc_Payroll_Select_PaySlipOption");

            SqlParameter p_OptID = command.Parameters.Add("OptID", SqlDbType.Char);
            p_OptID.Direction = ParameterDirection.Input;
            p_OptID.Value = StrOptId;

            objDAL.CreateDSFromProc(command, "PaySlipOption");
            return objDAL.ds.Tables["PaySlipOption"];
        }

        //Insert into user in out date time log history table
        public void InsertUserInOutHistory(string strLogInId, string strUserId, string strLogInDate, string strLogOutDate, string strStatus, string strIsUpdate)
        {
            SqlCommand command = new SqlCommand("proc_INSERT_UserInOutHistory");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter p_LOGINID = command.Parameters.Add("LogInId", SqlDbType.BigInt);
            p_LOGINID.Direction = ParameterDirection.Input;
            p_LOGINID.Value = strLogInId;

            SqlParameter p_USERID = command.Parameters.Add("UserId", SqlDbType.Char);
            p_USERID.Direction = ParameterDirection.Input;
            p_USERID.Value = strUserId;

            SqlParameter p_InOutDate = command.Parameters.Add("LogInDate", SqlDbType.Char);
            p_InOutDate.Direction = ParameterDirection.Input;
            p_InOutDate.Value = strLogInDate;

            SqlParameter p_LogOutDate = command.Parameters.Add("LogOutDate", SqlDbType.Char);
            p_LogOutDate.Direction = ParameterDirection.Input;
            p_LogOutDate.Value = strLogOutDate;

            SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
            p_Status.Direction = ParameterDirection.Input;
            p_Status.Value = strStatus;

            SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = strIsUpdate;

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

        #region Select Queries From Tables By store procedure
        //Select Option Bag

        public DataTable SelectOptionBag(string OptId)
        {
            SqlCommand command = new SqlCommand("proc_Select_OptionBag");

            SqlParameter p_OptId = command.Parameters.Add("OptID", SqlDbType.Char);
            p_OptId.Direction = ParameterDirection.Input;
            p_OptId.Value = OptId;

            objDAL.CreateDSFromProc(command, "OptionBag");
            return objDAL.ds.Tables["OptionBag"];
        }

        #endregion
    }
}