using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Tess
{
    class Connection:IDisposable
    {
        public SqlConnection conn;
        public Connection()
        {
            if((conn=_conn())==null)
            {
                this.Dispose();
            }
        }
        private SqlConnection _conn()
        {
            SqlConnection c = null;
            string connectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;User Instance=True;AttachDbFilename=C:\Users\urbanowicz\documents\visual studio 2013\Projects\Tess\Tess\Database1.mdf";
            try
                {
                c=new SqlConnection(connectionString);
                c.Open();
                return c;
                }
                catch
            {
                System.Windows.MessageBox.Show("Blad połączenia");
                c.Dispose();
                return null;
                }
        }
        public void Dispose()
        {
            if(conn!=null)
            {
                conn.Dispose();
            }
        }
        public string NonQuery(string s)
        {
            using (SqlCommand cmd = new SqlCommand(s, this.conn))
            {
                return cmd.ExecuteNonQuery().ToString();
            }
        }
        public DataTable FillTable(string s)
        {
            SqlCommand cmd= new SqlCommand(s, this.conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Dictionary");
            da.Fill(dt);
            return dt;
        }
        public SqlDataReader Query(string s)
        {
            SqlCommand cmd = new SqlCommand(s, this.conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return reader;
            }
            else return null;
        }

      


    }
}
