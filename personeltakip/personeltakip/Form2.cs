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
    }
    
}
