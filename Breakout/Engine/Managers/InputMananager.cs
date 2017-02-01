/*-------------------------------------
 * USINGS
 *-----------------------------------*/

using Breakout.Engine.Components;
using Breakout.Engine.Core;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Engine.Managers {

    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class InputMananager : Manager {

        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/
        public override void update(float t, float dt) {
            var entities = Engine.getInst().entities;

            foreach (var entity in entities.Values) {
                var score    = entity.getComponent<Score>();
                var position = entity.getComponent<Position>();
                var velocity = entity.getComponent<Velocity>();

                if (score != null) {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) && position.x < Engine.getInst().GraphicsDevice.Viewport.Width) velocity.x -= 600.0f * dt;
                    if (Keyboard.GetState().IsKeyDown(Keys.Right) && position.x > 0) velocity.x += 600.0f * dt;
                }
            }
        }
    }
}
