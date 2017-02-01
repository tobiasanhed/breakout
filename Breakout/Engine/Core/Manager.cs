namespace Breakout.Engine.Core {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public abstract class Manager {

        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/
        public virtual void draw(float t, float dt) { }
        public virtual void update(float t, float dt) { }
    }
}