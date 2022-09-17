using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voltmetru_Tensiune__Curent__Putere
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        System.Drawing.Graphics Desen;
        Voltmetru Voltmetru_tensiune;
        Voltmetru Voltmetru_curent;
        Voltmetru Voltmetru_putere;
        
        public class Voltmetru
        {
            int x0;
            int y0;
            int Latime;
            double vm;
            System.Drawing.Pen Creion_rosu = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Font font_ni = new System.Drawing.Font("Nina", 8);
            System.Drawing.SolidBrush Pensula_albastra = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);

            public void Deseneaza_voltmetru(System.Drawing.Graphics Zona_desenare)
            {
                int lt = 15;
                int lg = 22;
                int x1, x2, xt, y1, y2, yt;
                int xc = x0 + Latime / 2;
                int yc = y0 + Latime / 2;
                int raza = Latime / 2;
                int nrd;
                int val_a = 0;

                // alfa_gr unghiul in grade
                double Unghi_alfa_grade = 140;
                nrd = 0;
                while (Unghi_alfa_grade >= 40)
                {
                    double Unghi_alfa_radiani = 2 * System.Math.PI * (Unghi_alfa_grade) / 360;// unghiul in radiani
                    if (nrd % 5 == 0)
                    {
                        x1 = System.Convert.ToInt16(xc + (raza - lt) * System.Math.Cos(Unghi_alfa_radiani));
                        y1 = System.Convert.ToInt16(yc - (raza - lt) * System.Math.Sin(Unghi_alfa_radiani));
                        xt = System.Convert.ToInt16(xc - 5 + raza * System.Math.Cos(Unghi_alfa_radiani));
                        yt = System.Convert.ToInt16(yc - raza * System.Math.Sin(Unghi_alfa_radiani));
                        Zona_desenare.DrawString(System.Convert.ToString(val_a), font_ni, Pensula_albastra, xt, yt);
                        val_a = val_a + System.Convert.ToInt16(vm / 10);

                    }
                    else
                    {
                        x1 = System.Convert.ToInt16(xc + (raza - lg) * System.Math.Cos(Unghi_alfa_radiani));
                        y1 = System.Convert.ToInt16(yc - (raza - lg) * System.Math.Sin(Unghi_alfa_radiani));
                    }
                    x2 = System.Convert.ToInt16(xc + (raza - 2 * lt) * System.Math.Cos(Unghi_alfa_radiani));
                    y2 = System.Convert.ToInt16(yc - (raza - 2 * lt) * System.Math.Sin(Unghi_alfa_radiani));
                    Zona_desenare.DrawLine(Creion_rosu, x1, y1, x2, y2);
                    Unghi_alfa_grade -= 2;
                    nrd++;
                }
                Zona_desenare.DrawRectangle(Creion_rosu, xc - raza, yc - raza - 2, 2 * raza, 5 * raza / 4);
            }
            public void setval(System.Drawing.Graphics zona_des, double val)
            {
                int Unghi_grade = 140 - System.Convert.ToInt16(100 * val / vm); ;//unghiul in grade

                int lg = 17;
                int xc = x0 + Latime / 2;
                int yc = y0 + Latime / 2;
                int raza = Latime / 2;
                System.Drawing.SolidBrush radiera = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                zona_des.FillPie(radiera, x0 + 2 * lg - 1, y0 + 2 * lg - 1, Latime - 4 * lg + 2, Latime - 4 * lg + 2, 10, -180);
                double Unghi_radiani = 2 * System.Math.PI * (Unghi_grade) / 360;// unghiul in radiani
                int x = System.Convert.ToInt16(xc + (raza - 2 * lg) * System.Math.Cos(Unghi_radiani));
                int y = System.Convert.ToInt16(yc - (raza - 2 * lg) * System.Math.Sin(Unghi_radiani));
                zona_des.DrawLine(Creion_rosu, x, y, xc, yc);
                Unghi_grade = 40;
                zona_des.DrawRectangle(Creion_rosu, xc - raza, yc - raza - 2, 2 * raza, 5 * raza / 4);

            }

            public Voltmetru(int pozx, int pozy, int lat, double val_max)
            {
                x0 = pozx;
                y0 = pozy;
                Latime = lat;
                vm = val_max;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Desen = this.CreateGraphics();
           
            Voltmetru_tensiune.Deseneaza_voltmetru(Desen);
           
            Voltmetru_curent.Deseneaza_voltmetru(Desen);
            
            Voltmetru_putere.Deseneaza_voltmetru(Desen);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Voltmetru_tensiune = new Voltmetru(50,50, 300, 250);
            Voltmetru_curent = new Voltmetru(370, 50, 300, 20);
            Voltmetru_putere = new Voltmetru(690, 50, 300, 5000);
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            int Valoare_tensiune = this.trackBar1.Value;
            Voltmetru_tensiune.setval(Desen, Valoare_tensiune);

            int Valoare_curent = this.trackBar2.Value;
            Voltmetru_curent.setval(Desen, Valoare_curent);

            Voltmetru_putere.setval(Desen, Valoare_curent * Valoare_tensiune);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int Valoare_tensiune = this.trackBar1.Value;
            Voltmetru_tensiune.setval(Desen, Valoare_tensiune);

            int Valoare_curent = this.trackBar2.Value;
            Voltmetru_curent.setval(Desen, Valoare_curent);

            Voltmetru_putere.setval(Desen, Valoare_curent * Valoare_tensiune);

        }
    }
}
