namespace YCDbConfig
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    internal static class Constants
    {
        static internal string dbName = "ycdb.mdf";
        static internal string dbLName = "ycdb_log.ldf";
        static internal string connection_string = YcDbConfg.BuildConnectionString(); //@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + dbPath + ";Persist Security Info=True;Connect Timeout=60;User Id=sa;password=YCDbConfig_2023_1";
        internal static string GetConnectionString
        {
            get
            {
                return connection_string;
            }
        }

  
        internal static string GetDbName
        {
            get
            {
                return dbName;
            }
        }

        internal static string GetDbLName
        {
            get
            {
                return dbLName;
            }
        }

    }
}
