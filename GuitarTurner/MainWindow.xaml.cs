using GuitarTurner.Infra;
using System.Windows;

namespace GuitarTurner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Sound _sound = new();
        public TurnerViewModel turner = new();

        public MainWindow()
        {
            InitializeComponent();
            _sound.FrequenceChanged += Sound_FrequenceChanged;

            DataContext = turner;
        }

        private void Sound_FrequenceChanged(float frequency)
        {
            turner.Frequency = frequency;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sound.StartDetectAsync(0);
        }
    }
}
