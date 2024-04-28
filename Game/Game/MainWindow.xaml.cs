using System.Windows;

namespace Game
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            FightWindow fightWindow = new FightWindow();
            fightWindow.Run();
        }
    }
}
