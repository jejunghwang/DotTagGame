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
        public lbl_bgm_toggle(bool isBgmPlaying, bool isAltBg)
        {
            InitializeComponent();
            music_toggle.Checked = isBgmPlaying;
            UpdateLabel(isBgmPlaying);
            mode_toggle.Checked = isAltBg;
            UpdateModeLabel(isAltBg);
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
            bool nowAlt = mode_toggle.Checked;
            if (Owner is Lounge lounge)
                lounge.SetBackgroundMode(nowAlt);

            UpdateModeLabel(nowAlt);
        }

        private void UpdateModeLabel(bool alt)
        {
            lbl_mode_toggle.Text = alt ? "DARK" : "LIGHT";
            lbl_mode_toggle.ForeColor = alt ? Color.Black : Color.Gold;
        }

        private void other_Click(object sender, EventArgs e)
        {
            using (var dlg = new ColorDialog())
            {
                if (this.Owner is Lounge lounge)
                    dlg.Color = lounge.BackColor;

                dlg.AllowFullOpen = true;    // 사용자 지정 색상 허용
                dlg.AnyColor = true;    // 모든 색상 선택 가능
                dlg.SolidColorOnly = false;  // 그라데이션 등도 허용할지

                if (dlg.ShowDialog() == DialogResult.OK && this.Owner is Lounge ownerLounge)
                {
                    ownerLounge.SetCustomBackgroundColor(dlg.Color);
                }
            }
        }
    }
}
