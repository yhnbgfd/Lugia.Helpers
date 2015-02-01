using Lugia.Helpers.Algorithm;
using System.Windows.Controls;

namespace TestApp.Views.Pages.Algorithms.AES
{
    public partial class Page_AES : Page
    {
        public Page_AES()
        {
            InitializeComponent();
            Button_CreateKey_Click(null, null);
        }

        private void Button_Encrypt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.TextBox_DecryptText.Text = "";
            this.TextBox_CipherText.Text = AESHelper.EncryptStringToBytes_Aes(this.TextBox_Plaintext.Text, this.TextBox_Key.Text, this.TextBox_VI.Text);
        }

        private void Button_Decrypt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.TextBox_DecryptText.Text = AESHelper.DecryptStringFromBytes_Aes(this.TextBox_CipherText.Text, this.TextBox_Key.Text, this.TextBox_VI.Text);
        }

        private void Button_CreateKey_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] keys = AESHelper.CreateKeyAndIV();
            this.TextBox_Key.Text = keys[0];
            this.TextBox_VI.Text = keys[1];
        }
    }
}
