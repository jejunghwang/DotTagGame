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

        public Lounge(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Lounge_Load(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.bgm.Stop();   // BGM 종료
                mainForm.Hide();      // MainForm 숨김
            }
        }
    }
}
