using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Alarme
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string section, string key,
            string value, string filePath);

        #region Def_FlashWindow

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }
        private const uint FLASHW_STOP = 0;
        private const uint FLASHW_CAPTION = 1;
        private const uint FLASHW_TRAY = 2;
        private const uint FLASHW_ALL = 3;
        private const uint FLASHW_TIMER = 4;
        private const uint FLASHW_TIMERNOFG = 12;

        #endregion

        #region Def_SOM

        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */ //同步
            SND_ASYNC = 0x0001,  /* play asynchronously */ //异步
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        [DllImport("winmm")]
        public static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

        #endregion

        DateTime dtAlarme;
        Boolean Tocando = false;
        Boolean Carregando = false;
        Boolean InicDepoisHora = false;
        Boolean PrimeiroTick = true;
        int TmpToc = 0;

        #region Eventos
            public Form1()
            {
                InitializeComponent();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
                Carregando = true;
                txAcordar.Text = LeIni("Hora", "07:20:00");

                // txAcordar.Text = "14:28:00";
                dtAlarme = Convert.ToDateTime(txAcordar.Text);

                cbSoneca.SelectedIndex = int.Parse(LeIni("Soneca", "0"));
            }

            private void timer1_Tick(object sender, EventArgs e)
        {
            Carregando = false;
            TimeSpan span = DateTime.Now.Subtract(dtAlarme);
            if (Tocando)
            {
                TmpToc++;
                if (TmpToc > 2)
                    TocaSom();
            }
            else
            {

                if (DateTime.Now > dtAlarme)
                {
                    if (PrimeiroTick)
                    {
                        dtAlarme = dtAlarme.AddDays(1);
                    }
                    else
                    {
                        BtnStart.Enabled = true;
                        cbSoneca.Enabled = true;
                        this.TopMost = true;
                        TocaSom();
                        FazFlash();
                        if (this.WindowState == FormWindowState.Minimized)
                            this.WindowState = FormWindowState.Normal;
                    }
                }
            }
            PrimeiroTick = false;
            String sTempo = span.ToString();
            watchLb.Text = sTempo.Substring(1, 8);
        }

        private void TocaSom()
            {
                TmpToc = 0;
                Tocando = true;
                string s = Application.StartupPath + "\\galo.wav";
                PlaySound(s, IntPtr.Zero, PlaySoundFlags.SND_FILENAME | PlaySoundFlags.SND_ASYNC | PlaySoundFlags.SND_NODEFAULT);
            }

            private void BtnStart_Click(object sender, EventArgs e)
            {
                TiraFlash();
                BtnStart.Enabled = false;
                cbSoneca.Enabled = false;
                dtAlarme = HoraPraTocar(DateTime.Now);
                txAcordar.Text = dtAlarme.ToLongTimeString();
                Tocando = false;

            }

            private void txAcordar_Leave(object sender, EventArgs e)
            {
                EscreveINI("Hora", txAcordar.Text);
                dtAlarme = Convert.ToDateTime(txAcordar.Text);
            }

            private void cbSoneca_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (Carregando == false)
                {
                    EscreveINI("Soneca", cbSoneca.SelectedIndex.ToString());
                }
            }

        #endregion

        private DateTime HoraPraTocar(DateTime HoraINI)
        {
            int QtdSegSon = 0;
            int iSoneca = cbSoneca.SelectedIndex;            
            switch (iSoneca)
            {
                case 1: // 5 minutos
                    QtdSegSon = 5 * 60;
                    break;
                case 2: // 10 minutos
                    QtdSegSon = 10 * 60;
                    break;
                case 3: // 15 minutos
                    QtdSegSon = 15 * 60;
                    break;
                case 4: // 20 minutos
                    QtdSegSon = 20 * 60;
                    break;
                case 5: // 30 minutos
                    QtdSegSon = 30 * 60;
                    break;
                case 6: //  1 hora
                    QtdSegSon = 60 * 60;
                    break;
                default: // 1 minuto
                    QtdSegSon = 60;
                    break;
            }
            DateTime HoraTocar = HoraINI.AddSeconds(QtdSegSon);
            return HoraTocar;
        }

        #region Flash
        private void FazFlash()
        {
            IntPtr hWnd = this.Handle;
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMER; // FLASHW_TIMERNOFG;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;
            FlashWindowEx(ref fInfo);
        }

        private void TiraFlash()
        {
            IntPtr hWnd = this.Handle;
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = FLASHW_STOP;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;
            FlashWindowEx(ref fInfo);
        }

        #endregion

        #region INI
        private String LeIni(String Key, String Default)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString("Alarme", Key, Default, temp, 255, Application.StartupPath + "\\Alarme.ini");
            return temp.ToString();
        }

        private void EscreveINI(String Key, String Texto)
        {
            WritePrivateProfileString("Alarme", Key, Texto, Application.StartupPath + "\\Alarme.ini");
        }
        #endregion

    }
}
