using System.Collections.Generic;
using System.Linq;
using Breakout.Engine.Core;

namespace Breakout.Engine.Components
{
	public class FPSCounter : Component
	{
		public long totalFrames { get; private set; }
		public float totalSeconds { get; private set; }
		public float averageFramesPerSecond { get; private set; }
		public float currentFramesPerSecond { get; private set; }

		public const int MAXIMUM_SAMPLES = 100;

		private Queue<float> _sampleBuffer = new Queue<float>();

		public void update(float deltaTime)
		{
			currentFramesPerSecond = 1.0f / deltaTime;

			_sampleBuffer.Enqueue(currentFramesPerSecond);

			if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
			{
				_sampleBuffer.Dequeue();
				averageFramesPerSecond = _sampleBuffer.Average(i => i);
			}
			else
			{
				averageFramesPerSecond = currentFramesPerSecond;
			}

			totalFrames++;
			totalSeconds += deltaTime;
		}
	}
}
