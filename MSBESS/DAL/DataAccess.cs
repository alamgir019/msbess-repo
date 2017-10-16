using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAdmin.DAL
{   
    public class DataAccess
    {
        private string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;// works fine
        private SqlConnection SQLConnection;

        private SqlCommand com = new SqlCommand();
        private SqlDataAdapter da = new SqlDataAdapter();
        public SqlDataReader dr;
        public DataSet ds =new DataSet();
       

        public void CreateConnection()
        {
            SQLConnection = new SqlConnection(strConnection);
            SQLConnection.Open();
        }
        public void CloseConnection()
        {
            SQLConnection.Close();
        }

        /// <summary>
        /// This method creates a Dataset with table
        /// </summary>
        /// <param name="SQLQueryDS">Query string</param>
        /// <param name="TableName">Table name</param>
        public void CreateDS(string SQLQueryDS, string TableName)//, SqlCommand com, SqlDataAdapter da)
        {
            SQLConnection = new SqlConnection(strConnection);
            com.CommandText = SQLQueryDS;
            com.Connection = SQLConnection;
            da.SelectCommand = com;
            da.Fill(ds, TableName);
            CloseConnection();
        }

        /// <summary>
        /// This method creates a Dataset with table from procedure
        /// </summary>
        /// <param name="SQLQueryDS">Query string</param>
        /// <param name="TableName">Table name</param>
        public void CreateDSFromProc(SqlCommand cmd, string TableName)//, SqlCommand com, SqlDataAdapter da)
        {
            try
            {
                SQLConnection = new SqlConnection(strConnection);

                SQLConnection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = SQLConnection;
                cmd.CommandTimeout = 120;
                da.SelectCommand = cmd;

                da.Fill(ds, TableName);
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (SQLConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// This method creates a DATATABLE with table
        /// </summary>
        /// <param name="SQLQueryDT">Query string</param>
        /// <param name="TableName">Table name</param>
        public DataTable CreateDT(string SQLQueryDT, string TableName)//, SqlCommand com, SqlDataAdapter da)
        {
            try
            {
                //SQLConnection.Open(); 
                SQLConnection = new SqlConnection(strConnection);
                SQLConnection.Open();
                com.CommandText = SQLQueryDT;
                com.Connection = SQLConnection;
                da.SelectCommand = com;
                da.Fill(ds, TableName);
                SQLConnection.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (SQLConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
            return ds.Tables[TableName];
        }

        /// <summary>
        /// This method creates a DATATABLE with table
        /// </summary>
        /// <param name="command">SQLCommand</param>
        /// <param name="TableName">Table name</param>
        /// <returns>DataTable</returns>
        public DataTable CreateDT(SqlCommand command, string TableName)//, SqlCommand com, SqlDataAdapter da)
        {
            try
            {
                //SQLConnection.Open(); 
                SQLConnection = new SqlConnection(strConnection);
                SQLConnection.Open();
                command.CommandType = CommandType.Text;
                command.Connection = SQLConnection;
                da.SelectCommand = command;
                da.Fill(ds, TableName);

                SQLConnection.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (SQLConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }

            return ds.Tables[TableName];
        }

        /// <summary>
        /// This method returns SqlDataReader
        /// </summary>
        /// <param name="SQLQueryDR">Query String</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader CreateDR(string SQLQueryDR)
        {

            CreateConnection();
            com.CommandText = SQLQueryDR;
            com.Connection = SQLConnection;
            dr = com.ExecuteReader();
            //CloseConnection();

            return dr;
        }

        /// <summary>
        /// This method returns SqlDataReader
        /// </summary>
        /// <param name="Cmd">SQL Command String</param>
        /// <returns>Singe value in String</returns>
        public string GetScalarVal(SqlCommand Cmd)
        {
            try
            {
                using (SQLConnection = new SqlConnection(strConnection))
                {
                    SQLConnection.Open();
                    Cmd.Connection = SQLConnection;
                    string result = Convert.ToString(Cmd.ExecuteScalar());
                    SQLConnection.Close();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new HttpException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new HttpException("SQL:" + Cmd.CommandText + " Error found" + ex.ToString());
            }
        }
        /// <summary>
        /// This method can execute an Insert, delete or update query
        /// </summary>
        /// <param name="Cmd">The query to execute</param>

        public void ExecuteQuery(SqlCommand Cmd)
        {
            DataTable datatable = new DataTable();
            SqlTransaction transaction;
            SQLConnection = new SqlConnection(strConnection);
            using (SQLConnection)
            {
                SQLConnection.Open();
                transaction = SQLConnection.BeginTransaction();

                try
                {
                    Cmd.Transaction = transaction;
                    Cmd.Connection = SQLConnection;
                    Cmd.ExecuteNonQuery();
                    transaction.Commit();
                    SQLConnection.Close();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new HttpException(ex.ToString());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new HttpException("SQL:" + Cmd.CommandText + " Error found" + ex.ToString());
                }
                finally
                {
                    if (SQLConnection.State == ConnectionState.Open)
                    {
                        CloseConnection();
                    }
                }
            }
        }

        /// <summary>
        /// This method can execute an Insert, delete or update query
        /// </summary>
        /// <param name="Cmd">The query to execute</param>
        /// Return Msg as String

        public string ExecuteQueryRetMsg(SqlCommand Cmd)
        {
            string strMsg = "";
            DataTable datatable = new DataTable();
            SqlTransaction transaction;

            using (SQLConnection)
            {

                SQLConnection.Open();
                transaction = SQLConnection.BeginTransaction();

                try
                {
                    Cmd.Transaction = transaction;
                    Cmd.Connection = SQLConnection;
                    Cmd.ExecuteNonQuery();
                    strMsg = Cmd.Parameters["p_Msg"].Value.ToString();
                    transaction.Commit();
                    SQLConnection.Close();


                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new HttpException(ex.ToString());

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new HttpException("SQL:" + Cmd.CommandText + " Error found" + ex.ToString());
                }
                finally
                {
                    if (SQLConnection.State == ConnectionState.Open)
                    {
                        CloseConnection();
                    }
                }
            }
            return strMsg;
        }

        /// <summary>
        /// This method can execute an Insert, delete or update multiple query
        /// </summary>
        /// <param name="commands">SqlCommand[] Array of Commands to Execute</param>
        public void MakeTransaction(SqlCommand[] commands)
        {
            int i = 0;
            //SqlTransaction transaction;
            SQLConnection = new SqlConnection(strConnection);
            using (SQLConnection)
            {
                SqlTransaction transaction;
                SQLConnection.Open();
                transaction = SQLConnection.BeginTransaction();

                try
                {
                    foreach (SqlCommand command in commands)
                    {
                        if (command != null)
                        {
                            command.Transaction = transaction;
                            command.Connection = SQLConnection;
                            command.ExecuteNonQuery();
                            i++;
                        }
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new HttpException(ex.ToString());
                }
                catch (Exception err)
                {
                    int j = i;
                    transaction.Rollback();
                    throw new HttpException(err.Message);
                }
                finally
                {
                    if (SQLConnection.State == ConnectionState.Open)
                    {
                        CloseConnection();
                    }
                }
            }
        }

        /// <summary>
        /// This method can execute an Insert, delete or update multiple query
        /// </summary>
        /// <param name="commands">List<SqlCommand> List of Commands to Execute</param>
        public void MakeTransaction(List<SqlCommand> commands)
        {
            int i = 0;
            //SqlTransaction transaction;
            SQLConnection = new SqlConnection(strConnection);
            using (SQLConnection)
            {
                SqlTransaction transaction;
                SQLConnection.Open();
                transaction = SQLConnection.BeginTransaction();

                try
                {
                    foreach (SqlCommand command in commands)
                    {
                        if (command != null)
                        {
                            command.Transaction = transaction;
                            command.Connection = SQLConnection;
                            command.ExecuteNonQuery();
                            i++;
                        }
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new HttpException(ex.ToString());
                }
                catch (Exception err)
                {
                    int j = i;
                    transaction.Rollback();
                    throw new HttpException(err.Message);
                }
                finally
                {
                    if (SQLConnection.State == ConnectionState.Open)
                    {
                        CloseConnection();
                    }
                }
            }
        }
        /// <summary>
        /// This methos is use for Retrieving the Maximum Id.
        /// </summary>
        /// <param name="tbName">Name of the Table</param>
        /// <param name="field">Name of the Column</param>
        /// <returns> Numeric max Number</returns>
        public long GerMaxIDNumber(string tbName, string field)
        {
            try
            {
                SQLConnection = new SqlConnection(strConnection);
                using (SQLConnection)
                {
                    SQLConnection.Open();
                    long maxIDField = 0;
                    string strSQL = "select max(" + field + ") from " + tbName + " where (" + field + ") <>99999";
                    SqlCommand cmd = new SqlCommand(strSQL, SQLConnection);
                    //maxIDField =Convert.ToInt64(cmd.ExecuteScalar());

                    if (Convert.IsDBNull(cmd.ExecuteScalar()))
                    {
                        maxIDField = 0;
                    }
                    else
                    {
                        maxIDField = Convert.ToInt64(cmd.ExecuteScalar());
                    }
                    cmd = null;
                    SQLConnection.Close();
                    return maxIDField + 1;
                }
            }

            catch (SqlException ex)
            {
                throw new HttpException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new HttpException("SQL: Error found" + ex.ToString());
            }
            finally
            {
                if (SQLConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// This methos is use for Retrieving the Maximum Id.
        /// </summary>
        /// <param name="tbName">Name of the Table</param>
        /// <param name="field">Name of the Column</param>
        /// <returns> String max ID in 10 Char (Ex. 2170219001)</returns>
        public string GetMaxIdVar(string tbName, string field)
        {
            try
            {
                
                using (SQLConnection)
                {
                    CreateConnection();
                    String maxIDField = "";
                    int num = 0;
                    string substr = "";
                    string y1 = "";
                    string y2 = "";
                    DateTime dt = DateTime.Now;
                    string y = dt.Year.ToString();
                    string m = dt.Month.ToString();
                    string d = dt.Day.ToString();
                    if (m.Length == 1)
                    {
                        m = "0" + m;
                    }
                    if (d.Length == 1)
                    {
                        d = "0" + d;
                    }
                    y1 = y.Substring(0, 1);
                    y2 = y.Substring(2, 2);
                    y = y1 + y2;
                    string thisyeaar = y + "" + m + "" + d;
                    string strSQL = "select max(" + field + ") from " + tbName + " where " + field + " Like'" + thisyeaar + "%'";
                    SqlCommand cmd = new SqlCommand(strSQL, SQLConnection);
                    string strMaxID = this.GetScalarVal(cmd);
                    if (string.IsNullOrEmpty(strMaxID) == true)
                    {
                        thisyeaar = thisyeaar + "001";
                    }
                    else
                    {
                        maxIDField = strMaxID;
                        int intCardLength = Convert.ToInt32(maxIDField.Length);
                        substr = maxIDField.Substring(7, 3).ToString();
                        num = Convert.ToInt16(substr) + 1;
                        thisyeaar = thisyeaar + num.ToString().PadLeft(3, '0');
                    }
                    cmd = null;
                    CloseConnection();
                    return thisyeaar;
                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("SQL: Error found" + ex.ToString());
            }
            finally
            {
                if (SQLConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
        }
        /// <summary>
        /// This methos is use for Retrieving the Maximum Id.
        /// </summary>
        /// <param name="tbName">Name of the Table</param>
        /// <param name="field">Name of the Column</param>
        /// <param name="inLen">Length of Auto Number</param>
        /// <returns> String max ID as per inLen (Ex. if inLen=3 Then 2170219001)</returns>
        public string GetMaxIdVar(string tbName, string field, int inLen)
        {
            try
            {
                using (SQLConnection)
                {
                    CreateConnection();
                    string maxIDField = "";

                    int num = 0;
                    string strSQL = "select max(" + field + ") from " + tbName;
                    SqlCommand cmd = new SqlCommand(strSQL, SQLConnection);

                    if (Convert.IsDBNull(cmd.ExecuteScalar()))
                    {
                        maxIDField = "1".PadLeft(inLen, '0');
                    }
                    else
                    {
                        maxIDField = Convert.ToString(cmd.ExecuteScalar());
                        int intCardLength = Convert.ToInt32(maxIDField.Length);
                        num = Convert.ToInt16(maxIDField) + 1;
                        maxIDField = num.ToString().PadLeft(inLen, '0');
                    }
                    cmd = null;
                    CloseConnection();
                    return maxIDField;
                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("SQL: Error found" + ex.ToString());
            }
            finally
            {
                if (SQLConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// This methos is use for checking duplicate value in table.
        /// </summary>
        /// <param name="TableName">Name of the Table</param>
        /// <param name="ValueField">Name of the value Column</param>
        /// <param name="TestValueField">Value of the Value Column</param>
        /// <param name="IdField">Parimary/Unique Column Name of the Table</param>
        /// <param name="TestIdField">Value of the Parimary/Unique Column</param>
        /// <param name="isUpdate">Data in Insert Mode or Update Mode</param>
        /// <returns> True if Duplicate; False if Not Duplicate </returns>
        public bool IsDuplicate(string TableName, string ValueField, string TestValueField, string IdField, string TestIdField, bool isUpdate)
        {
            string strSQL = "";
            if (isUpdate == false)
                strSQL = "SELECT " + ValueField + " FROM " + TableName + " WHERE " + ValueField + " = '" + TestValueField + "'";
            else
                strSQL = "SELECT " + ValueField + " FROM " + TableName + " WHERE " + ValueField + " = '" + TestValueField + "' AND " + IdField + " <> '" + TestIdField + "'";

            SqlConnection objCon = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(strSQL, objCon);
            try
            {
                using (objCon)
                {
                    objCon.Open();
                    if (cmd.ExecuteScalar() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    //cmd = null; 
                    //objCon.Close();
                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("SQL: Error found" + ex.ToString());
            }
            finally
            {
                cmd = null;
                objCon.Close();
            }

        }
        #region Query & Parameter Generator

        public void SaveDataTable(DataTable dtData, string CmdType)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd = GenerateDML(dtData, CmdType);
                ExecuteQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// ONE MASTER RECORD MULTI CHILD RECORD
        /// DELETE ALL CHILD RECORD THEN INSERT ALL CHILD RECORD
        public void SaveDataTable(List<DataRow> lstRow, string CmdType)
        {
             
            try
            {
                if(lstRow.Count>1)
                {
                    List<SqlCommand> lstCmd = new List<SqlCommand>();
                    // PRIMARY TABLE OPERATION
                    lstCmd.Add(GenerateDML(lstRow[0], CmdType));

                    // DELETE CHILD TABLE RECORD
                    lstCmd.Add(DeleteData(lstRow[0], lstRow[1].Table.TableName.ToString()));

                    // INSERT CHILD TABLE RECORD
                    for(int i=1;i< lstRow.Count;i++)
                    {
                        lstCmd.Add(GenerateDML(lstRow[i], "I"));
                    }
                    MakeTransaction(lstCmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        public SqlCommand GenerateDML(DataTable dtData, string CmdType)
        {
            string strSQL = "";
            string strColumns = "";
            string strValues = "";
            string strWhere = "";
            int unColCount = 0;

            switch (CmdType)
            {
                case "I":
                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        if (strColumns == "")
                        {
                            strColumns = dtData.Columns[i].ColumnName.ToString();
                            strValues = "@" + strColumns;
                        }
                        else
                        {
                            strColumns = strColumns + ", " + dtData.Columns[i].ColumnName.ToString();
                            strValues = strValues + ",@" + dtData.Columns[i].ColumnName.ToString();
                        }

                    }
                    strSQL = "INSERT INTO " + dtData.TableName.ToString() + "(" + strColumns + ") VALUES(" + strValues + ")";
                    break;
                case "U":
                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        DataColumn[] primCols = dtData.PrimaryKey;
                        if (dtData.Columns[i].Unique == true || this.FindInColCollections(primCols, dtData.Columns[i].ColumnName) == true)
                        {
                            if (strWhere == "")
                            {
                                strWhere = dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                            }
                            else
                            {
                                strWhere = " AND " + dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                            }
                            unColCount++;
                        }
                        else
                        {
                            if (strColumns == "")
                            {
                                strColumns = dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                            }
                            else
                            {
                                strColumns = strColumns + ", " + dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                            }
                        }

                    }
                    strSQL = "UPDATE " + dtData.TableName.ToString() + " SET " + strColumns + " WHERE " + strWhere;
                    // strSQL = " UPDATE GENERAL_INFO SET EMP_NM=:EMP_NM, F_NM=:F_NM WHERE GOVT_ID=:GOVT_ID ";
                    break;
            }


            SqlParameter[] param = new SqlParameter[dtData.Columns.Count];
            //SqlParameter[] param = new SqlParameter[2];
            SqlCommand cmd = new SqlCommand(strSQL);
            int[] posUnCol = new int[unColCount];
            int j = 0;
            int p = 0;

            switch (CmdType)
            {
                case "I":
                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        param[i] = new SqlParameter();

                        if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == true) || (dtData.Rows[0][i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                        {
                            param[i] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), DBNull.Value);
                        }
                        else
                        {
                            param[i] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), SetColDataType(dtData.Columns[i].DataType.Name));
                        }
                        param[i].Direction = ParameterDirection.Input;
                        if (dtData.Columns[i].AllowDBNull == true)
                        {
                            param[i].IsNullable = true;
                        }


                        if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == false) && (dtData.Rows[0][i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                        {
                            if (dtData.Columns[i].DataType.Name == "DateTime")
                                //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                                param[i].Value = dtData.Rows[0][i];
                            else
                                param[i].Value = dtData.Rows[0][i].ToString().Trim();

                        }

                    }

                    break;
                case "U":

                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        // Intializing the Columns Parameter First
                        DataColumn[] primCols = dtData.PrimaryKey;
                        if (dtData.Columns[i].Unique == false && this.FindInColCollections(primCols, dtData.Columns[i].ColumnName) == false)
                        {

                            param[j] = new SqlParameter();
                            if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == true) || (dtData.Rows[0][i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                            {
                                param[j] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), DBNull.Value);
                            }
                            else
                            {
                                param[j] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), SetColDataType(dtData.Columns[i].DataType.Name));
                            }
                            param[j].Direction = ParameterDirection.Input;
                            if (dtData.Columns[i].AllowDBNull == true)
                            {
                                param[j].IsNullable = true;
                            }
                            if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == false) && (dtData.Rows[0][i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                            {
                                if (dtData.Columns[i].DataType.Name == "DateTime")
                                    //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                                    param[j].Value = dtData.Rows[0][i];
                                else
                                    param[j].Value = dtData.Rows[0][i].ToString().Trim();

                            }

                            j++;
                        }
                        else
                        {
                            posUnCol[p] = i;
                            p++;
                        }

                    }

                    // Intializing the WHERE Condition Parameter at the end
                    for (int k = 0; k < posUnCol.Length; k++)
                    {
                        param[j] = new SqlParameter();
                        param[j] = cmd.Parameters.Add(dtData.Columns[posUnCol[k]].ColumnName.ToString(), SetColDataType(dtData.Columns[posUnCol[k]].DataType.Name));
                        param[j].Direction = ParameterDirection.Input;
                        if (dtData.Columns[p].AllowDBNull == true)
                        {
                            param[j].IsNullable = true;
                        }
                        if ((string.IsNullOrEmpty(dtData.Rows[0][posUnCol[k]].ToString().Trim()) == false) && (dtData.Rows[0][posUnCol[k]].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                        {
                            if (dtData.Columns[posUnCol[k]].DataType.Name == "DateTime")
                                param[j].Value = dtData.Rows[0][posUnCol[k]];
                            else
                                param[j].Value = dtData.Rows[0][posUnCol[k]].ToString().Trim();

                        }
                    }

                    break;

            }
            return cmd;
        }

        public SqlCommand GenerateDML(DataRow dRow, string CmdType)
        {
            string strSQL = "";
            string strColumns = "";
            string strValues = "";
            string strWhere = "";
            int unColCount = 0;

            switch (CmdType)
            {
                case "I":
                    for (int i = 0; i < dRow.Table.Columns.Count; i++)
                    {
                        if (strColumns == "")
                        {
                            strColumns = dRow.Table.Columns[i].ColumnName.ToString();
                            strValues = "@" + strColumns;
                        }
                        else
                        {
                            strColumns = strColumns + ", " + dRow.Table.Columns[i].ColumnName.ToString();
                            strValues = strValues + ",@" + dRow.Table.Columns[i].ColumnName.ToString();
                        }

                    }
                    strSQL = "INSERT INTO " + dRow.Table.TableName.ToString() + "(" + strColumns + ") VALUES(" + strValues + ")";
                    break;
                case "U":
                    for (int i = 0; i < dRow.Table.Columns.Count; i++)
                    {
                        DataColumn[] primCols = dRow.Table.PrimaryKey;
                        if (dRow.Table.Columns[i].Unique == true || this.FindInColCollections(primCols, dRow.Table.Columns[i].ColumnName) == true)
                        {
                            if (strWhere == "")
                            {
                                strWhere = dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                            }
                            else
                            {
                                strWhere = strWhere + " AND " + dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                            }
                            unColCount++;
                        }
                        else
                        {
                            if (strColumns == "")
                            {
                                strColumns = dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                            }
                            else
                            {
                                strColumns = strColumns + ", " + dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                            }
                        }

                    }
                    strSQL = "UPDATE " + dRow.Table.TableName.ToString() + " SET " + strColumns + " WHERE " + strWhere;
                    // strSQL = " UPDATE GENERAL_INFO SET EMP_NM=:EMP_NM, F_NM=:F_NM WHERE GOVT_ID=:GOVT_ID ";
                    break;

            }


            SqlParameter[] param = new SqlParameter[dRow.Table.Columns.Count];
            //SqlParameter[] param = new SqlParameter[2];
            SqlCommand cmd = new SqlCommand(strSQL);
            int[] posUnCol = new int[unColCount];
            int j = 0;
            int p = 0;

            switch (CmdType)
            {
                case "I":
                    for (int i = 0; i < dRow.Table.Columns.Count; i++)
                    {
                        param[i] = new SqlParameter();

                        if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == true) || (dRow[i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                        {
                            param[i] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), DBNull.Value);
                        }
                        else
                        {
                            param[i] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), SetColDataType(dRow.Table.Columns[i].DataType.Name));
                        }
                        param[i].Direction = ParameterDirection.Input;
                        if (dRow.Table.Columns[i].AllowDBNull == true)
                        {
                            param[i].IsNullable = true;
                        }


                        if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == false) && (dRow[i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                        {
                            if (dRow.Table.Columns[i].DataType.Name == "DateTime")
                                //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                                param[i].Value = dRow[i];
                            else
                                param[i].Value = dRow[i].ToString().Trim();

                        }

                    }

                    break;
                case "U":

                    for (int i = 0; i < dRow.Table.Columns.Count; i++)
                    {
                        // Intializing the Columns Parameter First

                        DataColumn[] primCols = dRow.Table.PrimaryKey;
                        if (dRow.Table.Columns[i].Unique == false && this.FindInColCollections(primCols, dRow.Table.Columns[i].ColumnName) == false)
                        {

                            param[j] = new SqlParameter();
                            if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == true) || (dRow[i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                            {
                                param[j] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), DBNull.Value);
                            }
                            else
                            {
                                param[j] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), SetColDataType(dRow.Table.Columns[i].DataType.Name));
                            }
                            param[j].Direction = ParameterDirection.Input;
                            if (dRow.Table.Columns[i].AllowDBNull == true)
                            {
                                param[j].IsNullable = true;
                            }
                            if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == false) && (dRow[i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                            {
                                if (dRow.Table.Columns[i].DataType.Name == "DateTime")
                                    //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                                    param[j].Value = dRow[i];
                                else
                                    param[j].Value = dRow[i].ToString().Trim();

                            }

                            j++;
                        }
                        else
                        {
                            posUnCol[p] = i;
                            p++;
                        }

                    }

                    // Intializing the WHERE Condition Parameter at the end
                    for (int k = 0; k < posUnCol.Length; k++)
                    {
                        param[j] = new SqlParameter();
                        param[j] = cmd.Parameters.Add(dRow.Table.Columns[posUnCol[k]].ColumnName.ToString(), SetColDataType(dRow.Table.Columns[posUnCol[k]].DataType.Name));
                        param[j].Direction = ParameterDirection.Input;
                        if (dRow.Table.Columns[p].AllowDBNull == true)
                        {
                            param[j].IsNullable = true;
                        }
                        if ((string.IsNullOrEmpty(dRow[posUnCol[k]].ToString().Trim()) == false) && (dRow[posUnCol[k]].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                        {
                            if (dRow.Table.Columns[posUnCol[k]].DataType.Name == "DateTime")
                                param[j].Value = dRow[posUnCol[k]];
                            else
                                param[j].Value = dRow[posUnCol[k]].ToString().Trim();

                        }
                    }

                    break;


            }
            return cmd;
        }


        public SqlCommand DeleteData(DataRow KeyRow, string DeleteTableName)
        {
            SqlCommand delCmd = new SqlCommand();
            string strWhere = "";
            string strSQL = "";
            DataColumn[] primCols = KeyRow.Table.PrimaryKey;
            SqlParameter[] param = new SqlParameter[KeyRow.Table.PrimaryKey.Count()];
            for (int i = 0; i < KeyRow.Table.Columns.Count; i++)
            {
                if (KeyRow.Table.Columns[i].Unique == true || this.FindInColCollections(primCols, KeyRow.Table.Columns[i].ColumnName) == true)
                {
                    if (strWhere == "")
                    {
                        strWhere = KeyRow.Table.Columns[i].ColumnName.ToString() + "=@" + KeyRow.Table.Columns[i].ColumnName.ToString();
                    }
                    else
                    {
                        strWhere = strWhere + " AND " + KeyRow.Table.Columns[i].ColumnName.ToString() + "=@" + KeyRow.Table.Columns[i].ColumnName.ToString();
                    }
                }

            }
            strSQL = "DELETE FROM " + DeleteTableName + " WHERE " + strWhere;
            delCmd = new SqlCommand(strSQL);

            for (int k = 0; k < primCols.Length; k++)
            {
                param[k] = new SqlParameter();
                param[k] = delCmd.Parameters.Add(primCols.ElementAt(k).ColumnName.ToString(), SetColDataType(primCols.ElementAt(k).DataType.Name));
                param[k].Direction = ParameterDirection.Input;
                if (primCols.ElementAt(k).AllowDBNull == true)
                {
                    param[k].IsNullable = true;
                }
                if ((string.IsNullOrEmpty(KeyRow[primCols.ElementAt(k).ToString().Trim()].ToString().Trim()) == false) && (KeyRow[primCols.ElementAt(k).ToString().Trim()].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                {
                    if (primCols.ElementAt(k).DataType.Name == "DateTime")
                        param[k].Value = KeyRow[primCols.ElementAt(k).ToString().Trim()];
                    else
                        param[k].Value = KeyRow[primCols.ElementAt(k).ToString().Trim()];

                }
            }

            return delCmd;

        }
        protected SqlDbType SetColDataType(string name)
        {
            SqlDbType dbType = new SqlDbType();
            switch (name)
            {
                case "Decimal":
                    dbType = SqlDbType.Decimal;
                    break;
                case "String":
                    dbType = SqlDbType.VarChar;
                    break;
                case "DateTime":
                    dbType = SqlDbType.Date;
                    break;
                case "Char":
                    dbType = SqlDbType.Char;
                    break;
                case "Byte[]":
                    dbType = SqlDbType.Image;
                    break;
            }
            return dbType;
        }


        protected bool FindInColCollections(DataColumn[] cols, string strColName)
        {
            bool retValue = false;
            foreach (DataColumn col in cols)
            {
                if (strColName == col.ColumnName)
                {
                    retValue = true;
                    break;
                }
            }
            return retValue;
        }
        #endregion
    }
}