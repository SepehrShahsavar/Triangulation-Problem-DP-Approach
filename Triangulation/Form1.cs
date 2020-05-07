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
    public partial class Form1 : Form
    {
        public List<Point> points;
        public Form1()
        {
            points = new List<Point>();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x =0 ,y =0;

            try
            {
                x = int.Parse(xCoordinate.Text);
                y = int.Parse(yCoordinate.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("INVALID INPUTS!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                xCoordinate.Text = "";
                yCoordinate.Text = "";
                return;
            }
            
            Point p = new Point(x , y);
            if (!points.Contains(p))
            {
                points.Add(p);
            }
            xCoordinate.Text = "";
            yCoordinate.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            points.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int[,] adj = createAdjMatrix();
            Form2 form2 = new Form2(500 , 500 , points , adj);
            form2.Show();
        }

        private int[,] createAdjMatrix()
        {
            int size = points.Count;
           int[,] adj = new int[size, size];
            for (int i = 0; i < size; i++)
            { 
                if(i+1 >= size)
                {
                    adj[i, 0] = 1;
                    adj[0, i] = 1;
                    continue;
                }
                adj[i, i + 1] = 1;
                adj[i + 1, i] = 1;
            }

            return adj;
        }
    }
}
