using System.Windows;

namespace Game
{
    public partial class MainWindow : Window
    {
        private FightWindow _fightWindow;
        private ControlsWindow _controlsWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            _fightWindow = new FightWindow();
            this.Hide();
            _fightWindow.Run();
        }

        private void ControlsButton_Click(object sender, RoutedEventArgs e)
        {
            _controlsWindow = new ControlsWindow();
            this.Hide();
            _controlsWindow.ShowDialog();
            this.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
