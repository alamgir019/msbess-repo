using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAdmin.DAL;

namespace WebAdmin.BLL
{
    public class MasterTablesManager
    {
        DataAccess objDAL = new DataAccess();
        public DataTable SelectRelationList(Int32 RelationId)
        {
            SqlCommand command = new SqlCommand("proc_Select_RelationList");

            SqlParameter p_DesgID = command.Parameters.Add("RelationId", SqlDbType.BigInt);
            p_DesgID.Direction = ParameterDirection.Input;
            p_DesgID.Value = RelationId;

            objDAL.CreateDSFromProc(command, "RelationList");
            return objDAL.ds.Tables["RelationList"];
        }
        
        public DataTable SelectProjectList(int Id)
        {
            SqlCommand command = new SqlCommand("proc_Select_ProjectList");

            SqlParameter p_Id = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
            p_Id.Direction = ParameterDirection.Input;
            p_Id.Value = Id;

            objDAL.CreateDSFromProc(command, "tblProjectList");
            return objDAL.ds.Tables["tblProjectList"];
        }
    }
}