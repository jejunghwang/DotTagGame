using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Setting : Form
    {
        public Setting(bool isBgmPlaying)
        {
            InitializeComponent();
            music_toggle.Checked = isBgmPlaying;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void music_toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Owner is Lounge lounge)
            {
                lounge.ToggleBgm(music_toggle.Checked);
            }
        }
    }
}
