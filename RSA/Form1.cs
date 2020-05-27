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
       
        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(125, Color.Black);
            panel2.BackColor = Color.FromArgb(125, Color.Black);
            panel3.BackColor = Color.FromArgb(125, Color.Black);
            panel4.BackColor = Color.FromArgb(125, Color.Black);
            panel5.BackColor = Color.FromArgb(125, Color.Black);
            btn_exit.BackColor = Color.FromArgb(125, Color.White);
            button1.BackColor = Color.FromArgb(125, Color.White);
            button2.BackColor = Color.FromArgb(125, Color.White);
            btn_kodol.BackColor = Color.FromArgb(125, Color.White);
            label1.BackColor = Color.FromArgb(125, Color.White);
            label2.BackColor = Color.FromArgb(125, Color.White);
            label3.BackColor = Color.FromArgb(125, Color.White);


            //kulcsok.Text = string.Format("Publikus kulcs: {0} \n privát kulcs: {1}",publicKeyString,privateKeyString);

            richTextBox1.Text = GenerateTestString();

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
            var cryptoServiceProvider = new RSACryptoServiceProvider(2048);
            
            var publicKey = cryptoServiceProvider.ExportParameters(false);

            string publicKeyString = GetKeyString(publicKey);
            
            string encryptedText = Encrypt(richTextBox1.Text, publicKeyString);
            richTextBox2.Text = encryptedText;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var cryptoServiceProvider = new RSACryptoServiceProvider(2048);
            var privateKey = cryptoServiceProvider.ExportParameters(true);
            string privateKeyString = GetKeyString(privateKey);
            eredmeny.Text = Decrypt(richTextBox2.Text, privateKeyString);
        }

        public static string GetKeyString(RSAParameters publicKey)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }

        public static string Encrypt(string textToEncrypt, string publicKeyString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

            using(var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string Decrypt(string textToDecrypt, string privateKeyString)
        {
            var bytesToDecrypt = Encoding.UTF8.GetBytes(textToDecrypt);
            using(var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(privateKeyString);
                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private static string GenerateTestString()
        {
            Guid opportunityId = Guid.NewGuid();
            Guid systemUserId = Guid.NewGuid();
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("opportunityid={0}", opportunityId.ToString());
            sb.AppendFormat("&systemuserid={0}", systemUserId.ToString());
            sb.AppendFormat("&currenttime={0}", currentTime);

            return sb.ToString();
        }
    }
}
