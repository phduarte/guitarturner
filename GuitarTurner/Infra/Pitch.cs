using NAudio.Wave;
using Text = GuitarTurner.Messages.Pitch;

namespace GuitarTurner.Infra
{
    public class Pitch
    {
        readonly IWaveProvider _source;
        readonly WaveBuffer _waveBuffer;
        readonly Autocorrelator _pitchDetector;

        public Pitch(IWaveProvider source)
        {
            Check.IsTrue(source.WaveFormat.SampleRate != 44100, Text.SourceMustBe44100);
            Check.IsTrue(source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat, Text.SourceMustBeIEEE);
            Check.IsTrue(source.WaveFormat.Channels != 1, Text.SourceMustBeMono);

            _source = source;
            _pitchDetector = new Autocorrelator(source.WaveFormat.SampleRate);
            _waveBuffer = new WaveBuffer(8192);
        }

        public float Get(byte[] buffer)
        {
            int bytesRead = _source.Read(_waveBuffer, 0, buffer.Length);

            if (bytesRead > 0)
            {
                bytesRead = buffer.Length;
            }

            int frames = bytesRead / sizeof(float);

            return _pitchDetector.DetectPitch(_waveBuffer.FloatBuffer, frames);
        }
    }
}
