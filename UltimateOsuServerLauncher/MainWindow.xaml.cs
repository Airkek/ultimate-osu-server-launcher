using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace UltimateOsuServerLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ServerLauncher _launcher;

        public MainWindow()
        {
            _launcher = new ServerLauncher();
            InitializeComponent();
            
            updateCurrentServer();
        }

        public void updateCurrentServer()
        {
            currentServerLabel.Content = _launcher.CurrentServer.ServerName;
            websiteText.Text = _launcher.CurrentServer.DisplayUrl;
            
            prevServerButton.IsEnabled = !_launcher.IsFirstServer;
            nextServerButton.IsEnabled = !_launcher.IsLastServer;

            prevServerButton.Content = _launcher.IsFirstServer ? " " : "<";
            nextServerButton.Content = _launcher.IsLastServer ? " " : ">";
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void LaunchButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            _launcher.Launch();
        }

        private void websiteText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(_launcher.CurrentServer.FrontendUrl);
        }

        private void NextServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            _launcher.NextServer();
            updateCurrentServer();
        }

        private void PrevServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            _launcher.PreviousServer();
            updateCurrentServer();
        }
    }
}