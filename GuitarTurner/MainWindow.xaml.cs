using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuitarTurner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Sound _sound = new Sound();
        public TurnerViewModel turner = new TurnerViewModel();

        public MainWindow()
        {
            InitializeComponent();
            _sound.FrequenceChanged += _sound_FrequenceChanged;
            //_sound.NoteFound += _sound_NoteFound;

            DataContext = turner;
        }

        //private void _sound_NoteFound(string note, float frequency)
        //{
        //    turner.Note = note;
        //}

        private void _sound_FrequenceChanged(float frequency)
        {
            turner.Frequency = frequency;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sound.StartDetectAsync(0);
        }
    }
}
