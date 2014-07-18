using System;
using System.Data;
using System.Data.SqlClient;


    public  class clsDatabase 
    {
        private SqlConnection objConn;
        private SqlCommand objCmd;
        private SqlTransaction Trans;
        private String strConnString;

            public  clsDatabase() //(string Type)
            {
                strConnString = System.Configuration.ConfigurationManager.AppSettings["strConnString"].ToString();
            }
        public SqlDataReader QueryDataReader(String strSQL)
            {
                SqlDataReader dtReader;
                objConn = new SqlConnection();
                objConn.ConnectionString = strConnString;
                objConn.Open();
                objCmd = new SqlCommand(strSQL, objConn);
                dtReader = objCmd.ExecuteReader();
                objConn.Close();
                return dtReader; //*** Return DataReader ***//
            }

        public DataSet QueryDataSet(String strSQL)
        {
                DataSet ds = new DataSet();
                SqlDataAdapter dtAdapter = new SqlDataAdapter();
                objConn = new SqlConnection();
                objConn.ConnectionString = strConnString;
                objConn.Open();
                objCmd = new SqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = strSQL;
                objCmd.CommandType = CommandType.Text;
                dtAdapter.SelectCommand = objCmd;
                dtAdapter.Fill(ds);
                objConn.Close();
                return ds;   //*** Return DataSet ***//
        }

        public DataTable QueryDataTable(String strSQL)
          {
                SqlDataAdapter dtAdapter;
                DataTable dt = new DataTable();
                objConn = new SqlConnection();
                objConn.ConnectionString = strConnString;
                objConn.Open();
                dtAdapter = new SqlDataAdapter(strSQL, objConn);
                dtAdapter.Fill(dt);
                objConn.Close();
                return dt; //*** Return DataTable ***//
           }

        public Boolean QueryExecuteNonQuery(String strSQL)
            {
            objConn = new SqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();
            try
				{
				objCmd = new SqlCommand();
				objCmd.Connection = objConn;
				objCmd.CommandType = CommandType.Text;
				objCmd.CommandText = strSQL;
				objCmd.ExecuteNonQuery();
                objConn.Close();
				return true; //*** Return True ***//
				}
            catch (Exception)
				{
                objConn.Close();
                return false; //*** Return False ***//
				}
            }

        public Object QueryExecuteScalar(String strSQL)
            {
            Object obj;
            objConn = new SqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();
            try
            {
            objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strSQL;
            obj = objCmd.ExecuteScalar();  //*** Return Scalar ***//
            objConn.Close();
            return obj;
            }
            catch (Exception)
            {
            objConn.Close();
            return null; //*** Return Nothing ***//
            }
            }

        public void TransStart()
            {
            objConn = new SqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();
            Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted);
            objConn.Close();
            }

        public void TransExecute(String strSQL)
            {
            objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.Transaction = Trans;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strSQL;
            objCmd.ExecuteNonQuery();
            }

        public void TransRollBack()
            {
            Trans.Rollback();
            }

        public void TransCommit()
            {
            Trans.Commit();
            }
 
        public void Close()
            {
            objConn.Close();
            objConn = null;
            }

    }

