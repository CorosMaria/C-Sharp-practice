using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osciloscop_frecventa__translatie_si_amplitudine_modificabile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public System.Drawing.Graphics desen;
        public osciloscop osciloscop1;
        int pozx = 40, pozy = 80, n_maxx = 300, n_maxy = 200;
        double alfa = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Array.Resize(ref valori, n_maxx + 1);
            desen = this.CreateGraphics();
            osciloscop1 = new osciloscop(desen, pozx, pozy, n_maxx, n_maxy);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.trackBar1.Maximum = n_maxy / 5;
            this.trackBar1.Minimum = -n_maxy / 5;
            this.trackBar1.Value = 0;
            this.trackBar2.Maximum = n_maxy;
            this.trackBar2.Value = n_maxy / 3;
            this.trackBar3.Minimum = 1;
            this.trackBar3.Value = 10;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fi = fi + 0.3;
            alfa = fi;
            int transl = -this.trackBar1.Value;
            int amplif = this.trackBar2.Value;
            int fr = this.trackBar3.Value;
            int zero = n_maxy / 2;
            for (int i = 1; i <= n_maxx; i++)
            {
                alfa += System.Convert.ToDouble(fr) / 100;
                int f = System.Convert.ToInt32(transl + zero - amplif * Math.Sin(alfa));
                if ((f < n_maxy) && (f >= 0))
                    valori[i] = f;
                if (f > n_maxy)
                    valori[i] = n_maxy - 1;
                if (f < 0)
                    valori[i] = 0;
            }

            osciloscop1.setval(valori, n_maxx, amplif, fr);
        }

        double fi = 0;
        static int[] valori = new int[0];
        public class osciloscop
        {
            int x0;
            int y0;
            int w;
            int h;
            int val_max, val, val_v;
            int nr_max;
            System.Drawing.Graphics zona_des;
            System.Drawing.Pen creion_r = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Font font_ni = new System.Drawing.Font("Nina", 8);
            System.Drawing.SolidBrush pens_blu = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.SolidBrush radiera = new System.Drawing.SolidBrush(System.Drawing.Color.White);

            System.Drawing.Bitmap img;
            System.Drawing.Bitmap ims;

            public void setval(int[] vals, int nrv, int ampl, int fr)
            {
                img = new Bitmap(nr_max, val_max, zona_des);
                int val, i, j;

                // afisare grafic sub forma de puncte

                /*
                for (i = 0; i < w; i++)
                {
                    val = System.Convert.ToInt16(System.Convert.ToDouble(vals[i]) * (System.Convert.ToDouble(h) / System.Convert.ToDouble(val_max))); //scalare
                    img.SetPixel(i, val, System.Drawing.Color.Red);
                    if (val < h - 1)
                        img.SetPixel(i, val + 1, System.Drawing.Color.Red);
                }
              
                zona_des.DrawImage(ims, x0, y0);
                zona_des.DrawImage(img, x0, y0);
              
                */

                // afisare grafic sub forma de linii

                zona_des.DrawImage(ims, x0, y0);
                val_v = System.Convert.ToInt16(System.Convert.ToDouble(vals[1]) * (System.Convert.ToDouble(h) / System.Convert.ToDouble(val_max)));
                for (i = 1; i < w - 1; i++)
                {
                    val = System.Convert.ToInt16(System.Convert.ToDouble(vals[i]) * (System.Convert.ToDouble(h) / System.Convert.ToDouble(val_max))); //scalare
                    zona_des.DrawLine(creion_r, x0 + i, y0 + val_v, x0 + i + 1, y0 + val);
                    val_v = val;
                }
                zona_des.FillRectangle(radiera, x0, y0 + h, w + 20, 20);
                for (i = 0; i <= w; i += 50)
                {
                    val = System.Convert.ToInt16(System.Convert.ToDouble(i * fr / 30) * (System.Convert.ToDouble(nr_max) / System.Convert.ToDouble(w))); //scalare
                    zona_des.DrawString(val.ToString(), font_ni, pens_blu, x0 + i, y0 + h);
                }
                zona_des.FillRectangle(radiera, x0 - 20, y0 - 10, 20, h + 20);


                for (i = 0; i <= h; i += 50)
                {
                    val = System.Convert.ToInt16(System.Convert.ToDouble(i * ampl / 100) * (System.Convert.ToDouble(val_max) / System.Convert.ToDouble(h))); //scalare
                    zona_des.DrawString(val.ToString(), font_ni, pens_blu, x0 - 20, y0 + h - i - 10);
                }

            }
            public osciloscop(System.Drawing.Graphics desen, int pozx, int pozy, int n_maxx, int n_maxy)
            {
                x0 = pozx;
                y0 = pozy;
                w = n_maxx;
                h = n_maxy;
                nr_max = n_maxx;
                val_max = n_maxy;
                zona_des = desen;
                int i, j;
                img = new Bitmap(nr_max, n_maxy, zona_des);
                ims = new Bitmap(nr_max, n_maxy, zona_des);
                // sterg imaginea

                for (j = 0; j < val_max; j++)
                {
                    for (i = 0; i < nr_max; i++)
                    {
                        ims.SetPixel(i, j, System.Drawing.Color.WhiteSmoke);
                    }
                }
                // grid
                for (j = 0; j < val_max; j++)
                {

                    // grid orizontal


                    if (j % 10 == 0)
                    {
                        for (i = 0; i < nr_max; i++)
                        {
                            if (j % 50 == 0)
                                ims.SetPixel(i, j, System.Drawing.Color.Gray);
                            else
                                ims.SetPixel(i, j, System.Drawing.Color.LightGray);
                        }
                    }
                    else
                    {

                        // grid orizontal vertical

                        for (i = 0; i < nr_max; i++)
                        {
                            if (i % 10 == 0)
                            {
                                if (i % 50 == 0)
                                    ims.SetPixel(i, j, System.Drawing.Color.Gray);
                                else
                                    ims.SetPixel(i, j, System.Drawing.Color.LightGray);
                            }
                        }
                    }
                }

                //chenar

                for (i = 0; i < n_maxx; i++)
                {
                    ims.SetPixel(i, 0, System.Drawing.Color.Blue);
                    ims.SetPixel(i, val_max - 1, System.Drawing.Color.Blue);
                }
                for (j = 0; j < val_max; j++)
                {
                    ims.SetPixel(0, j, System.Drawing.Color.Blue);
                    ims.SetPixel(nr_max - 1, j, System.Drawing.Color.Blue);
                }

            }

        }
    }
}
