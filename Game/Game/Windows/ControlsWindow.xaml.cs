using System.Windows;

namespace Game
{
    public partial class ControlsWindow : Window
    {
        public ControlsWindow()
        {
            InitializeComponent();
        }

        private void CloseStoryboard_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
