/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using Breakout.Engine.Managers;
using Breakout.Engine.Core;
using Microsoft.Xna.Framework.Graphics;
using Breakout.Breakout.Entities;
using Breakout.Engine.Components;

namespace Breakout {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/

    public class BreakoutImpl : GameImpl {
        /*-------------------------------------
         * CONSTANTS
         *-----------------------------------*/
        private const int ROWS = 3;
        private const int COLUMNS = 8;
		private int pointsToAdd = 0;
		private int ballsInPlay = 0;
		//private FPSCounter fpsCounter;
        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/
        public override void init() {
            Engine.Engine.getInst().managers.Add(new PhysicsManager());
            Engine.Engine.getInst().managers.Add(new RenderManager(new SpriteBatch(Engine.Engine.getInst().GraphicsDevice)));
            Engine.Engine.getInst().managers.Add(new SceneManager());
            Engine.Engine.getInst().managers.Add(new InputMananager());
			Engine.Engine.getInst().managers.Add(new ParticleManager());

            Engine.Engine.getInst().addEntity(Player.createComponents());
            initBricks();
            Engine.Engine.getInst().addEntity(Ball.createComponents());
			ballsInPlay++;

			//fpsCounter = new FPSCounter();
        }

        public override void update(float t, float dt) {
			//fpsCounter.update(dt);
			//var fps = string.Format("FPS: {0}", fpsCounter.currentFramesPerSecond);
			//setTitle(fps);
			var bricksAlive = 0;

			var entities = Engine.Engine.getInst().entities;
            
			foreach(Entity entity in entities.Values) {
                var brickStatus = entity.getComponent<BrickStatus>();
				var score = entity.getComponent<Score>();
                if (brickStatus != null && !brickStatus.alive) {
					pointsToAdd += 10;
					Removable remove = entity.getComponent<Removable>();
					remove.remove = true;
				}

				if (score != null) {
					score.score += pointsToAdd;
					pointsToAdd = 0;
					if (score.gameOver){
						score.gameOver = false;
						ballsInPlay--;
						if (ballsInPlay == 0){
							resetGame(true);
							score.score = 0.0f;
							break;
						}
					}
				}
            }

			foreach(Entity entity in entities.Values){
				var brickStatus = entity.getComponent<BrickStatus>();
				if(brickStatus != null && brickStatus.alive){
					bricksAlive++;
				}
			}

			if(bricksAlive == 0){
				resetGame(false);
			}
        }

        public void resetGame(bool loser) {
			if (loser){
				Engine.Engine.getInst().clearEntities();
	            Engine.Engine.getInst().addEntity(Player.createComponents());
			}

			initBricks();
			Engine.Engine.getInst().addEntity(Ball.createComponents());
			ballsInPlay++;
        }

        /*-------------------------------------
         * PRIVATE METHODS
         *-----------------------------------*/
        private void initBricks() {
            for(int i = 0; i < ROWS; i++) {
                for(int j = 0; j < COLUMNS; j++) {
                    var brick = Brick.createComponents();
                    brick.SetValue(new Position(((Engine.Engine.getInst().GraphicsDevice.Viewport.Width / (COLUMNS + 1)) * (j + 1)), (i + 1) * 40.0f), 1);
     				
                    Engine.Engine.getInst().addEntity(brick);
                }
            }
        }
    }
}
