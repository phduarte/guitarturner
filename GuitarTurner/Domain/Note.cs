using System;

namespace GuitarTurner
{
    record Note
    {
        public string Symbol { get; init; }
        public float Frequency { get; init; }

        public Note(string name, float frequency)
        {
            Symbol = name;
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
            return $"{Symbol} {Frequency}";
        }
    }
}
