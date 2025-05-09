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
    public partial class Lounge : Form
    {
        private Main mainForm;
        private Panel chatBackgroundPanel;
 
        public Lounge(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            // 배경 패널 (반투명 효과)
            chatBackgroundPanel = new Panel();
            chatBackgroundPanel.Size = new Size(400, 200);
            chatBackgroundPanel.Location = new Point(this.ClientSize.Width - 430, this.ClientSize.Height - 230);
            chatBackgroundPanel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            chatBackgroundPanel.BackColor = Color.Transparent;
            chatBackgroundPanel.Paint += (s, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, chatBackgroundPanel.ClientRectangle);
                }
            };

            // 채팅 로그 RichTextBox
            chatLogBox.ReadOnly = true;
            chatLogBox.BackColor = Color.Black; // 실제 배경은 안 보이지만 대비를 위해
            chatLogBox.ForeColor = Color.Black;
            chatLogBox.BorderStyle = BorderStyle.None;
            chatLogBox.Font = new Font("맑은 고딕", 9);
           // chatLogBox.Size = new Size(380, 300);
            chatLogBox.ScrollBars = RichTextBoxScrollBars.Vertical;

            // 입력 박스
            inputBox.PlaceholderText = "메시지를 입력하세요...";
            inputBox.Font = new Font("맑은 고딕", 9);
         //   inputBox.Size = new Size(380, 40);
            inputBox.BorderThickness = 0;
            inputBox.FillColor = Color.FromArgb(30, 30, 30);
            inputBox.ForeColor = Color.White;
            inputBox.BorderRadius = 5;

        }

        private void Lounge_Load(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.bgm.Stop();   // BGM 종료
                //mainForm.Hide();      // MainForm 숨김
            }
        }
    }
}
