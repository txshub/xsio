using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace XsiO
{
    public partial class Form1 : Form
    {
        // VARIABILE


        int Runda, scorx = 0, scoro = 0;
        int[,] T = new int[3,3];
        bool mousedown;
        System.Drawing.Point Punct;


        // .............................................................................

        // METODE


        // TRANSPUNEREA INFORMATIILOR IN MATRICE
        int Trans_Buton(Button B)
        {
            if (B.Text == "X") return 1;
            if (B.Text == "O") return 10;
            return 0;
        }
        void Trans_Matrice()
        {
            foreach (Button B in Panel.Controls)
            {
                switch (B.TabIndex)
                {
                    case 1:
                        T[0, 0] = Trans_Buton(B);
                        break;
                    case 2:
                        T[0, 1] = Trans_Buton(B);
                        break;
                    case 3:
                        T[0, 2] = Trans_Buton(B);
                        break;
                    case 4:
                        T[1, 0] = Trans_Buton(B);
                        break;
                    case 5:
                        T[1, 1] = Trans_Buton(B);
                        break;
                    case 6:
                        T[1, 2] = Trans_Buton(B);
                        break;
                    case 7:
                        T[2, 0] = Trans_Buton(B);
                        break;
                    case 8:
                        T[2, 1] = Trans_Buton(B);
                        break;
                    case 9:
                        T[2, 2] = Trans_Buton(B);
                        break;
                }
            }
        }

        // RESETAREA VALORILOR PENTRU INCEPEREA UNUI NOU JOC
        void Reset()
        {
            Runda = 1;
            foreach (Button B in Panel.Controls)
            {
                B.Enabled = true;
                B.Text = "";
            }
        }

        // AFISAREA RUNDEI
        void Afis_Runda()
        {
            if (Runda % 2 == 1)
                Runda_label.Text = "Jucatorul X";
            else
                Runda_label.Text = "Jucatorul O";
        }

        // AFISAREA UNEI CASUTE
        void Afis_Mutare(Button B)
        {
            if (Runda % 2 == 1)
                B.Text = "X";
            else
                B.Text = "O";
            Runda++;
        }

        // VERIFICARE
        bool Linii(int x)
        {
            for (int i = 0; i < 3; i++)
                if (T[i, 0] + T[i, 1] + T[i, 2] == x)
                    return true;
            return false;
        }
        bool Coloane(int x)
        {
            for (int i = 0; i < 3; i++)
                if (T[0, i] + T[1, i] + T[2, i] == x)
                    return true;
            return false;
        }
        bool Diagonale(int x)
        {
            if ((T[0, 0] + T[1, 1] + T[2, 2] == x) || (T[0, 2] + T[1, 1] + T[2, 0] == x))
                return true;
            return false;
        }
        bool Terminat(int x)
        {
            return (Linii(x) || Coloane(x) || Diagonale(x));
        }
        bool Remiza()
        {
            return (Runda == 10);
        }

        //  BLOCAREA BUTOANELOR
        void Block()
        {
            foreach (Button B in Panel.Controls)
                B.Enabled = false;
        }


        // .............................................................................
        
        // HANDLERE


        // CONSTRUCTOR
        public Form1()
        {
            InitializeComponent();
            Block();
        }

        // CUSTOMIZARE FUNDAL
        private void Fundal_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem c = (ToolStripMenuItem)sender;
            this.BackColor = c.BackColor;
        }

        // CUSTOMIZARE BUTOANE
        private void Butoane_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem c = (ToolStripMenuItem)sender;
            foreach (Button B in Panel.Controls)
            {
                B.BackColor = c.BackColor;
            }
        }

        // BUTONUL AJUTOR
        private void Ajutor_Click(object sender, EventArgs e)
        {
            Process.Start(@"Informatii de utilizare.docx");
        }

        // BUTONUL DE IESIRE
        private void Iesire_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // AFISAREA REGULILOR JOCULUI
        private void Reguli_Click(object sender, EventArgs e)
        {
            Reguli reguli = new Reguli();
            reguli.Show();
        }

        // APASAREA PE UN BUTON
        private void Buton_Click(object sender, EventArgs e)
        {
            Button B = (Button)sender;
            Afis_Mutare(B);
            Afis_Runda();
            B.Enabled = false;
            Trans_Matrice();
            if (Terminat(3))
            {
                Block();
                scorx++;
                ScorX.Text = scorx.ToString();
                MessageBox.Show("Jucatorul X a castigat!");
            }
            else if (Terminat(30))
            {
                Block();
                scoro++;
                ScorO.Text = scoro.ToString();
                MessageBox.Show("Jucatorul O a castigat!");
            }
            else if (Remiza())
            {
                Block();
                MessageBox.Show("Este remiza!");
            }
        }

        // JOC NOU J1 VS J2
        private void JocNou_Click(object sender, EventArgs e)
        {
            Reset();
            Afis_Runda();
            foreach (Button B in Panel.Controls)
                B.Enabled = true;
        }

        // RESETAREA SCORULUI
        private void Reseteaza_Click(object sender, EventArgs e)
        {
            scorx = 0;
            scoro = 0;
            ScorX.Text = "0";
            ScorO.Text = "0";
        }

        // MUTAREA FERESTREI
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

        
    }
}
