/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using Breakout.Engine.Core;

namespace Breakout.Engine.Components {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class Position : Component {
        /*-------------------------------------
         * PUBLIC PROPERTIES
         *-----------------------------------*/
        public float x;
        public float y;

        /*-------------------------------------
         * CONSTRUCTOR
         *-----------------------------------*/
        public Position(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public Position() { }
    }
}
