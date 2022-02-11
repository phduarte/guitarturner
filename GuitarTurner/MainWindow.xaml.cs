using GuitarTurner.Infra;
using System.Windows;

namespace GuitarTurner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SoundListner _sound = new();
        public TurnerViewModel _turner = new();

        public MainWindow()
        {
            InitializeComponent();
            _sound.FrequenceChanged += Sound_FrequenceChanged;

            DataContext = _turner;
        }

        private void Sound_FrequenceChanged(float frequency)
        {
            _turner.Frequency = frequency;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sound.StartAsync(0);
        }
    }
}
