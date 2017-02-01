/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using Breakout.Engine.Components;
using Breakout.Engine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Breakout.Entities {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/

    public static class Player {
        public static Component[] createComponents() {
            return new Component[] {
                new BoundingRectangle() { height = 1, width = Engine.Engine.getInst().Content.Load<Texture2D>("player").Width },
				new Position() { x = Engine.Engine.getInst().GraphicsDevice.Viewport.Width * 0.5f, y = Engine.Engine.getInst().GraphicsDevice.Viewport.Height - 25 },
                new Score() { },
                new Sprite() { texture = Engine.Engine.getInst().Content.Load<Texture2D>("player_black") },
                new Velocity() { x = 0, y = 0 },
				new Angle() { angle = 0.0f },
				new AngularVelocity { velocity = 0.0f }
            };
        }
    }
}
