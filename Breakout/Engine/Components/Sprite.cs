/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using Breakout.Engine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Engine.Components {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class Sprite : Component {
        /*-------------------------------------
         * PUBLIC PROPERTIES
         *-----------------------------------*/
        public Texture2D texture;

        /*-------------------------------------
         * CONSTRUCTORS
         *-----------------------------------*/
        public Sprite(Texture2D texture) {
            this.texture = texture;
        }

        public Sprite() { }
    }
}
