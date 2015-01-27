using System.Windows;

namespace TestApp.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_RSA_Click(object sender, RoutedEventArgs e)
        {
            this.Frame_Content.Content = new Pages.Algorithms.RSA.Page_RSA();
        }

        private void Button_DataGridDragDrop_Click(object sender, RoutedEventArgs e)
        {
            this.Frame_Content.Content = new Pages.WpfControls.DataGrid.Page_DataGridDragDrop();
        }

        private void MenuItem_File_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            this.Frame_Content.Content = new Pages.Systems.Page_About();
        }
    }
}
