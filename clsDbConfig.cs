using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YCDbConfig
{
    public class YcDbConfg
    {
        private string ip;
        private UInt16 port = 1433;
        private string username;
        private string password;
        private string db_name;

        public YcDbConfg(string ip, UInt16 port, string username, string password, string db_name)
        {
            this.ip = ip;
            this.port = port;
            this.username = username;
            this.password = password;
            this.db_name = db_name;
        }

        public string IP
        {
            get
            {
                return this.ip;
            }
        }

        public string GetUserNamme
        {
            get
            {
                return this.username;
            }
        }
        public string GetPassword
        {
            get
            {
                return this.password;
            }
        }

        public string GetDbName
        {
            get
            {
                return this.db_name;
            }
        }

        public UInt16 GetPort
        {
            get
            {
                return this.port;
            }
        }

        public YcDbConfg(byte[] ar)
        {
            try
            {
                int index = 0;
                byte[] art = new byte[0];
                int l = 0;

                this.port = BitConverter.ToUInt16(ar, index);
                index += 2;

                l = BitConverter.ToInt32(ar, index);
                index += 4;

                art = new byte[l];
                Array.Copy(ar, index, art, 0, l);
                index += l;

                this.ip = Encoding.UTF8.GetString(art);

                l = BitConverter.ToInt32(ar, index);
                index += 4;

                art = new byte[l];
                Array.Copy(ar, index, art, 0, l);
                index += l;

                this.username = Encoding.UTF8.GetString(art);

                l = BitConverter.ToInt32(ar, index);
                index += 4;

                art = new byte[l];
                Array.Copy(ar, index, art, 0, l);
                index += l;

                this.password = Encoding.UTF8.GetString(art);


                l = BitConverter.ToInt32(ar, index);
                index += 4;

                art = new byte[l];
                Array.Copy(ar, index, art, 0, l);
                index += l;

                this.db_name = Encoding.UTF8.GetString(art);
            }
            catch
            {
                this.port = 0;
                this.ip = "";
                this.username = "";
                this.password = "";
                this.db_name = "";
            }

        }

        public static bool Read(out YcDbConfg config)
        {
            bool res = false;
            config = null;

            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ycdata\\dbConfig.bin";

                FileStream fs = File.Open(path, FileMode.Open);
                byte[] ar = new byte[fs.Length];
                fs.Read(ar, 0, ar.Length);
                fs.Close();

                config = new YcDbConfg(ar);
                res = true;
            }
            catch { }

            return res;
        }

        public static bool Delete()
        {
            bool res = false;


            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ycdata\\dbConfig.bin";
                System.IO.File.Delete(path);
                res = true;
            }
            catch { }

            return res;
        }

        public static bool Write(YcDbConfg config)
        {
            bool res = false;


            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ycdata";

                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                }

                FileStream fs = File.Open(path + "\\dbConfig.bin", FileMode.OpenOrCreate);
                byte[] ar = config.ToBytes();
                fs.Write(ar, 0, ar.Length);
                fs.Close();
                res = true;
            }
            catch { }

            return res;
        }

        public static string BuildConnectionString()
        {
            string res;
            YcDbConfg ycs;

            if (Read(out ycs))
            {
                res = @"Data Source=" + ycs.ip + ";AttachDbFilename=";
                res += ycs.db_name + ";";

                res += "Persist Security Info = True; Connect Timeout = 60";

                if (ycs.GetUserNamme.Length > 0)
                {
                    res += ";User Id=" + ycs.GetUserNamme;
                }

                if (ycs.GetPassword.Length > 0)
                {
                    res += ";password=" + ycs.GetPassword;
                }
            }
            else
            {
                res = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=";
                res += Path.GetPathRoot(Environment.SystemDirectory) + "\\YCDB5\\ycdb.mdf";
                res += ";Persist Security Info = True; Connect Timeout = 60;";
            }

            return res;
        }

        public static string GetDbDir()
        {
            string res = "";

            YcDbConfg ycs;
            if (Read(out ycs))
            {
                int p = ycs.db_name.LastIndexOf("\\", 0);
                res = ycs.db_name.Substring(0, p);
            }
            else
            {
                res = Path.GetPathRoot(Environment.SystemDirectory) + "\\YCDB5\\";
            }
            return res;
        }

        public static string GetDbPath()
        {
            string res = "";

            YcDbConfg ycs;
            if (Read(out ycs))
            {
                res = ycs.db_name;
            }
            else
            {
                res = Path.GetPathRoot(Environment.SystemDirectory) + "\\YCDB5\\ycdb.mdf";
            }
            return res;
        }

        public byte[] ToBytes()
        {
            List<byte> ar = new List<byte>();

            ar.AddRange(BitConverter.GetBytes(this.port));

            ar.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetBytes(this.ip).Count()));
            ar.AddRange(Encoding.UTF8.GetBytes(this.ip));

            ar.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetBytes(this.username).Count()));
            ar.AddRange(Encoding.UTF8.GetBytes(this.username));

            ar.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetBytes(this.password).Count()));
            ar.AddRange(Encoding.UTF8.GetBytes(this.password));

            ar.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetBytes(this.db_name).Count()));
            ar.AddRange(Encoding.UTF8.GetBytes(this.db_name));
            return ar.ToArray();
        }
    }
}
