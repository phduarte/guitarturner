using NAudio.Wave;
//using System;
//using System.Diagnostics;
using System.Threading.Tasks;

namespace GuitarTurner.Infra
{
    public delegate void FrequenceChanged(float frequency);

    public class SoundListner
    {
        private BufferedWaveProvider _bufferedWaveProvider = null;
        private bool _exit;

        public event FrequenceChanged FrequenceChanged;

        //public int SelectInputDevice()
        //{
        //    int inputDevice = 0;
        //    bool isValidChoice = false;

        //    do
        //    {
        //        Debug.WriteLine(Messages.Sound.SelectInput);

        //        for (int i = 0; i < WaveInEvent.DeviceCount; i++)
        //        {
        //            Debug.WriteLine(i + ". " + WaveInEvent.GetCapabilities(i).ProductName);
        //        }

        //        try
        //        {
        //            if (int.TryParse(Console.ReadLine(), out inputDevice))
        //            {
        //                isValidChoice = true;
        //                Debug.WriteLine($"{Messages.Sound.YouHaveChosen} {WaveInEvent.GetCapabilities(inputDevice).ProductName}.\n");
        //            }
        //            else
        //            {
        //                isValidChoice = false;
        //            }
        //        }
        //        catch
        //        {
        //            throw new ArgumentException(Messages.Sound.DeviceOutOfRange);
        //        }

        //    } while (isValidChoice == false);

        //    return inputDevice;
        //}

        public void Start(int inputDevice)
        {
            WaveInEvent waveIn = new()
            {
                DeviceNumber = inputDevice,
                WaveFormat = new WaveFormat(44100, 1),
            };

            waveIn.DataAvailable += WaveIn_DataAvailable;

            _bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);

            // begin record
            waveIn.StartRecording();

            const int bufferSize = 8192;
            IWaveProvider stream = new Wave16ToFloatProvider(_bufferedWaveProvider);
            PitchDetector pitchDetector = new(stream, bufferSize);
            byte[] buffer = new byte[bufferSize];

            int bytesRead;

            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);

                float frequency = pitchDetector.Detect(buffer);

                if (frequency != 0)
                {
                    FrequenceChanged?.Invoke(frequency);
                }

            } while (bytesRead != 0 || !_exit);

            waveIn.StopRecording();
            waveIn.Dispose();
        }

        public Task StartAsync(int inputDevice)
        {
            return Task.Factory.StartNew(() =>
            {
                Start(inputDevice);
            });
        }

        public void Stop()
        {
            _exit = true;
        }

        void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_bufferedWaveProvider != null)
            {
                _bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
                _bufferedWaveProvider.DiscardOnBufferOverflow = true;
            }
        }
    }
}
