using System.Collections.Generic;
using System.Linq;

namespace GuitarTurner
{
    static class Notes
    {
        static List<NoteFrequency> noteBaseFreqs = new List<NoteFrequency>
            {
                new NoteFrequency{ Key = "C", Frequency= 16.35f, IsBass = false, IsGuitar = false, IsUkulele = false },
                new NoteFrequency{ Key = "C#", Frequency= 17.32f , IsBass = false, IsGuitar = false, IsUkulele = false },
                new NoteFrequency{ Key = "D", Frequency= 18.35f , IsBass = true, IsGuitar = true, IsUkulele = false },
                new NoteFrequency{ Key = "Eb", Frequency= 19.45f , IsBass = false, IsGuitar = false, IsUkulele = false },
                new NoteFrequency{ Key = "E", Frequency= 20.60f , IsBass = true, IsGuitar = true, IsUkulele = false },
                new NoteFrequency{ Key = "F", Frequency= 21.83f , IsBass = false, IsGuitar = false, IsUkulele = false },
                new NoteFrequency{ Key = "F#",Frequency=  23.12f , IsBass = false, IsGuitar = false, IsUkulele = false },
                new NoteFrequency{ Key = "G", Frequency= 24.50f , IsBass = true, IsGuitar = true, IsUkulele = false },
                new NoteFrequency{ Key = "G#", Frequency= 25.96f , IsBass = false, IsGuitar = false, IsUkulele = false },
                new NoteFrequency{ Key = "A", Frequency= 27.50f , IsBass = true, IsGuitar = true, IsUkulele = false },
                new NoteFrequency{ Key = "Bb", Frequency= 29.14f , IsBass = false, IsGuitar = false, IsUkulele = false },
                new NoteFrequency{ Key = "B", Frequency= 30.87f , IsBass = false, IsGuitar = true, IsUkulele = false },
            };

        public static IEnumerable<NoteFrequency> GetAll() => noteBaseFreqs;
        public static IEnumerable<NoteFrequency> GetAllGuitarNotes() => noteBaseFreqs.Where(x => x.IsGuitar);
    }
}
