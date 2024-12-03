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
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            label11.ForeColor = Color.DarkRed;
            label11.Text=Form1.adi+" " + Form1.soyadi;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik No 11 Karakter olmalı!");
            radioButton1.Checked = true;
            textBox2.CharacterCasing= CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            Kullanicilari_goster();

            //Personel İşlemleri

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width=100;
            pictureBox2.Height=100;
            pictureBox2.BorderStyle=BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000";
            maskedTextBox2.Mask = "LL????????????????????";
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("İlköğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");

            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Memur");
            comboBox2.Items.Add("Şoför");
            comboBox2.Items.Add("İşçi");

            comboBox3.Items.Add("Arge");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Paketleme");
            comboBox3.Items.Add("Nakliye");

            DateTime zaman = DateTime.Now;
            int yil= int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1975, 1, 1);
            dateTimePicker1.MaxDate = new DateTime (yil - 18, ay,gun);
            dateTimePicker1.Format=DateTimePickerFormat.Short;

            radioButton3.Checked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 karakter olmalı!");
            else errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
             if(((int)e.KeyChar>=48 && (int)e.KeyChar<=57) || (int)e.KeyChar==8 )
                e.Handled = false;
             else e.Handled = true; 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else e.Handled = true;
        }



        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else e.Handled = true;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 8)

                errorProvider1.SetError(textBox4, "Kullanıcı adı 8 karakter olmalı");
            else errorProvider1.Clear();
                        
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
                e.Handled = false;
            else e.Handled = true;
        }
        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            //Regex kütüphanesi ingilizce karakterleri baz aldığından, Türkçe karakterli ingilizce karakterlerine dönüştürmeliyiz.
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre= sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');

            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                    textBox5.Text = sifre;
                MessageBox.Show("Parolanızdaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür.");
            }
















        }
    }
    
}
