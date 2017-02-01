using Microsoft.Xna.Framework;

namespace Breakout.Engine.Core {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public abstract class GameImpl : Game {
        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/
        public virtual void init() { }
        public virtual void update(float t, float dt) { }
		public virtual void setTitle(string title) {
			Window.Title = title;
		}
    }

}