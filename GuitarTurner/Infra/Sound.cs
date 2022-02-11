using NAudio.Wave;
//using System;
//using System.Diagnostics;
using System.Threading.Tasks;

namespace GuitarTurner
{
    public delegate void FrequenceChanged(float frequency);

    public class Sound
    {
        BufferedWaveProvider bufferedWaveProvider = null;
        private bool exit;

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

        public void StartDetect(int inputDevice)
        {
            WaveInEvent waveIn = new();

            waveIn.DeviceNumber = inputDevice;
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;

            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);

            // begin record
            waveIn.StartRecording();

            IWaveProvider stream = new Wave16ToFloatProvider(bufferedWaveProvider);
            Pitch pitch = new(stream);

            byte[] buffer = new byte[8192];
            int bytesRead;

            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);

                float freq = pitch.Get(buffer);

                if (freq != 0)
                {
                    FrequenceChanged?.Invoke(freq);
                }

            } while (bytesRead != 0 || !exit);

            waveIn.StopRecording();
            waveIn.Dispose();
        }

        public Task StartDetectAsync(int inputDevice)
        {
            return Task.Factory.StartNew(() =>
            {
                StartDetect(inputDevice);
            });
        }

        public void Stop()
        {
            exit = true;
        }

        void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (bufferedWaveProvider != null)
            {
                bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
                bufferedWaveProvider.DiscardOnBufferOverflow = true;
            }
        }
    }
}
