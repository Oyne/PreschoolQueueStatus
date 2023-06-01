using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Queue
{
    /// <summary>
    /// Interaction logic for Author.xaml
    /// </summary>
    public partial class Author : Window
    {
        public Author()
        {
            InitializeComponent();
        }

        private void MeImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.instagram.com/andreoyne_litvinov/") { UseShellExecute = true });

        }
        private void GmailImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("mailto:a.a.litvinov@student.csn.khai.edu") { UseShellExecute = true });
        }
        private void TelegramImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://t.me/Andreoyne") { UseShellExecute = true });
        }

        private void GitHubImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/Oyne") { UseShellExecute = true });
        }

        
    }
}
