/*-------------------------------------
 * USINGS
 *-----------------------------------*/

using Breakout.Engine.Components;
using Breakout.Engine.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Breakout.Engine.Managers {

    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class PhysicsManager : Manager {
		private int rotation = 0;
        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/
        public override void update(float t, float dt) {
            var entities = Engine.getInst().entities;

			foreach(var entity in entities.Values){
				var br = entity.getComponent<BoundingRectangle>();
				if (br != null) br.collided = false;
			}

            foreach (var entity in entities.Values) {
                var position 		= entity.getComponent<Position>();
                var velocity 		= entity.getComponent<Velocity>();
                var br       		= entity.getComponent<BoundingRectangle>();
                var score    		= entity.getComponent<Score>();
                var sprite  		= entity.getComponent<Sprite>();
				var angle 	 		= entity.getComponent<Angle>();
				var angularVelocity = entity.getComponent<AngularVelocity>();

				if(score != null && angularVelocity.velocity != 0.0f){
					if (angle.angle < -0.3f || angle.angle > 0.3f)
						angularVelocity.velocity *= -1;
					if((rotation < 0 && angle.angle > 0) || (rotation > 0 && angle.angle < 0)){
						angularVelocity.velocity = 0.0f;
						angle.angle = 0.0f;
						rotation = 0;
					}
						
				}

				if(angle != null && angularVelocity != null){
					angle.angle += angularVelocity.velocity;
				}

                if (velocity != null) {
					updateVelocity(dt, entity, velocity, position, score, sprite, entities);
                }
                
				if (br != null /*|| (velocity == null || score != null)*/) {
                    br.x = position.x - (sprite.texture.Width * 0.5f);
                    br.y = position.y - (sprite.texture.Height * 0.5f);
                }/*else if(br != null) {
                    br.x = position.x;
                    br.y = position.y;
                }*/          

                foreach (var entity2 in entities.Values) {
                    if (entity2.id == entity.id || velocity == null || score != null) continue; // Kollar bara vad bollen kolliderar med...
                    var br2  = entity2.getComponent<BoundingRectangle>();
                    var pos2 = entity2.getComponent<Position>();
                    var spr2 = entity2.getComponent<Sprite>();

                    br2.x = pos2.x - (spr2.texture.Width * 0.5f);
                    br2.y = pos2.y - (spr2.texture.Height * 0.5f);

					if (IntersectPixels(new Rectangle((int)br.x, (int)br.y, br.width, br.height), sprite,
					                    new Rectangle((int)br2.x, (int)br2.y, br2.width, br2.height), spr2) && (!br.collided && !br2.collided)) {
						solveCollision(dt, entity, entity2, velocity, position, sprite, pos2, spr2, br2);
						br.collided = true;
						br2.collided = true;
						// Make particle effect.
                    }
                }
            }
			clearOldEntites(entities);

		}

        /*-------------------------------------
         * PRIVATE METHODS
         *-----------------------------------*/

		private void solveCollision(float dt, Entity entity, Entity entity2, Velocity velocity, Position position, Sprite sprite, Position pos2, Sprite spr2, BoundingRectangle br2){
			var managers = Engine.getInst().managers;
			ParticleManager pm = null;
			for (int i = 0; i < managers.Count; i++)
			{
				if (managers[i].GetType().ToString() == "Breakout.Engine.Managers.ParticleManager")
				{
					pm = (ParticleManager)managers[i];
				}
			}
			BrickStatus brickStatus = entity2.getComponent<BrickStatus>();
			if (brickStatus != null){
				if (!brickStatus.alive) return;
				// Kollision med brick
				float x = position.x;
				float y = position.y;
				if (position.x < br2.x - (sprite.texture.Width * 0.5f) || position.x > br2.x + br2.width + (sprite.texture.Width * 0.5f))
				{
					velocity.x *= -1;
					x = (position.x < br2.x - (sprite.texture.Width * 0.5f)) ? x - (sprite.texture.Width * 0.5f) : x + (sprite.texture.Width * 0.5f);
				}
				else {
					velocity.y *= -1;
					y = (position.y < br2.y - (sprite.texture.Height * 0.5f)) ? y - (sprite.texture.Height * 0.5f) : y + (sprite.texture.Height * 0.5f);
				}

				entity2.getComponent<BrickStatus>().alive = false;
				for (int j = 0; j < 100; j++){
					pm.generateNewParticle(new Vector2(x, y));
				}
			} else { // Kollision med paddlee
				if ((position.x < br2.x - (sprite.texture.Width * 0.5f) || position.x > br2.x + br2.width + (sprite.texture.Width * 0.5f)) && position.y > br2.y + (sprite.texture.Height * 0.5f))
				{
					velocity.x *= -1;
				}
				else {
					// Pythagoras på X och Y hastigheten för att räkna ut totala hastigheten.
					float speedXY = (float)Math.Sqrt((velocity.x * velocity.x) + (velocity.y * velocity.y));

					// Räkna ut bollens position relativt center av paddlen, resultat är emellan -1 och +1.
					float posX = (position.x - pos2.x) / (spr2.texture.Width * 0.5f);

					// Värde mellan 0 och 1 för hur mycket påverkan det ska ha på x-hastighet.
					float influenceX = 0.5f;

					// Räkna ut nya X-hastigheten baserat på vart den träffar paddlen, 
					// gör den även relativ till originalhastighet och tweaka efter influenceX ovan.
					velocity.x = speedXY * posX * influenceX;

					// Baserat på nya x-hastigheten, räkna ut nya y-hastigheten så den totala hastigheten blir samma
					// som innan. Återigen pythagoras sats.
					velocity.y = (float)Math.Sqrt((speedXY * speedXY) - (velocity.x * velocity.x)) *
						(velocity.y > 0 ? -1 : 1);

					// Add some bounce on paddle
					var velocityPaddle = entity2.getComponent<Velocity>();
					velocityPaddle.y += 300.0f;

					//Angle angle = entity.getComponent<Angle>();
					AngularVelocity av = entity.getComponent<AngularVelocity>();

					// Spin ball
					av.velocity = 0.03f * posX;

					// Hit tilt effect
					AngularVelocity avPaddle = entity2.getComponent<AngularVelocity>();
					if (position.x < pos2.x - 15.0f || position.x > pos2.x + 15.0f){
						rotation = position.x < pos2.x ? -1 : 1;
						avPaddle.velocity = rotation < 0 ? -0.01f : 0.01f;
					}
				}
			}

			// "Clamp"
			if (((br2.x - (sprite.texture.Width * 0.5f) + 5) < position.x && position.x < (br2.x + br2.width + (sprite.texture.Width * 0.5f) - 5)) &&
					((br2.y - (sprite.texture.Height * 0.5f) + 5) < position.y && position.y < (br2.y + br2.height + (sprite.texture.Height * 0.5f) - 5)))
			{
				if (Math.Abs(position.x - (br2.x + br2.width + (sprite.texture.Width * 0.5f))) < Math.Abs(position.x - (br2.x - (sprite.texture.Width * 0.5f))))
					position.x = (br2.x + br2.width + (sprite.texture.Width * 0.5f)) + 1;
				else
					position.x = (br2.x - (sprite.texture.Width * 0.5f)) - 1;
			}
		}

		private void updateVelocity(float dt, Entity entity, Velocity velocity, Position position, Score score, Sprite sprite, Dictionary<int, Entity> entities){
            position.y += velocity.y * dt;
			position.x += velocity.x * dt;

			// Fix paddle position to prevent drifting away.
			if(score != null && position.y < Engine.getInst().GraphicsDevice.Viewport.Height - 25){
				position.y = Engine.getInst().GraphicsDevice.Viewport.Height - 25;
				velocity.y = 0.0f;
			}
				

			// Hit top
			if (position.y - (sprite.texture.Height * 0.5f) < 0) 
				velocity.y *= -1;

			// Hit sides
			if (position.x > Engine.getInst().GraphicsDevice.Viewport.Width - (sprite.texture.Width * 0.5f) ||
				position.x - (sprite.texture.Width * 0.5f) < 0) 
				velocity.x *= -1;

			// "Gravity" on paddle speed
			if (score != null)
				velocity.x += ((velocity.x * -1) * dt) * 1.5f; 

			// Hit bottom
			if (score == null && position.y > Engine.getInst().GraphicsDevice.Viewport.Height - (sprite.texture.Height * 0.5f))
			{
				Removable remove = entity.getComponent<Removable>();
				remove.remove = true;
				foreach (var ent in entities.Values)
				{
					var sco = ent.getComponent<Score>();
					if (sco != null)
					{
						sco.gameOver = true;
						break;
					}

				}
			}else if(position.y > Engine.getInst().GraphicsDevice.Viewport.Height - (sprite.texture.Height * 0.5f)){
				velocity.y *= -1;
			}

			// Clamp in view

			position.x = MathHelper.Clamp(position.x,
										  (sprite.texture.Width * 0.5f),
										  Engine.getInst().GraphicsDevice.Viewport.Width - (sprite.texture.Width * 0.5f));
			position.y = MathHelper.Clamp(position.y,
										  (sprite.texture.Height * 0.5f),
										  Engine.getInst().GraphicsDevice.Viewport.Height - (sprite.texture.Height * 0.5f));

		}

        private bool IntersectPixels(Rectangle rectangleA, Sprite sprite,
                                    Rectangle rectangleB, Sprite spr2) {

			Color[] dataA = new Color[rectangleA.Width * rectangleA.Height];
			sprite.texture.GetData(dataA);
			Color[] dataB = new Color[spr2.texture.Width * spr2.texture.Height];
			spr2.texture.GetData(dataB);

            if (!rectangleA.Intersects(rectangleB))
                return false;

            Rectangle its = Rectangle.Intersect(rectangleA, rectangleB);

            // Check every point within the intersection bounds
            for (int y = its.Top; y < its.Bottom; y++) {
                for (int x = its.Left; x < its.Right; x++) {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0) {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

		private void clearOldEntites(Dictionary<int, Entity> entities){
			int[] indexToRemove = new int[entities.Count];
			int i = 0;
			foreach(var entity in entities.Values){
				Removable remove = entity.getComponent<Removable>();
				if(remove != null && remove.remove){
					indexToRemove[i++] = entity.id;
				}
			}


			for (i = 0; i < indexToRemove.Length; i++)
			{
				entities.Remove(indexToRemove[i]);
			}
		}
    }
}
