using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Encrypt_And_Decrypt_Tool
{
    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();
        }

        private string Hash = "2fede9472d49e9a2ea77b8cebbbad261230e26319b6d87e237a22e2224bd7b70";

        private void Encrypt()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter the Text to be Encrypted!", "Encrypt And Decrypt Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    byte[] Data = UTF8Encoding.UTF8.GetBytes(textBox1.Text);

                    using (MD5CryptoServiceProvider MD5CryptoServiceProvider = new MD5CryptoServiceProvider())
                    {
                        byte[] Keys = MD5CryptoServiceProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));

                        using (TripleDESCryptoServiceProvider TripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider() { Key = Keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                        {
                            ICryptoTransform ICryptoTransform = TripleDESCryptoServiceProvider.CreateEncryptor();

                            byte[] Output = ICryptoTransform.TransformFinalBlock(Data, 0, Data.Length);

                            textBox2.Text = Convert.ToBase64String(Output, 0, Output.Length);
                        }
                    }
                }
                catch (Exception Error)
                {
                    MessageBox.Show(Error.Message, "Encrypt And Decrypt Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Decrypt()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter the Text to be Decrypted!", "Encrypt And Decrypt Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    byte[] Data = Convert.FromBase64String(textBox2.Text);

                    using (MD5CryptoServiceProvider MD5CryptoServiceProvider = new MD5CryptoServiceProvider())
                    {
                        byte[] Keys = MD5CryptoServiceProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));

                        using (TripleDESCryptoServiceProvider TripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider() { Key = Keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                        {
                            ICryptoTransform ICryptoTransform = TripleDESCryptoServiceProvider.CreateDecryptor();

                            byte[] Output = ICryptoTransform.TransformFinalBlock(Data, 0, Data.Length);

                            textBox3.Text = UTF8Encoding.UTF8.GetString(Output);
                        }
                    }
                }
                catch (Exception Error)
                {
                    MessageBox.Show(Error.Message, "Encrypt And Decrypt Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Encrypt();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Decrypt();
        }
    }
}