namespace GuitarTurner.Domain
{
    record NoteFrequency
    {
        public string Key { get; init; }
        public float Frequency { get; init; }
        public bool IsGuitar { get; init; }
        public bool IsBass { get; init; }
        public bool IsUkulele { get; init; }
    }
}
