using System;
using System.IO;
using System.Windows;
using SaikyoVpn.Core;

namespace SaikyoVpn.Gui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            var (priv, pub) = WireGuardHelper.GenerateKeyPair();
            TxtPrivateKey.Text = priv;
            TxtPublicKey.Text = pub;
            TxtConfig.Text = WireGuardHelper.BuildConfig(priv);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = "saikyo.conf",
                DefaultExt = ".conf",
                Filter = "Config files (*.conf)|*.conf|All files (*.*)|*.*"
            };
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, TxtConfig.Text);
                MessageBox.Show("Configuration saved.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
