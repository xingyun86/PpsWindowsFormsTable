﻿using System.Windows.Forms;
using ppsyqm;

namespace WindowsFormsTable
{
    public partial class MainForm : Form
    {
        SuperTableForm superTableFormLeft = new SuperTableForm();
        SuperTableForm superTableFormRight = new SuperTableForm();
        public MainForm()
        {
            InitializeComponent();

            this.panelCtrl.AutoScroll = true;
            this.panelCtrl.Dock = DockStyle.Fill;
            {
                superTableFormLeft.InitForm();
                superTableFormLeft.Dock = DockStyle.Left;
                superTableFormLeft.FormBorderStyle = FormBorderStyle.None;
                this.panelCtrl.Controls.Add(superTableFormLeft);
                superTableFormLeft.Show();
            }
            {
                superTableFormRight.InitForm();
                superTableFormRight.Dock = DockStyle.Right;
                superTableFormRight.FormBorderStyle = FormBorderStyle.None;
                this.panelCtrl.Controls.Add(superTableFormRight);
                superTableFormRight.Show();
            }
        }
    }
}
