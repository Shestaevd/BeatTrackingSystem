using BeatSystem.scripts.BeatSystem.Domain.Properties;
using Godot;
using System;

namespace BeatSystem.scripts.BeatSystem.Domain.Track
{
    internal record GodotTrack(AudioStream Stream) : HasBpm, HasFullLength
    {
        double Length = Stream.GetLength();
        public int GetBpm()
        {
            return Convert.ToInt32(Stream._GetBpm());
        }

        public double GetFullLength()
        {
            return Length;
        }
    }
}
