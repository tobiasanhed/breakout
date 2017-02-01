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

    public static class Ball {
        public static Component[] createComponents() {
            return new Component[] {
                //new BoundingCircle() { radius = Engine.Engine.getInst().Content.Load<Texture2D>("ball").Width / 2 },
                new BoundingRectangle() { height = Engine.Engine.getInst().Content.Load<Texture2D>("ball_square").Height, width = Engine.Engine.getInst().Content.Load<Texture2D>("ball_square").Width},
                new Position() { x = Engine.Engine.getInst().GraphicsDevice.Viewport.Width * 0.5f, y = Engine.Engine.getInst().GraphicsDevice.Viewport.Height * 0.8f },
                new Sprite() { texture = Engine.Engine.getInst().Content.Load<Texture2D>("ball_square") },
                new Velocity() { x = 0.0f, y = -300.0f },
				new Angle() { angle = 0.0001f },
				new AngularVelocity() { velocity = 0.0f },
				new Removable() { }
            };
        }
    }
}
