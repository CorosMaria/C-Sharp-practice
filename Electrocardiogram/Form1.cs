using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        System.Drawing.Graphics Desen;
        System.Drawing.Pen Creion_albastru;
        System.Drawing.Pen Radiera;
        System.Drawing.Pen Creion_rosu;
        int x0,y0,w,h,x,vv;

        private void Form1_Load(object sender, EventArgs e)
        {
            x0 = 100;
            x = 0;
            y0 = 10;
            w = 400;
            h = 300;
            
            Desen = this.CreateGraphics();
            
            Radiera = new System.Drawing.Pen(this.BackColor);
            Creion_albastru = new System.Drawing.Pen(System.Drawing.Color.DeepSkyBlue);
            Creion_rosu = new System.Drawing.Pen(System.Drawing.Color.Red);
            Desen.Clear(this.BackColor);
    }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Desen.DrawRectangle(Creion_albastru, 100, 10, 400,300);
            Desen.DrawLine(Creion_rosu, x + x0+1, y0, x + x0+1, y0 + h);
            Desen.DrawLine(Radiera, x + x0, y0, x + x0, y0 + h);
            //Desen.DrawLine(Creion_albastru, x0 + x, y0 + h, x0 + x, y0 + h - this.trackBar1.Value);
            Desen.DrawLine(Creion_albastru, x0+x-1,y0+h-vv,x+x0,y0+h-this.trackBar1.Value);
            vv = this.trackBar1.Value;
            x++;
            if (x >= w)
            {
                x = 0; 

            }
        }
        }
}
