using NVorbis;

namespace MiCore2d.Audio
{
    /// <summary>
    /// OggLoader. Load OGG audio from file.
    /// </summary>
    public class OggLoader
    {
        /// <summary>
        /// LoadOggFromFile.
        /// </summary>
        /// <param name="filename">audio file name</param>
        /// <param name="_channels">out parameter. number of channels</param>
        /// <param name="_sampleRate">oug parameter. sample rate</param>
        /// <returns>audio array buffer</returns>
        public static float[] LoadOggFromFile(string filename, out int _channels, out int _sampleRate)
        {
            using(VorbisReader vorbis = new VorbisReader(filename))
            {
                if (vorbis == null)
                    throw new FileNotFoundException(filename);
                
                int channel = vorbis.Channels;
                int sampleRate = vorbis.SampleRate;
                long totalSamples = vorbis.TotalSamples;

                _channels = channel;
                _sampleRate = sampleRate;

                int buffer_size = (int)totalSamples * channel;
                float[] buffer = new float[buffer_size];
                vorbis.ReadSamples(buffer, 0, buffer_size);

                return buffer;
            }
        }
    }
}
