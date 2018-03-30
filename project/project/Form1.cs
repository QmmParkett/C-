using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Form1 : Form
    {
        private Bitmap inMap;
        private Bitmap outMap;
        private int w;
        private int h;
        private int[,] Dm; //Halftone matrix 2 x 2


        public Form1()
        {
            InitializeComponent();
            inMap = null;
            outMap = null;
            Dm = new int[2, 2];
            Dm[0, 0] = 0;
            Dm[0, 1] = 1;
            Dm[1, 0] = 3;
            Dm[1, 1] = 2;

        }
        private int Aproxim(int intensity)
        {
            if (intensity > 0 && intensity < 63)
                return 3;
            else
                if (intensity >= 63 && intensity < 127)
                return 2;
            else
                    if (intensity >= 127 && intensity < 191)
                return 1;
            else
                        if (intensity >= 191 && intensity < 255)
                return 0;
            else
                return 0;
        }


        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fname;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = openFileDialog1.FileName;
                inMap = new Bitmap(Image.FromFile(fname));
                w = inMap.Size.Width;
                h = inMap.Size.Height;
                pictureBox1.Size = new Size(w, h);
                pictureBox1.Image = inMap;


            }
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = inMap;
        }

        private void halftoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color p;
            outMap = new Bitmap(inMap);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    p = outMap.GetPixel(x, y);

                    //extract pixel component ARGB
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //find average
                    int avg = (r + g + b) / 3;

                    //set new pixel value
                    outMap.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                }

            }
            pictureBox1.Image = outMap;
        }
    }
}
