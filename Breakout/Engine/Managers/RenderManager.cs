using Breakout.Engine.Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Breakout.Engine.Components;

namespace Breakout.Engine.Managers {

    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/

    public class RenderManager : Manager {

		/*-------------------------------------
         * PRIVATE FIELDS
         *-----------------------------------*/
		private SpriteFont scoreFont;
        private SpriteBatch spriteBatch;
		private FPSCounter fpsCounter;

        /*-------------------------------------
         * CONSTRUCTORS
         *-----------------------------------*/

        public RenderManager(SpriteBatch spriteBatch) {
			fpsCounter = new FPSCounter();
			this.spriteBatch = spriteBatch;
			scoreFont = Engine.getInst().Content.Load<SpriteFont>("score");
        }

        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/

        public override void draw(float t, float dt) {
            var entities = Engine.getInst().entities;
			spriteBatch.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

			fpsCounter.update(dt);
			spriteBatch.DrawString(scoreFont, string.Format("FPS: {0}", fpsCounter.currentFramesPerSecond), 
			                       new Vector2(Engine.getInst().GraphicsDevice.Viewport.Width - 100, 10), Color.Black);

            foreach(var entity in entities.Values) {
                var sprite   	= entity.getComponent<Sprite>();
                var position 	= entity.getComponent<Position>();
                var score    	= entity.getComponent<Score>();
				var brickColor	= entity.getComponent<BrickColor>();
				var angle 		= entity.getComponent<Angle>();

				if (position == null || sprite == null) 
					break;

				if (brickColor != null){
					spriteBatch.Draw(
						sprite.texture,
						origin: new Vector2(sprite.texture.Width / 2, sprite.texture.Height / 2),
						position: new Vector2(position.x, position.y),
						color: brickColor.color
						);
				}else if(angle != null){
					spriteBatch.Draw(
						sprite.texture,
						origin: new Vector2(sprite.texture.Width / 2, sprite.texture.Height / 2),
						position: new Vector2(position.x, position.y),
						color: Color.White,
						rotation: angle.angle
						);
				}else{
					spriteBatch.Draw(
						sprite.texture,
						origin: new Vector2(sprite.texture.Width / 2, sprite.texture.Height / 2),
						position: new Vector2(position.x, position.y),
						color: Color.White
						);
				}
				if(score != null) spriteBatch.DrawString(scoreFont, score.score.ToString(), new Vector2(10, 10), Color.Black);
            }

			var managers = Engine.getInst().managers;
			for (int i = 0; i < managers.Count; i++)
			{
				if (managers[i].GetType().ToString() == "Breakout.Engine.Managers.ParticleManager")
				{
					ParticleManager pm = (ParticleManager)managers[i];
					pm.drawParticles(t, dt, spriteBatch);
				}
			}

            spriteBatch.End();
        }
    }
}
