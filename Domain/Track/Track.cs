using BeatSystem.scripts.BeatSystem.Domain.Properties;
using NAudio.Wave;
using System;

namespace BeatSystem.scripts.BeatSystem.Domain.Track
{
    public record Track(WaveChannel32 Stream, int Bpm) : HasBpm, HasFullLength
    {
        double FullLength = Convert.ToDouble(Stream.Length) / Stream.WaveFormat.AverageBytesPerSecond;
        public double GetFullLength()
        {
            return FullLength;
        }

        public int GetBpm()
        {
            return Bpm;
        }
    }
}
