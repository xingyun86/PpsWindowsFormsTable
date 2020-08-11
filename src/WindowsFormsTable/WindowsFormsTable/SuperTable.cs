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
        List<EntityObject> head_list = new List<EntityObject>();
        public List<List<EntityObject>> data_list = new List<List<EntityObject>>();

        public void init_list(ref List<string> headList, ref List<List<string>> dataList, ref List<int> rowSizeList, ref List<int> colSizeList, int startPointX = 0, int startPointY = 0)
        {
            for(var i = 0; i < headList.Count; i++)
            {
                head_list.Add(new EntityObject() { });
                head_list[i].value = headList[i];
            }
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
            if (headList.Count > 0)
            {
                row_size_list.Insert(0, rowSizeList[0]);
                data_list.Insert(0, new List<EntityObject>() { });
                for (var row = 0; row < dataList[0].Count; row++)
                {
                    data_list[0].Add(new EntityObject() { });
                }
            }
            start_point.X = startPointX;
            start_point.Y = startPointY;
        }

        public void init_list(ref List<string> headList, ref List<List<string>> dataList, ref List<int> rowSizeList, ref List<int> colSizeList, Point startPoint)
        {
            init_list(ref headList, ref dataList, ref rowSizeList, ref colSizeList, startPoint.X, startPoint.Y);
        }

        public void paint_table(Graphics graphics)
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
                ptP.Y += row_size_list[i];
            }
            for (var i = 0; i < yL; i++)
            {
                ptP.X += col_size_list[i];
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
        public void paint_cells(Graphics graphics)
        {
            //设置画笔颜色和Pen_Size=3
            Font drawFont = new Font("Arial", 8);
            // Create point for upper-left corner of drawing.
            SolidBrush drawBrush = new SolidBrush(Color.DarkSlateBlue);
            var startPos = 0;
            if(head_list.Count > 0)
            {
                for (var col = 0; col < data_list[0].Count && col < head_list.Count; col += 1)
                {
                    string value = head_list[col].value;
                    Point ptPhysicsXY = Logical2Physics(0, col);
                    int nRowWidthXY = row_size_list[0];
                    int nColWidthXY = col_size_list[col];

                    Pen drawRectanglePen = new Pen(Color.DarkGreen, 2);
                    SolidBrush drawRectangleBrush = new SolidBrush(Color.CadetBlue);
                    var size = graphics.MeasureString(value, drawFont);
                    graphics.DrawRectangle(drawRectanglePen, ptPhysicsXY.X, ptPhysicsXY.Y , nRowWidthXY, nColWidthXY);
                    graphics.FillRectangle(drawRectangleBrush, ptPhysicsXY.X, ptPhysicsXY.Y, nRowWidthXY, nColWidthXY);
                    graphics.DrawString(value, drawFont, drawBrush, ptPhysicsXY.X + (nRowWidthXY - size.Width) / 2, ptPhysicsXY.Y + (nColWidthXY - size.Height) / 2);
                }
                startPos = 1;
            }
            for (var row = startPos; row < data_list.Count && row < row_size_list.Count; row += 1)
            {
                for (var col = 0; col < data_list[row].Count && col < col_size_list.Count; col += 1)
                {
                    string value = data_list[row][col].value;
                    Point ptPhysicsXY = Logical2Physics(row, col);
                    int nRowWidthXY = row_size_list[row];
                    int nColWidthXY = col_size_list[col];

                    Pen drawEllipsePen = new Pen(Color.DarkGreen, 2);
                    SolidBrush drawEllipseBrush = new SolidBrush(Color.LightBlue);
                    var size = graphics.MeasureString(value, drawFont);
                    graphics.DrawEllipse(drawEllipsePen, ptPhysicsXY.X + 6, ptPhysicsXY.Y + 6, nRowWidthXY - 6 - 6, nColWidthXY - 6 - 6);
                    graphics.FillEllipse(drawEllipseBrush, ptPhysicsXY.X + 6, ptPhysicsXY.Y + 6, nRowWidthXY - 6 - 6, nColWidthXY - 6 - 6);
                    graphics.DrawString(value, drawFont, drawBrush, ptPhysicsXY.X + (nRowWidthXY - size.Width) / 2, ptPhysicsXY.Y + (nColWidthXY - size.Height) / 2);
                }
            }
        }
        public void paint_lines(Graphics graphics)
        {
            var startValue = (head_list.Count > 0) ? 1 : 0;
            var countValue = (head_list.Count > 0) ? (data_list[0].Count + 1) : data_list[0].Count;
            //画折线
            Pen drawLinePen = new Pen(Color.Red, 2);
            Random randNum = new Random();
            int[] randNumList = new int[countValue];
            for (var row = 0; row < countValue; row++)
            {
                randNumList[row] = randNum.Next(startValue, countValue - 1);
            }
            for (var row = startValue; row < countValue - 1; row++)
            {
                DrawLineWithPoints(graphics, drawLinePen, row, randNumList[row], row + 1, randNumList[row + 1]);
            }
        }
    }
}
