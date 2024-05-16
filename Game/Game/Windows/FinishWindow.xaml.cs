using System.Windows;

namespace Game
{
    public partial class FinishWindow : Window
    {
        private FightWindow _fightWindow;
        private MainWindow _mainWindow;

        public FinishWindow(string winnerText, FightWindow fightWindow, MainWindow mainWindow)
        {
            InitializeComponent();

            WinnerTextBlock.Text = winnerText;
            _fightWindow = fightWindow;
            _mainWindow = mainWindow;
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            // Сброс состояния FightWindow
            _fightWindow.Reset();

            // Закрытие FinishWindow
            Close();
        }

        private void ExitToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            _fightWindow.Dispose();
            Environment.Exit(0); // Завершение работы приложения
        }
    }
}
