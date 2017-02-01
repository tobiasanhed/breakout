using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Breakout.Engine.Core;
using System;

namespace Breakout.Engine.Components
{
		public class BrickColor : Component
	{
		public Color color;

		public BrickColor(Random random)
		{
			color = new Color(
				(float)random.NextDouble(),
				(float)random.NextDouble(),
				(float)random.NextDouble());
			
		}
	}
}
