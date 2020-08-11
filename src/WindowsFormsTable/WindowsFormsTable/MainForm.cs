using System.Windows.Forms;
using ppsyqm;

namespace WindowsFormsTable
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.panelCtrl.AutoScroll = true;
            this.panelCtrl.Dock = DockStyle.Fill;
            {
                this.panelCtrl.Controls.Add(new SuperTableForm());
            }
        }
    }
}
