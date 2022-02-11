using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GuitarTurner
{
    public class TurnerViewModel : INotifyPropertyChanged
    {
        static readonly List<Note> _notes = new();

        public event PropertyChangedEventHandler PropertyChanged;

        static TurnerViewModel()
        {
            float baseFreq;

            foreach (var note in Notes.GetAllGuitarNotes())
            {
                baseFreq = note.Frequency;

                for (int i = 0; i < 9; i++)
                {
                    _notes.Add(new Note(note.Key + i, baseFreq));
                    baseFreq *= 2;
                }
            }
        }

        float frequency;
        public float Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;

                var n = FindNote(frequency);

                if (n != null)
                {
                    Note = n.Symbol;
                    Distance = n.Distance(frequency);
                    Pitch = System.Math.Abs(160 + Distance);
                    BackgroundColor = n.Match(frequency) ? "#00ff00" : "#000000";
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Frequency)));
            }
        }

        string note = ".";
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Note)));
            }
        }

        double pitch = 160;
        public double Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pitch)));
            }
        }

        double distance;
        public double Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pitch)));
            }
        }

        string backgroundColor = "#000000";//"#C9C58F";

        public string BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
            }
        }

        private static Note FindNote(float freq)
            => _notes.OrderBy(x => x.Distance(freq)).First();
    }
}
