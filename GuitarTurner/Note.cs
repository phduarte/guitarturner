using System;

namespace GuitarTurner
{
    class Note
    {
        public string Name { get; set; }
        public float Frequency { get; set; }

        public Note(string name, float frequency)
        {
            Name = name;
            Frequency = frequency;
        }

        public float Distance(float freq)
        {
            return Math.Abs(Frequency - freq);
        }

        public bool Match(float freq)
        {
            return (freq >= Frequency - 0.5) && (freq < Frequency + 0.485) || (freq == Frequency);
        }

        public override string ToString()
        {
            return $"{Name} {Frequency}";
        }
    }
}
