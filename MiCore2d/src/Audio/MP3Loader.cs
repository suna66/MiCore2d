using NLayer;

namespace MiCore2d.Audio
{
    public class MP3Loader
    {
        public static float[] LoadMP3FromFile(string filename, out int _channels, out int _sampleRate)
        {
            using(MpegFile _stream = new MpegFile(filename))
            {
                if (_stream == null)
                    throw new FileNotFoundException(filename);

                _sampleRate = _stream.SampleRate;
                _channels = _stream.Channels;

                int size = (int)_stream.Length;
                float[] data = new float[size/sizeof(float)];

                _stream.ReadSamples(data, 0, size/sizeof(float));

                return data;
            }
        }
    }
}
