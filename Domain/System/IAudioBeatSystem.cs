using BeatSystem.scripts.BeatSystem.Domain.Properties;

namespace BeatSystem.scripts.BeatSystem.Domain.System
{
    public interface IAudioBeatSystem<T> where T : HasBpm, HasFullLength
    {
        public double GetPlaybackPosition();
        public void SetPlaybackPosition(long position);
        public void Play();
        public void Stop();
        public void Pause();
        public void SetVolume(double volume);
        public double InTargetOfBeat();
        public double GetInterval();
        public double UntilNextBeat();

    }
}
