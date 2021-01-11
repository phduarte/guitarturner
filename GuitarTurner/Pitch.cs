using NAudio.Wave;

namespace GuitarTurner
{
    public class Pitch
    {
        readonly IWaveProvider _source;
        readonly WaveBuffer _waveBuffer;
        readonly Autocorrelator _pitchDetector;

        public Pitch(IWaveProvider source)
        {
            Check.IsTrue(source.WaveFormat.SampleRate != 44100, "Source must be at 44.1kHz");
            Check.IsTrue(source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat, "Source must be IEEE floating point audio data");
            Check.IsTrue(source.WaveFormat.Channels != 1, "Source must be a mono input source");

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
