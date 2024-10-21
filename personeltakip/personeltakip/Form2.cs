using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;//Regex kütüphane -- Güvenli Parola

//Giriş Çıkış kütüp.
using System.IO;
using System.Linq.Expressions;//input--output

namespace personeltakip
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        readonly OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.16.0;Data Source=personel.accdb");


        private void Kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select tcno as [TC KİMLİK NO], ad AS[ADI],soyad AS [SOYADI], yetki as [YETKİ],kullaniciadi as [KULLANICI ADI],parola as [PAROLA] from kullanicilar Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message,"SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close() ;
               
            }
            }

        private void Personelleri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter(" select tcno as [TC KİMLİK NO],ad as [AD],soyad as [SOYAD],cinsiyet,dogumtarihi,gorevi,gorevyeri,maasi from personeller", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();


            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();


            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Kullanicilari_goster();
            Personelleri_goster();
            pictureBox1.Height = 150;
            pictureBox1.Height = 150;
            pictureBox1.SizeMode=PictureBoxSizeMode.StretchImage;

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");
            }
            catch 
            {
                Image ımage = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");
                pictureBox1.Image = ımage;


            }

        }
    }
    
}
