using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XsiO
{
    public partial class Reguli : Form
    {
        bool mousedown;
        System.Drawing.Point Punct;
        public Reguli()
        {
            InitializeComponent();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
            Punct = e.Location;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                this.Location = new Point(this.Location.X - Punct.X + e.X, this.Location.Y - Punct.Y + e.Y);
                this.Update();
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
