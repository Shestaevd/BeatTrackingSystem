using BeatSystem.scripts.BeatSystem.Domain.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBeat.scripts.BeatSystem.BeatTrackingSystem.Domain.Utils
{
    internal class Utils
    {
        public static double FindInterval(double fullLength, int bpm)
        {
            double bps = Math.Round(bpm / 60d, 1);
            double bpt = Math.Round(fullLength * bps, 1);
            return Math.Round(fullLength / bpt, 1);
        }

        public static double FindTargetBeat(double position, double interval)
        {
            double halfInterval = interval / 2;
            double fromLeftPosition = Math.Round(position % interval, 1);
            double fromRightPosition = Math.Round(interval - fromLeftPosition, 1);
            double fromAbsolutePosition = fromLeftPosition > fromRightPosition ? fromLeftPosition : fromRightPosition;
            return Math.Round((fromAbsolutePosition - halfInterval) / halfInterval, 2);
        }
    }
}
