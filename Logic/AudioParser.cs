using BeatSystem.scripts.BeatSystem.Domain.Track;
using Godot;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace BeatSystem.scripts.BeatSystem.Logic
{
	// Just funny script to split track by bpm and make a lot of WaveStreams out of it
	public class AudioParser
	{

		private Dictionary<string, List<WaveStream>> _parsedAudio = new Dictionary<string, List<WaveStream>>();

		public Dictionary<string, List<WaveStream>> ParsedAudio
		{
			get { return _parsedAudio; }
			set { _parsedAudio = value; }
		}


		public AudioParser Parse(string name, Track origin) 
		{
			WaveStream ws = origin.Stream;
			WaveFormat wf = origin.Stream.WaveFormat;
			double bps = Math.Round(origin.Bpm / 60d, 1);

			int bytesPerSecond = wf.AverageBytesPerSecond;
			int bytesPerBeat = Convert.ToInt32(bytesPerSecond / bps);

			for (int i = 0; i < ws.Length / bytesPerBeat; i++)
			{
				byte[] bytes = new byte[bytesPerBeat];
				writePartToCache(name, bytes, ws, wf);
			}

			if (ws.Position != ws.Length)
			{
				long remains = ws.Length - ws.Position;
				byte[] bytes = new byte[remains];
				writePartToCache(name, bytes, ws, wf);
			}

			return this;
		}

		private void writePartToCache(string name, byte[] bytes, WaveStream ws, WaveFormat wf) 
		{
			ws.Read(bytes, 0, bytes.Length);
			MemoryStream newMemoryStream = new MemoryStream(bytes);

			RawSourceWaveStream rsws = new RawSourceWaveStream(newMemoryStream, wf);

			List<WaveStream> wss;
			if (_parsedAudio.TryGetValue(name, out wss))
			{
				wss.Add(rsws);
			}
			else
			{
				wss = new List<WaveStream> { rsws };
				_parsedAudio.Add(name, wss);
			}
		}

	}
}
