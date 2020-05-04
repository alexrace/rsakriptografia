using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    public partial class Form1 : Form
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;
        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(125, Color.Black);
            panel2.BackColor = Color.FromArgb(125, Color.Black);
            panel3.BackColor = Color.FromArgb(125, Color.Black);
            panel4.BackColor = Color.FromArgb(125, Color.Black);
            btn_exit.BackColor = Color.FromArgb(125, Color.White);
            button1.BackColor = Color.FromArgb(125, Color.White);
            button2.BackColor = Color.FromArgb(125, Color.White);
            btn_kodol.BackColor = Color.FromArgb(125, Color.White);
            label1.BackColor = Color.FromArgb(125, Color.White);
            label2.BackColor = Color.FromArgb(125, Color.White);
        }

        static public byte[] Encryption(byte [] data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedDAta;
                using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedDAta = RSA.Encrypt(data, DoOAEPPadding);
                }
                return encryptedDAta;
            }
            catch(CryptographicException e)
            {
                return null;
            }
        }

        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch(CryptographicException e)
            {
                return null;
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Btn_kodol_Click(object sender, EventArgs e)
        {
            plaintext = ByteConverter.GetBytes(richTextBox1.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            richTextBox2.Text = ByteConverter.GetString(encryptedtext);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            byte[] decrpytedtext = Decryption(encryptedtext, RSA.ExportParameters(true), false);
            richTextBox1.Text = ByteConverter.GetString(decrpytedtext);
        }
    }
}
