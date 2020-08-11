using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ppsyqm
{    class SuperTableForm: Form
    {
        SuperTable superTable = new SuperTable();
        List<EntityObject> dataList = new List<EntityObject>();
        public void InitForm(int ROW_NUM = 20, int COL_NUM = 13, int ROW_WIDTH = 36, int COL_HEIGHT = 36, int START_X = 10, int START_Y = 10)
        {
            this.TopLevel = false;
            this.Size = new System.Drawing.Size(ROW_NUM * ROW_WIDTH + START_X + 1, COL_NUM * COL_HEIGHT+ START_Y + 1);

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
            for (var i = 0; i < 16; i++)
            {
                s_list.Add(new List<string>() { });
                for (var j = 0; j < 12; j++)
                {
                    s_list[i].Add(i.ToString());
                }
            }
            superTable.init_list(ref s_list, ref r_s_list, ref c_s_list, START_X, START_Y);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //绘制表格
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;  //图片柔顺模式选择
            g.CompositingQuality = CompositingQuality.HighQuality;//再加一点
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;//高质量
            g.CompositingMode = CompositingMode.SourceOver;

            g.Clear(this.BackColor);
            //g.Clear(Color.Transparent);
            superTable.paint_table(g, e);
            superTable.paint_lines(g, e);
            superTable.paint_cells(g, e);
            base.OnPaint(e);
        }
    }
}
