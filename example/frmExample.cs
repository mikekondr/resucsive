using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example
{
    public partial class frmExample : Form
    {
        private bool flag = false;

        public frmExample()
        {
            InitializeComponent();
        }

        private void MyDraw(Graphics g, int N, int x, int y)
        {
            flag = true;
            timer1.Enabled = true;
            while (flag)
                Application.DoEvents();
            timer1.Enabled = false;

            if (N == 0)
                return;
            else
            {
                g.DrawRectangle(new Pen(Brushes.Blue, 2),
                    0, 0, x, y);
                x += 50;
                y += 50;
                N--;
                MyDraw(g, N, x, y);
            }
        }

        private void frmExample_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            MyDraw(g, 7, 50, 50);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            flag = false;
        }
    }
}
