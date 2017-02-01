/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using Breakout.Engine.Core;

namespace Breakout.Engine.Components {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class Score : Component {
        /*-------------------------------------
         * PUBLIC PROPERTIES
         *-----------------------------------*/
        public float score = 0.0f;
		public bool gameOver = false;
    }
}
