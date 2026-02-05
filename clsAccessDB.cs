namespace YCDbConfig
{
    using System.Data.SqlClient;
    using System.Data;
    using System;
    internal class SqlServerDB
    {
        private string connection_string;
        internal SqlServerDB(string connection_string) 
        {
            this.connection_string = connection_string;
        }
        internal int ExcuteNonQuery(string cmdsql)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(this.connection_string))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(cmdsql, con);
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
            }

            return res;
        }
        internal bool ExcuteQuery(string cmdsql,ref DataTable dt)
        {
            bool res = false;
            try
            {
                using (SqlDataAdapter adp = new SqlDataAdapter(cmdsql, new SqlConnection(this.connection_string)))
                {
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        res = true;
                    }
                }
            }
            catch(Exception e)
            {  
            }

            return res;
        }
    }
}
