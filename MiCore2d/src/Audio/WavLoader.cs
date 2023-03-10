#nullable disable warnings
#pragma warning disable CS0675
using System.Runtime.InteropServices;

namespace MiCore2d.Audio
{
    /// <summary>
    /// WaveLoader. WAV audio loader.
    /// </summary>
    public class WavLoader
    {
        /// <summary>
        /// LoadWavFile. Load WAV audio from file.
        /// </summary>
        /// <param name="filename">audio file name</param>
        /// <param name="_channels">out parameter. number of channels</param>
        /// <param name="_bitsPerSample">out parameter. bits par sample</param>
        /// <param name="_sampleRate">out parameter. sample rate</param>
        /// <returns>audio array buffer</returns>
        public static float[] LoadWavFile(string filename, out int _channels, out int _bitsPerSample, out int _sampleRate)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                return loadWav(reader, out _channels, out _bitsPerSample, out _sampleRate);
            }
        }

        /// <summary>
        /// LoadWavFile. Load WAV audio from stream.
        /// </summary>
        /// <param name="stream">stream</param>
        /// <param name="_channels">out parameter. number of channels</param>
        /// <param name="_bitsPerSample">out parameter. bits par sample</param>
        /// <param name="_sampleRate">out parameter. sample rate</param>
        /// <returns>audio array buffer</returns>
        public static float[] LoadWavStream(Stream stream, out int _channels, out int _bitsPerSample, out int _sampleRate)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                return loadWav(reader, out _channels, out _bitsPerSample, out _sampleRate);
            }
        }

        /// <summary>
        /// loadWav
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <param name="_channels">out parameter. number of channels</param>
        /// <param name="_bitsPerSample">out parameter. bits par sample</param>
        /// <param name="_sampleRate">out parameter. sample rate</param>
        /// <returns>audio array buffer</returns>
        private static float[] loadWav(BinaryReader reader , out int _channels, out int _bitsPerSample, out int _sampleRate)
        {
            Stream baseStream = reader.BaseStream;

            string signature = new string(reader.ReadChars(4));
            //Console.WriteLine(signature);
            if (signature != "RIFF")
                throw new NotSupportedException("Not wav format.");

            int riff_chunck_size = reader.ReadInt32();

            string format = new string(reader.ReadChars(4));
            if (format != "WAVE")
                throw new NotSupportedException("Not wav format.");

            string format_signature = "";
            byte[] data = null!;
            _channels = 0;
            _bitsPerSample = 0;
            _sampleRate = 0;

            while (baseStream.Position != baseStream.Length)
            {
                format_signature = new string(reader.ReadChars(4));
                if (format_signature == "fmt ")
                {
                    reader.ReadInt32(); //chunk size
                    int fmt = reader.ReadInt16(); //audio format
                    int num_channels = reader.ReadInt16(); //number of channels
                    int sample_rate = reader.ReadInt32(); //sample rate
                    reader.ReadInt32(); //byte rate
                    reader.ReadInt16(); //block algin
                    int bits_per_sample = reader.ReadInt16(); //bits per sample
                    if (fmt != 0x01) {
                            throw new NotSupportedException("unsupport format. PCM format only");
                    }
                    _channels = num_channels;
                    _bitsPerSample = bits_per_sample;
                    _sampleRate = sample_rate;
                }
                else if (format_signature == "data")
                {
                    int data_chunk_size = reader.ReadInt32();
                    data = reader.ReadBytes(data_chunk_size);
                }
                else
                {
                    int chunk_size = reader.ReadInt32();
                    reader.ReadBytes(chunk_size);
                }
            }
            if (_bitsPerSample == 8)
            {
                return convert8ToFloat32(data);
            }
            else if (_bitsPerSample == 16)
            {
                return convert16ToFloat32(data);
            }
            else
            {
                return convertFloat32(data);
            }
        }

        /// <summary>
        /// convertFloat32. Convert byte array data to float array data.
        /// </summary>
        /// <param name="data">byte array data</param>
        /// <returns>float audio array buffer</returns>
        private static float[] convertFloat32(byte[] data)
        {
            int data_index = 0;
            int result_size = 0;
            int result_index = 0;

            result_size = (int)((data.Length / 3));
            float[] result_data = new float[result_size];

            while (data_index < data.Length)
            {
                byte c1 = data[data_index++];
                byte c2 = data[data_index++];
                byte c3 = data[data_index++];

                int value = (int)(((c3 & 0x000000ff) << 16) | ((c2 & 0x000000ff) << 8) | (c1 & 0x000000ff));
                value = (int)((value & 0x00ffffff) | (((value & 0x800000) != 0) ? 0xff000000 : 0x00000000));

                result_data[result_index++] =  (float)value / 8388607.0f;
            }
            return result_data;
        }

        /// <summary>
        /// convert16ToFloat32. Convert 16bit audio array buffer to float array buffer.
        /// </summary>
        /// <param name="data">16bit audio array buffer</param>
        /// <returns>float audio array buffer</returns>
        private static float[] convert16ToFloat32(byte[] data)
        {
            int data_index = 0;
            int result_size = 0;
            int result_index = 0;

            result_size = (int)((data.Length / 2));
            float[] result_data = new float[result_size];

            while (data_index < data.Length)
            {
                byte c1 = data[data_index++];
                byte c2 = data[data_index++];

                int value = (int)(((c2 & 0x000000ff) << 8) | (c1 & 0x000000ff));
                value = (int)((value & 0x0000ffff) | (((value & 0x8000) != 0) ? 0xffff0000 : 0x00000000));

                result_data[result_index++] =  (float)value / 65535.0f;
            }
            return result_data;
        }

        /// <summary>
        /// convert8ToFloat32. Convert 8bit audio array data to float array buffer.
        /// </summary>
        /// <param name="data">8bit audio array buffer</param>
        /// <returns>float audio array buffer</returns>
        private static float[] convert8ToFloat32(byte[] data)
        {
            int data_index = 0;
            int result_size = 0;
            int result_index = 0;

            result_size = data.Length;
            float[] result_data = new float[result_size];

            while (data_index < data.Length)
            {
                byte c1 = data[data_index++];

                int value = (int)c1 - 128;
                result_data[result_index++] =  (float)value / 128;
            }
            return result_data;
        }
    }
}
