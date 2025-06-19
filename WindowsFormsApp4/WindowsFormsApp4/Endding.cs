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
    public partial class Endding : Form
    {
        public List<Character> Standings { get; set; }
        public Endding()
        {
            InitializeComponent();
            
        }

        private void Endding_Load(object sender, EventArgs e)
        {
            if (Standings != null)
            {
                dataGridView1.DataSource = Standings
                    .OrderByDescending(c => c.HP)
                    .Select((c, idx) => new {
                        순위 = idx + 1,
                        이름 = c.Name,
                        HP = c.HP
                    })
                    .ToList();
            }
        }


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
