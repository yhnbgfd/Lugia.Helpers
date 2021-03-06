﻿using Lugia.Helpers.Algorithm;
using System.Windows;
using System.Windows.Controls;

namespace TestApp.Views.Pages.Algorithms.RSA
{
    public partial class Page_RSA : Page
    {
        public Page_RSA()
        {
            InitializeComponent();
            Button_CreateKey_Click(null, null);
            this.TextBox_EncryptStr.Focus();
        }

        private void Button_CreateKey_Click(object sender, RoutedEventArgs e)
        {
            int dwKeySize = int.Parse(((ComboBoxItem)this.ComboBox_dwKeySize.SelectedItem).Content.ToString());
            string[]  keys = new RSAHelper().CreateKey(dwKeySize);
            this.TextBox_Publickey.Text = keys[1];
            this.TextBox_Privatekey.Text = keys[0];
        }

        private void Button_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            this.TextBox_EncryptResult.Text = new RSAHelper().Encrypt(this.TextBox_Publickey.Text, this.TextBox_EncryptStr.Text);
            this.TextBox_DecryptStr.Text = "";
        }

        private void Button_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            this.TextBox_DecryptStr.Text = new RSAHelper().Decrypt(this.TextBox_Privatekey.Text, this.TextBox_EncryptResult.Text);
        }

        private void ComboBox_dwKeySize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
            {
                return;
            }
            Button_CreateKey_Click(null, null);
        }

        private void Button_SignData_Click(object sender, RoutedEventArgs e)
        {
            this.TextBox_EncryptResult.Text = new RSAHelper().SignData(this.TextBox_Privatekey.Text, this.TextBox_EncryptStr.Text);
            this.TextBox_DecryptStr.Text = "";
        }

        private void Button_VerifyData_Click(object sender, RoutedEventArgs e)
        {
            this.TextBox_DecryptStr.Text = new RSAHelper().VerifyData(this.TextBox_Publickey.Text, this.TextBox_EncryptStr.Text,this.TextBox_EncryptResult.Text).ToString();
        }
    }
}
