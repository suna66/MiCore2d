using NLayer;

namespace MiCore2d.Audio
{
    /// <summary>
    /// MP3 Audio Loader.
    /// </summary>
    public class MP3Loader
    {
        /// <summary>
        /// LoadMP3FromFile. Load MP3 audio from file.
        /// </summary>
        /// <param name="filename">audio file name</param>
        /// <param name="_channel">out parameter. number of channels</param>
        /// <param name="_sampleRate">out parameter. sample rate</param>
        /// <returns>audio arrray buffer</returns>
        public static float[] LoadMP3FromFile(string filename, out int _channels, out int _sampleRate)
        {
            using(MpegFile _stream = new MpegFile(filename))
            {
                if (_stream == null)
                    throw new FileNotFoundException(filename);

                return loadMP3(_stream, out _channels, out _sampleRate);
            }
        }

        /// <summary>
        /// LoadMP3FromStream.
        /// </summary>
        /// <param name="stream">Straem</param>
        /// <param name="_channel">out parameter. number of channels</param>
        /// <param name="_sampleRate">out parameter. sample rate</param>
        /// <returns>audio arrray buffer</returns>
        public static float[] LoadMP3FromStream(Stream stream, out int _channels, out int _sampleRate)
        {
            using(MpegFile _stream = new MpegFile(stream))
            {
                if (_stream == null)
                    throw new ArgumentNullException("file stream is null");

                return loadMP3(_stream, out _channels, out _sampleRate);
            }
        }

        /// <summary>
        /// loadMP3.
        /// </summary>
        /// <param name="_stream">Stream</param>
        /// <param name="_channel">out parameter. number of channels</param>
        /// <param name="_sampleRate">out parameter. sample rate</param>
        /// <returns>audio arrray buffer</returns>
        private static float[] loadMP3(MpegFile _stream, out int _channels, out int _sampleRate)
        {
            _sampleRate = _stream.SampleRate;
            _channels = _stream.Channels;

            int size = (int)_stream.Length;
            float[] data = new float[size/sizeof(float)];

            _stream.ReadSamples(data, 0, size/sizeof(float));

            return data;
        }
    }
}
