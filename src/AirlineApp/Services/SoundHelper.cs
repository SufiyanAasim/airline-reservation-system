namespace AirlineApp.Services
{
    using System;
    using System.IO;
    using System.Media;
    using System.Threading.Tasks;

    public static class SoundHelper
    {
        public static void PlayTap()
        {
            Task.Run(() =>
            {
                try
                {
                    // Futuristic UI High-Frequency Synth Chime (1200 Hz 40ms)
                    byte[] wav = GenerateToneWav(1200, 40, 0.25f);
                    using var ms = new MemoryStream(wav);
                    using var player = new SoundPlayer(ms);
                    player.PlaySync();
                }
                catch
                {
                    // Fallback silently if audio hardware busy
                }
            });
        }

        public static void PlayAlert()
        {
            Task.Run(() =>
            {
                try
                {
                    // Aviation Cockpit Dual-Tone Alert Chime (880 Hz + 440 Hz)
                    byte[] wav1 = GenerateToneWav(880, 120, 0.4f);
                    byte[] wav2 = GenerateToneWav(440, 150, 0.4f);
                    
                    using (var ms = new MemoryStream(wav1))
                    using (var player = new SoundPlayer(ms))
                    {
                        player.PlaySync();
                    }
                    using (var ms = new MemoryStream(wav2))
                    using (var player = new SoundPlayer(ms))
                    {
                        player.PlaySync();
                    }
                }
                catch
                {
                }
            });
        }

        public static void PlayMaydayAlarm()
        {
            Task.Run(() =>
            {
                try
                {
                    // Emergency Cockpit Siren Dual-Pulse (1500 Hz / 1000 Hz)
                    for (int i = 0; i < 3; i++)
                    {
                        byte[] wav1 = GenerateToneWav(1500, 90, 0.5f);
                        byte[] wav2 = GenerateToneWav(1000, 90, 0.5f);
                        using (var ms1 = new MemoryStream(wav1))
                        using (var p1 = new SoundPlayer(ms1))
                        {
                            p1.PlaySync();
                        }
                        using (var ms2 = new MemoryStream(wav2))
                        using (var p2 = new SoundPlayer(ms2))
                        {
                            p2.PlaySync();
                        }
                    }
                }
                catch
                {
                }
            });
        }

        public static void PlaySuccess()
        {
            Task.Run(() =>
            {
                try
                {
                    // Ascending Boarding Pass Confirmed Chime (C5 ➔ E5 ➔ G5)
                    int[] freqs = new int[] { 523, 659, 784 };
                    foreach (int f in freqs)
                    {
                        byte[] wav = GenerateToneWav(f, 80, 0.35f);
                        using var ms = new MemoryStream(wav);
                        using var player = new SoundPlayer(ms);
                        player.PlaySync();
                    }
                }
                catch
                {
                }
            });
        }

        private static byte[] GenerateToneWav(int frequency, int durationMs, float volume)
        {
            int sampleRate = 44100;
            int numSamples = sampleRate * durationMs / 1000;
            short[] samples = new short[numSamples];

            double angleStep = 2 * Math.PI * frequency / sampleRate;
            double angle = 0;

            for (int i = 0; i < numSamples; i++)
            {
                // Exponential decay envelope for smooth synth feel
                float envelope = (float)Math.Exp(-3.0 * i / numSamples);
                samples[i] = (short)(Math.Sin(angle) * short.MaxValue * volume * envelope);
                angle += angleStep;
            }

            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            // Write 44-byte RIFF WAV Header
            writer.Write(new char[] { 'R', 'I', 'F', 'F' });
            writer.Write(36 + numSamples * 2);
            writer.Write(new char[] { 'W', 'A', 'V', 'E' });
            writer.Write(new char[] { 'f', 'm', 't', ' ' });
            writer.Write(16); // Subchunk1Size (16 for PCM)
            writer.Write((short)1); // AudioFormat (1 for PCM)
            writer.Write((short)1); // NumChannels (1 for Mono)
            writer.Write(sampleRate);
            writer.Write(sampleRate * 2); // ByteRate
            writer.Write((short)2); // BlockAlign
            writer.Write((short)16); // BitsPerSample
            writer.Write(new char[] { 'd', 'a', 't', 'a' });
            writer.Write(numSamples * 2);

            foreach (var sample in samples)
            {
                writer.Write(sample);
            }

            return ms.ToArray();
        }
    }
}
