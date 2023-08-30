using BeatSystem.scripts.BeatSystem.Domain.Properties;
using NAudio.Wave;

namespace BeatSystem.scripts.BeatSystem.Domain.System.AudioLoader
{
    public interface IAudioLoader<T> where T : HasBpm, HasFullLength
    {
        public abstract T Load(string path, int bpm, float volume = 1f);
    }
}
