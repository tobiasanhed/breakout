using System;
using System.Collections.Generic;
using Breakout.Engine.Components;
using Breakout.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Engine.Managers
{
	public class ParticleManager : Manager
	{
		Random random = new Random();
		Texture2D particleTexture = Engine.getInst().Content.Load<Texture2D>("particle");
		private List<Particle> particles = new List<Particle>();

		public override void update(float t, float dt)
		{
			for (int i = 0; i < particles.Count; i++){
				particles[i].TTL--;
				particles[i].Position += particles[i].Velocity;
				particles[i].Angle += particles[i].AngularVelocity;

				if(particles[i].TTL <= 0){
					particles.RemoveAt(i);
					i--;
				}
			}
		}

		public void drawParticles(float t, float dt, SpriteBatch spriteBatch){
			for (int i = 0; i < particles.Count; i++)
			{
				Rectangle sourceRectangle = new Rectangle(0, 0, particles[i].Texture.Width, particles[i].Texture.Height);
				Vector2 origin = new Vector2(particles[i].Texture.Width * 0.5f, particles[i].Texture.Height * 0.5f);
				spriteBatch.Draw(particles[i].Texture, particles[i].Position, sourceRectangle, particles[i].Color,
								 particles[i].Angle, origin, particles[i].Size, SpriteEffects.None, 0.0f);
			}
		}

		public void generateNewParticle(Vector2 position){
			Texture2D texture = particleTexture;
			Vector2 velocity = new Vector2(
				1f * (float)(random.NextDouble() * 2 - 1),
				1f * (float)(random.NextDouble() * 2 - 1));
			float angle = 0;
			float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
			Color color = new Color(
				(float)random.NextDouble(),
				(float)random.NextDouble(),
				(float)random.NextDouble());
			float size = (float)random.NextDouble();
			int ttl = 500 + random.Next(500);

			particles.Add(new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl));
		}
	}
}