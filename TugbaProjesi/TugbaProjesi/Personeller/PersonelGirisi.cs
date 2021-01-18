﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TugbaProjesi
{
    public partial class PersonelGirisi : Form
    {
        public PersonelGirisi()
        {
            InitializeComponent();
        }
        private const string ConnectionString = @"Data Source = DESKTOP-LCEGS46; Initial Catalog = TProje; Integrated Security = True";
        SqlConnection baglanti = new SqlConnection(ConnectionString);
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                try
                {
                    baglanti.Open();
                    string sql = "Select * from personnel where personelAdi=@pAdi AND personelSifre=@pSifre";
                    SqlParameter prm1 = new SqlParameter("pAdi", textBox1.Text.Trim());
                    SqlParameter prm2 = new SqlParameter("pSifre", textBox2.Text.Trim());
                    SqlCommand komut = new SqlCommand(sql, baglanti);
                    komut.Parameters.Add(prm1);
                    komut.Parameters.Add(prm2);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        PersonelArayuz personelArayuz= new PersonelArayuz();
                        personelArayuz.Show();
                    }
                    baglanti.Close();
                }

                catch (Exception)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı !!!");
                }
            }
            else
            {
                MessageBox.Show("Yukardaki alanlar boş bırakılamaz.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
                checkBox1.Text = "Gizle";
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                textBox2.UseSystemPasswordChar = true;
                checkBox1.Text = "Göster";
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KullaniciSifreDegis sifreDegis = new KullaniciSifreDegis();
            sifreDegis.Show();
        }
    }
}
