using NAudio.Wave;
using Text = GuitarTurner.Messages.Pitch;

namespace GuitarTurner.Infra
{
    public class PitchDetector
    {
        private readonly IWaveProvider _waveProvider;
        private readonly WaveBuffer _waveBuffer;
        private float[] _prevBuffer;
        private readonly int _minOffset;
        private readonly int _maxOffset;
        private readonly float _sampleRate;

        public PitchDetector(IWaveProvider waveProvider, int bufferSize)
        {
            Check.IsTrue(waveProvider.WaveFormat.SampleRate != 44100, Text.SourceMustBe44100);
            Check.IsTrue(waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat, Text.SourceMustBeIEEE);
            Check.IsTrue(waveProvider.WaveFormat.Channels != 1, Text.SourceMustBeMono);

            _waveProvider = waveProvider;
            _waveBuffer = new WaveBuffer(bufferSize);

            int minFreq = 75, maxFreq = 335;

            _sampleRate = waveProvider.WaveFormat.SampleRate;
            _maxOffset = waveProvider.WaveFormat.SampleRate / minFreq;
            _minOffset = waveProvider.WaveFormat.SampleRate / maxFreq;
        }

        public float Analyze(byte[] buffer)
        {
            int bytesRead = _waveProvider.Read(_waveBuffer, 0, buffer.Length);

            if (bytesRead > 0)
            {
                bytesRead = buffer.Length;
            }

            int frames = bytesRead / sizeof(float);

            return Detect(_waveBuffer.FloatBuffer, frames);
        }

        private float Detect(float[] buffer, int frames)
        {
            _prevBuffer ??= new float[frames];

            float maxCorr = 0;
            int maxLag = 0;

            // starting with low frequencies, working to higher
            for (int lag = _maxOffset; lag >= _minOffset; lag--)
            {
                float corr = 0; // this is calculated as the sum of squares
                for (int i = 0; i < frames; i++)
                {
                    int oldIndex = i - lag;
                    float sample = ((oldIndex < 0) ? _prevBuffer[frames + oldIndex] : buffer[oldIndex]);
                    corr += (sample * buffer[i]);
                }
                if (corr > maxCorr)
                {
                    maxCorr = corr;
                    maxLag = lag;
                }
            }

            for (int n = 0; n < frames; n++)
            {
                _prevBuffer[n] = buffer[n];
            }

            float noiseThreshold = frames / 1000f;

            if (maxCorr < noiseThreshold || maxLag == 0) return 0.0f;

            return _sampleRate / maxLag;
        }
    }
}
