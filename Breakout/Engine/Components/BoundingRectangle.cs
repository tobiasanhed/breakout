/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using Breakout.Engine.Core;

namespace Breakout.Engine.Components {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class BoundingRectangle : Component {
        /*-------------------------------------
         * PUBLIC PROPERTIES
         *-----------------------------------*/
        public float x;
        public float y;
        public int height;
        public int width;
		public bool collided = false;

        /*-------------------------------------
         * CONSTRUCTORS
         *-----------------------------------*/
        public BoundingRectangle(float x, float y, int height, int width) {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
        }

        public BoundingRectangle() { }
    }
}
