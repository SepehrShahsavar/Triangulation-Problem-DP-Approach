using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangulation
{
    public partial class Form2 : Form
    {
        private int size;
        List<Point> points;
        int[,] adjMatrix;
        public Form2(int height , int width , List<Point> points , int[,] adj)
        {
            InitializeComponent();
            this.Height = height;
            this.Width = width;
            adjMatrix = adj;
            size = points.Count;
            this.points = points;
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Polygon polygon = new Polygon(size, adjMatrix, points);
            polygon.traingulate();

            Pen pen1 = new Pen(Color.FromArgb(50, 130, 184), 3);
            Pen pen2 = new Pen(Color.FromArgb(27, 38, 44) , 2);

            List<System.Drawing.Point> coordinates = new List<System.Drawing.Point>();
            points.ForEach(x => coordinates.Add(new System.Drawing.Point(x.X, x.Y)));
            g.DrawPolygon(pen1, coordinates.ToArray());

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(polygon.Diagonals[i,j] == 1)
                    {
                        Point p1 = points[i], p2 = points[j];
                        g.DrawLine(pen2, p1.X, p1.Y, p2.X, p2.Y);
                    }
                }
            }


            e.Dispose();
            MessageBox.Show("Cost is :" + polygon.TriangulationCost , "Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
