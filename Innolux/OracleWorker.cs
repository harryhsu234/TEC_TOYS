using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace INNOLUX_DB
{
    class OracleWorker
    {
        static String dbType = "Oracle";
        public static string m_ConnectionString = string.Empty;

        public static string GetConnectionString()
        {
            if (m_ConnectionString == string.Empty)
            {

                m_ConnectionString = ConfigurationManager.ConnectionStrings["RPT"].ConnectionString; //ConfigurationManager.ConnectionStrings["ConnSQL"].ToString(); ;
            }
            return m_ConnectionString;
        }

        public static OracleConnection GetConnection()
        {
            OracleConnection cn = new OracleConnection();
            cn.ConnectionString = GetConnectionString();
            return cn;
        }

        public static DataTable SelectDataTable(string strSQL)
        {
            return SelectDataTable(strSQL, null);
        }
        public static int Execute(string strSQL)
        {
            int Num = -1;
            OracleConnection cn = GetConnection();
            cn.Open();
            try
            {
                Num = new OracleCommand(strSQL, cn).ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                cn.Close();
            }
            return Num;
        }

        public static DataTable SelectDataTable(string strSQL, Hashtable args)
        {

            DataTable data = new DataTable();
            OracleConnection cn = new OracleConnection();
            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.ConnectionString = GetConnectionString();
                    cn.Open();
                }
                OracleCommand cmd = new OracleCommand(strSQL, cn);
                if (args != null) SetArgs(strSQL, args, cmd);
                new OracleDataAdapter(strSQL, cn).Fill(data);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                cn.Close();
            }
            return data;
        }

        private static void SetArgs(string sql, Hashtable args, IDbCommand cmd)
        {
            if (dbType == "SqlServer")
            {
                MatchCollection ms = Regex.Matches(sql, @"@\w+");

                foreach (Match m in ms)
                {
                    string key = m.Value;

                    Object value = args[key];
                    if (value == null)
                    {
                        value = args[key.Substring(1)];
                    }
                    if (value == null) value = DBNull.Value;

                    cmd.Parameters.Add(new OracleParameter(key, value));
                }
                cmd.CommandText = sql;
            }
        }
    }
}
