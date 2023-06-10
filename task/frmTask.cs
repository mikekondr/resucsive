using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace task
{
    public partial class frmTask : Form
    {
        private bool timerFlag = false;
        private bool isRunning = false;

        public frmTask()
        {
            InitializeComponent();
        }

        private void btnTask7_Click(object sender, System.EventArgs e)
        {
            if (isRunning)
            {
                MessageBox.Show("Зачекайте завершення поточного виводу малюнка...", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Graphics g = panel1.CreateGraphics();
            g.Clear(BackColor);
            int count = 0;

            while (true)
            {
                string str = Interaction.InputBox("Введіть кількість елементів:", "Рекурсивне малювання", "8");
                if (str == "")
                    return;

                if (!Int32.TryParse(str, out count) || count <= 0)
                    MessageBox.Show("Введіть дійсне, додатнє число!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    break;
            }

            // width / 2 = X center
            // height / 2 = Y center
            // min(X, Y) / 2 - R guide radius
            int X = panel1.Width / 2;
            int Y = panel1.Height / 2;
            int R = Math.Min(X, Y) / 2;
            //  max radius = l/2 = (2*(pi/count)*R)/2
            //  step radius = max radius /count
            int dr = (int)(2 * (Math.PI/(double)count) * R / 2.0) / count;

            if (dr == 0)
            {
                MessageBox.Show("За такої кількості елементів, найменше коло стає занадто малим (r < 1).\n" +
                    "Вкажіть меншу кількість елементів.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            timer1.Interval = 3000 / count;
            isRunning = true;
            draw7(g, 1, count, X, Y, R, dr);
        }

        private void draw7(Graphics g, int N, int count, int X, int Y, int R, int dr)
        {
            pause();

            // step = 2pi/count
            int x = X + (int)(R * Math.Cos(2 * Math.PI / count * N));
            int y = Y + (int)(R * Math.Sin(2 * Math.PI / count * N));
            int r = (count - N + 1) * dr;

            g.DrawEllipse(new Pen(Brushes.Blue), new Rectangle(x-r, y-r, r*2, r*2));
            if (N == count)
            {
                isRunning = false;
                return;
            }
            else
            {
                N++;
                draw7(g, N, count, X, Y, R, dr);
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            timerFlag = false;
        }

        private void pause()
        {
            timerFlag = true;
            timer1.Enabled = true;
            while (timerFlag)
                Application.DoEvents();
            timer1.Enabled = false;
        }
    }
}
