using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrkmTecvizHesabi
{
    public partial class Form1 : Form
    {
        //Form Sürükleme Genel Tanımlar
        bool dragging;
        Point offset;

        public Form1()
        {
            InitializeComponent();
            cmbYontem.SelectedIndex = 0;
        }

        //Değişken Tanımları
        double tecviz = 0;
        double fark = 0;
        double farkmin = 0;
        double farkmax = 0;
        double olcek = 0;
        double tapu = 0;
        double hesap = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            //Eğer Texbox Boş ise 0 ile doldur
            if (txtTapu.Text.Trim() == "")
            {txtTapu.Text = "0";}
            if (txtHesap.Text.Trim() == "")
            {txtHesap.Text = "0";}

            //Virgül yerine nokta atama
            txtTapu.Text = txtTapu.Text.Replace(",", ".");
            txtHesap.Text = txtHesap.Text.Replace(",", ".");

            //Hesap Kısmı
            string selectedcmbYontem = cmbYontem.SelectedItem.ToString();
            tapu = Convert.ToDouble(txtTapu.Text);
            olcek = Convert.ToDouble(txtOlcek.Text);
            hesap = Convert.ToDouble(txtHesap.Text);
            if (selectedcmbYontem == "Sayısal ( Yapılaşmış )")
            {
                tecviz = 0.013 * Math.Sqrt(olcek * tapu) + 0.0003 * tapu;
            }
            if (selectedcmbYontem == "Planimetrik ( Yapılaşmamış )")
            {
                tecviz = 0.0004*olcek * Math.Sqrt(tapu) + 0.0003 * tapu;
            }

            fark = tapu - hesap;
            fark = Math.Round(fark, 2);
            tecviz = Math.Round(tecviz, 2);
            farkmin = tapu - tecviz;
            farkmax = tapu + tecviz;
            lblAlanFarki.Text = fark.ToString() + " m²";
            lblTecviz.Text = tecviz.ToString() + " m²";
            lblMaxMin.Text = farkmin.ToString() + " ≤ " + "ALAN" + " ≤ " + farkmax.ToString();

        }

        //Sadece Rakam Girilmesi
        private void txtOlcek_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //Rakam + Virgül + Nokta Girilebilir
        private void txtTapu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            offset = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new
                Point(currentScreenPos.X - offset.X,
                currentScreenPos.Y - offset.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pBoxblogger_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://gorkemtabak.blogspot.com");
        }

        private void pBoxgithub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/grkm");
        }
    }
}
