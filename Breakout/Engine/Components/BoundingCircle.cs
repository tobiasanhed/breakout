/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using Breakout.Engine.Core;

namespace Breakout.Engine.Components {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class BoundingCircle : Component {
        /*-------------------------------------
         * PUBLIC PROPERTIES
         *-----------------------------------*/
        public float x;
        public float y;
        public float radius;

        /*-------------------------------------
         * CONSTRUCTORS
         *-----------------------------------*/
        public BoundingCircle(float x, float y, float radius) {
            this.x      = x;
            this.y      = y;
            this.radius = radius;
        }

        public BoundingCircle() { }
    }
}
