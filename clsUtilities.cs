namespace YCDbConfig
{
    using System.Data.SqlClient;

    internal static class Utilities
    {  
        static public bool CheckServer()
        {
            bool res = false;
            try
            {
                string constring = @"Data Source=(LocalDB)\v11.0;Connect Timeout=60";
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                con.Close();
                res = true;

            }
            catch 
            {
            }
            return res;
        }
        internal static bool CheckServerSec(string connecting_sting)
        {
            bool res = false;
            try
            {
                SqlConnection con = new SqlConnection(connecting_sting);
                con.Open();
                con.Close();
                res = true;
            }
            catch { }

            return res;
        }
    }
}
