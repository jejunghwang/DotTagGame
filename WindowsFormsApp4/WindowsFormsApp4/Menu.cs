using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace WindowsFormsApp4
{
    public partial class Menu : Form
    {
        private readonly string message =
            "술래로부터 도망쳐 승리를 쟁취하세요!\n" +
            "술래에게 잡히면 곧바로 술래가 됩니다.\n" +
            "술래가 되면 HP 가 계속해서 깎이게 된답니다...\n" +
            "HP가 다 닳면 패배하게 되니 조심하세요!!\n" +
            "또 다양한 아이템을 이용해서 술래로부터 도망치세요.\n" +
            "이제 친구들과 함께 시작해보세요!!!";
        private int currentIndex = 0;
         
        public Menu()
        {
            InitializeComponent();

            lbl_manual.Text = "";
        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            currentIndex = 0;
            lbl_manual.Text = "";
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < message.Length)
            {
                char c = message[currentIndex++];
                if (c == '\n')
                    lbl_manual.Text += Environment.NewLine;
                else
                    lbl_manual.Text += c;
            }
            else
            {
                timer.Stop();  // 다 찍었으면 타이머 정지
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
