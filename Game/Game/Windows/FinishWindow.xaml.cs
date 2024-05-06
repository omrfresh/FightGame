using System.Windows;

namespace Game
{
    public partial class FinishWindow : Window
    {
        public FinishWindow(string winnerText)
        {
            InitializeComponent();

            WinnerTextBlock.Text = winnerText;
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие FinishWindow
            Close();

            // Создание нового экземпляра FightWindow
            var fightWindow = new FightWindow();
            fightWindow.Run();
        }
    }
}
