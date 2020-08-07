using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsTable
{
    public partial class MainForm : Form
    {
        TableCtrls tableCtrls = new TableCtrls();
        const int ROW_NUM = 20;
        const int COL_NUM = 13;
        const int ROW_WIDTH = 36;
        const int COL_HEIGHT = 36;
        List<EntityObject> dataList = new List<EntityObject>();
        public MainForm()
        {
            InitializeComponent();
            List<int> r_s_list = new List<int>();
            List<int> c_s_list = new List<int>();
            List<List<string>> s_list = new List<List<string>>();
            for (var i = 0; i < ROW_NUM; i++)
            {
                r_s_list.Add(ROW_WIDTH); 
            }
            for (var j = 0; j < COL_NUM; j++)
            {
                c_s_list.Add(COL_HEIGHT);
            }
            for (var i = 0; i < 10; i++)
            {
                s_list.Add(new List<string>() { });
                for (var j = 0; j < 10; j++)
                {
                    s_list[i].Add("你好");
                }
            }
            tableCtrls.init_list(ref s_list, ref r_s_list, ref c_s_list, 10, 10);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //绘制表格
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;  //图片柔顺模式选择
            g.CompositingQuality = CompositingQuality.HighQuality;//再加一点
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;//高质量

            tableCtrls.paint_table(g, sender, e);
            tableCtrls.paint_lines(g, sender, e);
            tableCtrls.paint_cells(g, sender, e);
        }
    }
}
