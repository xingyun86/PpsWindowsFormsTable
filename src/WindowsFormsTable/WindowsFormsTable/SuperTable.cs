using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ppsyqm
{
    class SuperTable
    {
        Point start_point = new Point(10, 10);
        public int line_width;
        public Color bg_color;
        public Color line_color;
        public List<int> row_size_list = new List<int>();
        public List<int> col_size_list = new List<int>();
        public List<List<EntityObject>> data_list = new List<List<EntityObject>>();

        public void init_list(ref List<List<string>> dataList, ref List<int> rowSizeList, ref List<int> colSizeList, int startPointX = 0, int startPointY = 0)
        {
            row_size_list = rowSizeList;
            col_size_list = colSizeList;
            for (var row = 0; row < dataList.Count; row++)
            {
                data_list.Add(new List<EntityObject>() { });
                for (var col = 0; col < dataList[row].Count; col++)
                {
                    data_list[row].Add(new EntityObject() { });
                    data_list[row][col].value = dataList[row][col];
                }
            }
            start_point.X = startPointX;
            start_point.Y = startPointY;
        }

        public void init_list(ref List<List<string>> dataList, ref List<int> rowSizeList, ref List<int> colSizeList, Point startPoint)
        {
            init_list(ref dataList, ref rowSizeList, ref colSizeList, startPoint.X, startPoint.Y);
        }

        public void paint_table(Graphics graphics, System.Windows.Forms.PaintEventArgs e)
        {
            //绘制表格
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;  //图片柔顺模式选择
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;//再加一点
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;//高质量

            //设置画笔颜色和Pen_Size=3
            Pen drawPen = new Pen(Color.DarkRed, 1);
            Font drawFont = new Font("Arial", 7);
            // Create point for upper-left corner of drawing.
            SolidBrush drawBrush = new SolidBrush(line_color);

            //横向
            var YH = 0;
            for (var y = 0; y < col_size_list.Count; y += 1)
            {
                YH += col_size_list[y];
            }
            var xW = start_point.X;
            for (var x = 0; x < row_size_list.Count; x += 1)
            {
                if(x == 0)
                {
                    graphics.DrawLine(drawPen, xW, start_point.Y, xW, YH + start_point.Y);
                }
                xW += row_size_list[x];
                graphics.DrawLine(drawPen, xW, start_point.Y, xW, YH + start_point.Y);
            }
            //纵向
            var XW = 0;
            for (var x = 0; x < row_size_list.Count; x += 1)
            {
                XW += row_size_list[x];
            }
            var yH = start_point.Y;
            for (var y = 0; y < col_size_list.Count; y += 1)
            {
                if(y == 0)
                {
                    graphics.DrawLine(drawPen, start_point.X, yH, XW + start_point.X, yH);
                }
                yH += col_size_list[y];
                graphics.DrawLine(drawPen, start_point.X, yH, XW + start_point.X, yH);
            }
        }
        private Point Logical2Physics(int xL, int yL)
        {
            Point ptP = start_point;
            for (var i = 0; i < xL; i++)
            {
                ptP.X += row_size_list[i];
            }
            for (var i = 0; i < yL; i++)
            {
                ptP.Y += col_size_list[i];
            }
            return ptP;
        }
        private Point Logical2Physics(Point ptL)
        {
            return Logical2Physics(ptL.X,ptL.Y);
        }
        private void DrawLineWithPoints(Graphics graphics, Pen drawLinePen, int x1, int y1, int x2, int y2)
        {
            Point ptPhysics1 = Logical2Physics(x1,y1);
            int nRowWidth1 = row_size_list[x1];
            int nColWidth1 = col_size_list[y1];
            Point ptPhysics2 = Logical2Physics(x2,y2);
            int nRowWidth2 = row_size_list[x2];
            int nColWidth2 = col_size_list[y2];
            // (x1,y1) -> (x2,y2)连线
            graphics.DrawLine(drawLinePen, ptPhysics1.X + nRowWidth1 / 2, ptPhysics1.Y + nColWidth1 / 2, ptPhysics2.X + nRowWidth2 / 2, ptPhysics2.Y + nColWidth2 / 2);
        }
        private void DrawLineWithPoints(Graphics graphics, Pen drawLinePen, Point pt1, Point pt2)
        {
            DrawLineWithPoints(graphics, drawLinePen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }
        public void paint_cells(Graphics graphics, System.Windows.Forms.PaintEventArgs e)
        {
            //设置画笔颜色和Pen_Size=3
            Font drawFont = new Font("Arial", 8);
            // Create point for upper-left corner of drawing.
            SolidBrush drawBrush = new SolidBrush(Color.DarkSlateBlue);
            for (var x = 0; x < data_list.Count && x < row_size_list.Count; x += 1)
            {
                for (var y = 0; y < data_list[x].Count && y < col_size_list.Count; y += 1)
                {
                    string value = data_list[x][y].value;
                    Point ptPhysicsXY = Logical2Physics(x, y);
                    int nRowWidthXY = row_size_list[x];
                    int nColWidthXY = col_size_list[y];

                    Pen drawEllipsePen = new Pen(Color.DarkGreen, 2);
                    SolidBrush drawEllipseBrush = new SolidBrush(Color.LightBlue);
                    var size = graphics.MeasureString(value, drawFont);
                    graphics.DrawEllipse(drawEllipsePen, ptPhysicsXY.X + 6, ptPhysicsXY.Y + 6, nRowWidthXY - 12, nColWidthXY - 12);
                    graphics.FillEllipse(drawEllipseBrush, ptPhysicsXY.X + 6, ptPhysicsXY.Y + 6, nRowWidthXY - 12, nColWidthXY - 12);
                    graphics.DrawString(value, drawFont, drawBrush, ptPhysicsXY.X + (nRowWidthXY - size.Width) / 2, ptPhysicsXY.Y + (nColWidthXY - size.Height) / 2);
                }
            }
        }
        public void paint_lines(Graphics graphics, System.Windows.Forms.PaintEventArgs e)
        {
            //画折线
            Pen drawLinePen = new Pen(Color.Red, 2);
            Random randNum = new Random();
            int[] randNumList = new int[data_list.First().Count];
            for (var i = 0; i < data_list.First().Count; i++)
            {
                randNumList[i] = randNum.Next(0, 9);
            }
            for (var i = 0; i < randNumList.Count() - 1; i++)
            {
                DrawLineWithPoints(graphics, drawLinePen, randNumList[i], i, randNumList[i + 1], i + 1);
            }
        }
    }
}
