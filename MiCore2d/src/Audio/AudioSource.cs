#nullable disable warnings
using NLayer;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace MiCore2d.Audio
{
    public struct AudioClip
    {
        public int size;
        public int sampleRate;
        public int channels;
        public int state;
        public int buffer;
        public int source;
        public float[] data;
        public bool isLoop;
    }

    /// <summary>
    /// AudioSource. Management audio data
    /// </summaray>
    public class AudioSource : IDisposable
    {
        private ALDevice device;
        private ALContext context;
        private Dictionary<string, AudioClip> clipDic;

        private bool disposed = false;

        /// <summary>
        /// constractor.
        /// </summary>
        public AudioSource()
        {
            int[] attr = { 0 };
            string defname = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
            device = ALC.OpenDevice(defname);
            context = ALC.CreateContext(device, attr);
            ALC.MakeContextCurrent(context);
            ALC.ProcessContext(context);

            AL.Listener(ALListenerf.Gain, 0.5f);

            clipDic = new Dictionary<string, AudioClip>();
        }

        /// <summary>
        /// addAudioClipDic
        /// <summary>
        /// <param name="key">alias name</param>
        /// <param name="data">array data of audio data</param>
        /// <param name="sampleRate">sample rate</param>
        /// <param name="channel">number of channels</param>
        /// <param name="isLoop">playback looping or not</param>
        private void addAudioClipDic(string key, float[] data, int sampleRate, int channel, bool isLoop)
        {
            AudioClip clip = new AudioClip();
            clip.sampleRate = sampleRate;
            clip.channels = channel;
            clip.size = data.Length * sizeof(float);
            clip.data = data;

            clip.buffer = AL.GenBuffer();
            clip.source = AL.GenSource();

            if (channel == 1)
            {
                AL.BufferData(clip.buffer, ALFormat.MonoFloat32Ext, clip.data, clip.sampleRate);
            }
            else if (channel == 2)
            {
                AL.BufferData(clip.buffer, ALFormat.StereoFloat32Ext, clip.data, clip.sampleRate);
            }
            else
            {
                throw new NotSupportedException($"3 or upper channels is not supported.");
            }
            AL.Source(clip.source, ALSourcei.Buffer, clip.buffer);

            clip.isLoop = isLoop;
            if (isLoop)
            {
                AL.Source(clip.source, ALSourceb.Looping, true);
            }

            clipDic.Add(key, clip);
        }

        /// <summary>
        /// LoadMP3File
        /// <summary>
        /// <param name="key">alias name</param>
        /// <param name="fname">audio file name</param>
        /// <param name="isLoop">playback looping or not</param>
        public void LoadMP3File(string key, string fname, bool isLoop)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            int channel = 0;
            int sampleRate = 0;
            float[] data = MP3Loader.LoadMP3FromFile(fname, out channel, out sampleRate);
            addAudioClipDic(key, data, sampleRate, channel, isLoop);
        }

        /// <summary>
        /// LoadOggFile
        /// <summary>
        /// <param name="key">alias name</param>
        /// <param name="fname">audio file name</param>
        /// <param name="isLoop">playback looping or not</param>
        public void LoadOggFile(string key, string fname, bool isLoop)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            int channel = 0;
            int sampleRate = 0;
            float[] data = OggLoader.LoadOggFromFile(fname, out channel, out sampleRate);
            addAudioClipDic(key, data, sampleRate, channel, isLoop);
        }

        /// <summary>
        /// LoadWavFile
        /// <summary>
        /// <param name="key">alias name</param>
        /// <param name="fname">audio file name</param>
        /// <param name="isLoop">playback looping or not</param>
        public void LoadWavFile(string key, string fname, bool isLoop)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            int channel = 0;
            int sampleRate = 0;
            int bitsPerSample = 0;
            float[] data = WavLoader.LoadWavFile(fname, out channel, out bitsPerSample, out sampleRate);
            addAudioClipDic(key, data, sampleRate, channel, isLoop);
        }

        /// <summary>
        /// IsLoaded. 
        /// <summary>
        /// <param name="key">alias name</param>
        /// <return>ture is already loaded the audio specified the key. false is not.</return>
        public bool IsLoaded(string key)
        {
            if (clipDic.ContainsKey(key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Play. 
        /// <summary>
        /// <param name="key">alias name</param>
        public void Play(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            if (clipDic.ContainsKey(key))
            {
                if (IsPlay(key))
                {
                    Stop(key);
                }
                AL.SourcePlay(clipDic[key].source);
            }
        }

        /// <summary>
        /// IsPlay. 
        /// <summary>
        /// <param name="key">alias name</param>
        /// <return>ture is already playing the audio specified the key. false is not.</return>
        public bool IsPlay(string key)
        {
            if (clipDic.ContainsKey(key))
            {
                ALSourceState state = getSourceState(key);
                if (state == ALSourceState.Playing)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Stop. 
        /// <summary>
        /// <param name="key">alias name</param>
        public void Stop(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            if (clipDic.ContainsKey(key))
            {
                AL.SourceStop(clipDic[key].source);
            }
        }

        /// <summary>
        /// Stop.  All audios are stopped.
        /// <summary>
        /// <param name="key">alias name</param>
        public void Stop()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            // for (int i = 0; i < clipList.Count; i++)
            // {
            //     ALSourceState state = getSourceState(i);
            //     if (state == ALSourceState.Playing && state == ALSourceState.Paused)
            //     {
            //         Stop(i);
            //     }
            // }
            foreach (KeyValuePair<string, AudioClip> kvp in clipDic)
            {
                ALSourceState state = getSourceState(kvp.Key);
                if (state == ALSourceState.Playing && state == ALSourceState.Paused)
                {
                    Stop(kvp.Key);
                }
            }
        }

        /// <summary>
        /// Pause. 
        /// <summary>
        /// <param name="key">alias name</param>
        public void Pause(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            //AL.SourcePause(clipList[idx].source);
            if (clipDic.ContainsKey(key))
            {
                AL.SourcePause(clipDic[key].source);
            }
        }

        /// <summary>
        /// Pause.  All audio are paused.
        /// <summary>
        /// <param name="key">alias name</param>
        public void Pause()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            foreach (KeyValuePair<string, AudioClip> kvp in clipDic)
            {
                ALSourceState state = getSourceState(kvp.Key);
                if (state == ALSourceState.Playing)
                {
                    Pause(kvp.Key);
                }
            }
        }

        /// <summary>
        /// SetSourceVolume.
        /// <summary>
        /// <param name="key">alias name</param>
        /// <param name="vol">volume</param>
        public void SetSourceVolume(string key, float vol)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            if (clipDic.ContainsKey(key))
            {
                AL.Source(clipDic[key].source, ALSourcef.Gain, vol);
            }
            //AL.Source(clipList[idx].source, ALSourcef.Gain, vol);
        }

        /// <summary>
        /// GetSourceVolume.
        /// <summary>
        /// <param name="key">alias name</param>
        /// <return>volume value</return>
        public float GetSourceVolume(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            float vol;
            if (!clipDic.ContainsKey(key))
            {
                throw new KeyNotFoundException(key);
            }
            AL.GetSource(clipDic[key].source, ALSourcef.Gain, out vol);
            //AL.GetSource(clipList[idx].source, ALSourcef.Gain, out vol);
            return vol;
        }

        /// <summary>
        /// SetMasterVolume.
        /// <summary>
        /// <param name="vol">volume</param>
        public void SetMasterVolume(float vol)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            AL.Listener(ALListenerf.Gain, vol);
        }

        /// <summary>
        /// GetMasterVolume.
        /// <summary>
        /// <return>volume value</return>
        public float GetMasterVolume()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            float vol;
            AL.GetListener(ALListenerf.Gain, out vol);
            return vol;
        }

        /// <summary>
        /// GetClipCount.
        /// <summary>
        /// <return>managed audio count</return>
        public int GetClipCount()
        {
            return clipDic.Count;
        }

        /// <summary>
        /// RemoveAudioClip.
        /// <summary>
        /// <param name="key">alias name</param>
        public void RemoveAudioClip(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            ALSourceState state = getSourceState(key);
            if (state == ALSourceState.Playing)
            {
                Stop(key);
            }
            if (clipDic.ContainsKey(key))
            {
                AL.DeleteSource(clipDic[key].source);
                AL.DeleteBuffer(clipDic[key].buffer);
                clipDic.Remove(key);
            }
        }

        /// <summary>
        /// RemoveAudioClip. delete all audios.
        /// <summary>
        public void RemoveAudioClip()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            foreach (KeyValuePair<string, AudioClip> kvp in clipDic)
            {
                ALSourceState state = getSourceState(kvp.Key);
                if (state == ALSourceState.Playing)
                {
                    Stop(kvp.Key);
                }
                AL.DeleteSource(clipDic[kvp.Key].source);
                AL.DeleteBuffer(clipDic[kvp.Key].buffer);
            }
            clipDic.Clear();
        }

        /// <summary>
        /// getSourceState.
        /// <summary>
        /// <param name="key">alias name</param>
        /// <return>audio status</return>
        private ALSourceState getSourceState(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("AudioSource is alreay disposed");
            }
            int state;
            if (!clipDic.ContainsKey(key))
                throw new KeyNotFoundException(key);
            
            AL.GetSource(clipDic[key].source, ALGetSourcei.SourceState, out state);
            return (ALSourceState)state;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    RemoveAudioClip();
                    ALC.DestroyContext(context);
                    ALC.CloseDevice(device);
                }
                disposed = true;
            }
        }

        ~AudioSource()
        {
            Dispose(false);
        }

    }
}
