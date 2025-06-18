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
    public partial class lbl_bgm_toggle : Form
    {
        public lbl_bgm_toggle(bool isBgmPlaying)
        {
            InitializeComponent();
            music_toggle.Checked = isBgmPlaying;
            UpdateLabel(isBgmPlaying);
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
                UpdateLabel(music_toggle.Checked);
            }
        }

        private void UpdateLabel(bool isOn)
        {
            if (isOn)
            {
                bgm_toggle.Text = "ON";
                bgm_toggle.ForeColor = Color.RoyalBlue;
            }
            else
            {
                bgm_toggle.Text = "OFF";
                bgm_toggle.ForeColor = Color.Brown;
            }
        }

        private void mode_toggle_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
