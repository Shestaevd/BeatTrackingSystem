using BeatSystem.scripts.BeatSystem.Domain.System;
using BeatSystem.scripts.BeatSystem.Domain.Track;
using NAudio.Wave;
using System;

namespace BeatSystem.scripts.BeatSystem.Logic
{
    public class NAudioBeatSystem : IAudioBeatSystem<Track>
    {
        private Track _track;
        private double _interval;
        private WaveOutEvent _player = new WaveOutEvent();

        public NAudioBeatSystem(Track track) 
        {
            double bps = Math.Round(track.GetBpm() / 60d, 1);
            double bpt = Math.Round(track.GetFullLength() * bps, 1);
            _interval  = Math.Round(track.GetFullLength() / bpt, 1);
            _track = track;
            _player.Init(track.Stream);
        }

        public double GetInterval()
        {
            return _interval;
        }

        public double GetPlaybackPosition()
        {
            return Math.Round(Convert.ToDouble(_player.GetPosition()) / _track.Stream.WaveFormat.AverageBytesPerSecond, 1);
        }

        public double InTargetOfBeat()
        {
            double position = GetPlaybackPosition();
            double halfInterval = _interval / 2;
            double fromLeftPosition = Math.Round(position % _interval, 1);
            double fromRightPosition = Math.Round(_interval - fromLeftPosition, 1);
            double fromAbsolutePosition = fromLeftPosition > fromRightPosition ? fromLeftPosition : fromRightPosition;
            return Math.Round((fromAbsolutePosition - halfInterval) / halfInterval, 2);
        }

        public void Pause()
        {
            _player.Pause();
        }

        public void Play()
        {
            _player.Play();
        }

        public void Stop()
        {
            _player.Stop();
        }

        public void SetPlaybackPosition(long position)
        {
            _track.Stream.Position = (long)(_track.GetFullLength() - (position * _track.Stream.WaveFormat.AverageBytesPerSecond));
        }

        public void SetVolume(double volume)
        {
            _track.Stream.Volume = (float) volume;
        }

        public double UntilNextBeat()
        {
            return _interval - (GetPlaybackPosition() % _interval);
        }
    }
}
