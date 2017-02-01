/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using System;
using Breakout.Engine.Components;
using Breakout.Engine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Breakout.Entities {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/

    public static class Brick {
		private static Random random = new Random();
        public static Component[] createComponents() {
            return new Component[] {
                new BoundingRectangle() { height = Engine.Engine.getInst().Content.Load<Texture2D>("brick").Height, width = Engine.Engine.getInst().Content.Load<Texture2D>("brick").Width},
                new Position() { },
                new Sprite() { texture = Engine.Engine.getInst().Content.Load<Texture2D>("brick_white")},
                new BrickStatus() { },
				new BrickColor(random) { },
				new Removable() { }
            };
        }
    }
}
