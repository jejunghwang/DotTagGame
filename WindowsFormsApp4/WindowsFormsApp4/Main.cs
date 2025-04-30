using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
//test
namespace WindowsFormsApp4
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btn_manual_Click(object sender, EventArgs e)
        {
            /*SoundPlayer click = new SoundPlayer(@"Resources/menu_button-89141.mp3");
            click.Play();*/

            Manual manual = new Manual();
            manual.Owner = this;
            manual.Show();
            //this.Visible = false;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Start start = new Start();
            start.Owner = this;
            start.Show();
        }
    }
}
