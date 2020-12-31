using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GuitarTurner
{
    public class TurnerViewModel : INotifyPropertyChanged
    {
        static List<Note> _notes = new List<Note>();
        static Dictionary<string, float> noteBaseFreqs = new Dictionary<string, float>()
            {
                { "C", 16.35f },
                { "C#", 17.32f },
                { "D", 18.35f },
                { "Eb", 19.45f },
                { "E", 20.60f },
                { "F", 21.83f },
                { "F#", 23.12f },
                { "G", 24.50f },
                { "G#", 25.96f },
                { "A", 27.50f },
                { "Bb", 29.14f },
                { "B", 30.87f },
            };

        float frequency;
        string note;
        double position;

        public event PropertyChangedEventHandler PropertyChanged;

        static TurnerViewModel()
        {
            float baseFreq;

            foreach (var note in noteBaseFreqs)
            {
                baseFreq = note.Value;

                for (int i = 0; i < 9; i++)
                {
                    _notes.Add(new Note(note.Key + i, baseFreq));
                    baseFreq *= 2;
                }
            }
        }

        public float Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;

                var n = FindNote(frequency);

                if (n != null)
                {
                    Note = n.Name;
                    Distance = n.Distance(frequency);
                    Pitch = System.Math.Abs(160 + Distance);
                    BackgroundColor = n.Match(frequency) ? "#00ff00" : "#000000";
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Frequency)));
            }
        }

        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Note)));
            }
        }
        public double Pitch
        {
            get { return position; }
            set
            {
                position = value;
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

        private Note FindNote(float freq)
            => _notes.OrderBy(x => x.Distance(freq)).First();
    }
}
