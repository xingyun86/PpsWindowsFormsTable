using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ppsyqm
{    class SuperTableForm: Form
    {
        Bitmap gBitmap = null;
        Graphics gGraphics = null;
        SuperTable superTable = new SuperTable();
        List<EntityObject> dataList = new List<EntityObject>();
        public SuperTableForm(int ROW_NUM = 20, int COL_NUM = 13, int ROW_WIDTH = 36, int COL_HEIGHT = 36, int START_X = 10, int START_Y = 10)
        {
            InitForm(ROW_NUM, COL_NUM, ROW_WIDTH, COL_HEIGHT, START_X, START_Y);
        }
        public void InitForm(int ROW_NUM = 20, int COL_NUM = 13, int ROW_WIDTH = 36, int COL_HEIGHT = 36, int START_X = 10, int START_Y = 10)
        {
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.Dock = DockStyle.None;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopLevel = false;
            this.Size = new System.Drawing.Size(ROW_NUM * ROW_WIDTH + START_X + 1, COL_NUM * COL_HEIGHT+ START_Y + 1);

            List<int> r_s_list = new List<int>();
            List<int> c_s_list = new List<int>(); 
            List<string> h_list = new List<string>();
            List<List<string>> s_list = new List<List<string>>();
            for (var i = 0; i < ROW_NUM; i++)
            {
                r_s_list.Add(ROW_WIDTH);
            }
            for (var j = 0; j < COL_NUM; j++)
            {
                c_s_list.Add(COL_HEIGHT);
            }
            for (var col = 0; col < 16; col++)
            {
                s_list.Add(new List<string>() { });
                for (var row = 0; row < 12; row++)
                {
                    s_list[col].Add(row.ToString());
                }
            }
            for (var col = 0; col < 16; col++)
            {
                h_list.Add((col + 1).ToString() + "号");
            }
            superTable.init_list(ref h_list, ref s_list, ref r_s_list, ref c_s_list, START_X, START_Y);

            gBitmap = new Bitmap(Width, Height);
            gGraphics = Graphics.FromImage(gBitmap);
            RenderMemory(gGraphics);

            this.Show();
        }
        void RenderMemory(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;  //图片柔顺模式选择
            g.CompositingQuality = CompositingQuality.HighQuality;//再加一点
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;//高质量
            g.CompositingMode = CompositingMode.SourceOver;

            g.Clear(this.BackColor);
            //gGraphics.Clear(Color.Transparent);
            superTable.paint_table(g);
            superTable.paint_lines(g);
            superTable.paint_cells(g);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //绘制表格
            if (gGraphics != null && gBitmap != null)
            {
                e.Graphics.DrawImage(gBitmap, 0, 0);
            }
            //base.OnPaint(e);
        }
    }
}
