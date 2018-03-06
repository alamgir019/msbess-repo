using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebAdmin.DAL;
using System.Data.SqlClient;
namespace WebAdmin.BLL
{
         
    public class AttendanceManager
    {
        DataAccess objDAL = new DataAccess();
        public DataTable getDeskAwayLog(string strEmpId,string Intime,string logDate)
        {
            string strQuery= "SELECT * FROM EmpAwayDeskLOG WHERE 1=1";
            if (Intime==string.Empty)
            {
                strQuery = strQuery + " AND  InTime is null";
            }
            if (strEmpId != string.Empty)
            {          
                strQuery = strQuery+" AND  EmpId =@EmpId";
            }
            if (logDate!=string.Empty)
            {
                strQuery = strQuery + " AND  LogDate =@LogDate";
            }
            SqlCommand command = new SqlCommand(strQuery);
            command.CommandType = CommandType.Text;
            if (strEmpId != string.Empty)
            {
                SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = strEmpId;
            }
            if (logDate != string.Empty)
            {
                SqlParameter p_LogDate = command.Parameters.Add("LogDate", SqlDbType.Date);
                p_LogDate.Direction = ParameterDirection.Input;
                p_LogDate.Value = logDate;
            }
                objDAL.CreateDT(command, "EmpAwayLog");
            return objDAL.ds.Tables["EmpAwayLog"];
        }
    }
}