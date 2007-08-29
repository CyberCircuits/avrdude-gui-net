using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using avrdudegui.Orodja;

namespace avrdudegui
{
    public partial class Varovalke:Form
    {

        public Varovalke()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string cip = Vrednosti.Mikrokontroler.Split(' ')[1].ToLower().Remove(0, 2); ;
            cip = "AT" + cip;
            string naslov = "http://palmavr.sourceforge.net/cgi-bin/fc.cgi?P_PREV=" + cip + "&P=" + cip + Vrednosti.Spletna_stran + "&O_HEX=Apply+user+values";
            System.Diagnostics.Process.Start(naslov);
        }

        private void zapi�i_varovalke_Click(object sender, EventArgs e)
        {
            string[] cip = Vrednosti.Mikrokontroler.Split(' ');
            DialogResult result;
            result = MessageBox.Show("Ali ste prepri�ani da �elite zapisati varovalke?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Pripomo�ki.Zagon(@"-c " + Vrednosti.Programator + " -p " + cip[0] + " -P " + Vrednosti.Port + " -s -q -e -u -U hfuse:w:0x" + hfuse_vrstica.Text + ":m -U lfuse:w:0x" + lfuse_vrstica.Text + ":m");
            }
        }

        void preberi_varovalke_Click(object sender, System.EventArgs e)
        {
            string[] cip = Vrednosti.Mikrokontroler.Split(' ');
            Vrednosti.Spletna_stran = null;
            string izhod = Pripomo�ki.Zagon(@"-c " + Vrednosti.Programator + " -p " + cip[0] + " -P " + Vrednosti.Port + " -q -U lock:r:lock:h -U lfuse:r:lfuse:h -U hfuse:r:hfuse:h -U efuse:r:efuse:h");

            if (File.Exists("lfuse"))
            {
                lfuse_vrstica.Enabled = true;
                lfuse_vrstica.Text = File.OpenText("lfuse").ReadLine().Remove(0, 2).ToUpper();
                Vrednosti.Spletna_stran += "&V_LOW=" + lfuse_vrstica.Text;
            }
            if (File.Exists("hfuse"))
            {
                hfuse_vrstica.Enabled = true;
                hfuse_vrstica.Text = File.OpenText("hfuse").ReadLine().Remove(0, 2).ToUpper();
                Vrednosti.Spletna_stran += "&V_HIGH=" + hfuse_vrstica.Text;
            }
            if (File.Exists("lock"))
            {
                lockb_vrstica.Enabled = true;
                lockb_vrstica.Text = File.OpenText("lock").ReadLine().Remove(0, 2).ToUpper();
            }
            if (File.Exists("efuse"))
            {
                efuse_vrstica.Enabled = true;
                efuse_vrstica.Text = File.OpenText("lock").ReadLine().Remove(0, 2).ToUpper();
            }
            zapi�i_varovalke.Enabled = true;
            povezava_do_kalkulatorja.Enabled = true;
        }

    }
}
