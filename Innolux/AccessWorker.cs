using System.Data;
using System.Data.OleDb;

namespace INNOLUX_DB
{
    class AccessWorker
    {
        public void connectAccess()
        {
            string db_file = "D:\\INNOLUX\\AA.mdb";
            string sql = "select * from 資料明細表 ";

            DataTable dt = GetOleDbDataTable(db_file, sql);
        }

        public static DataTable GetOleDbDataTable(string db_file, string sql)
        {
            DataTable myDataTable = new DataTable();
            OleDbConnection icn = OleDbOpenConn(db_file);
            OleDbDataAdapter da = new OleDbDataAdapter(sql, icn);
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            myDataTable = ds.Tables[0];
            if (icn.State == ConnectionState.Open) icn.Close();
            return myDataTable;
        }
        public static void OleDbInsertUpdateDelete(string db_file, string sql)
        {
            // string cnstr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Database);
            OleDbConnection icn = OleDbOpenConn(db_file);
            OleDbCommand cmd = new OleDbCommand(sql, icn);
            cmd.ExecuteNonQuery();
            if (icn.State == ConnectionState.Open) icn.Close();
        }
        public static OleDbConnection OleDbOpenConn(string Database)
        {
            string cnstr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Database);
            OleDbConnection icn = new OleDbConnection();
            icn.ConnectionString = cnstr;
            if (icn.State == ConnectionState.Open) icn.Close();
            icn.Open();
            return icn;
        }
    }
}
