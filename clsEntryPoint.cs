namespace YCDbConfig
{
    using System;
    using System.Windows.Forms;

    static class clsEntryPoint
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);           
            FrmDbConfig frmDbConfig = new FrmDbConfig();
            frmDbConfig.ShowDialog();

        }
    }
}
