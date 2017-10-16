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

    }
}