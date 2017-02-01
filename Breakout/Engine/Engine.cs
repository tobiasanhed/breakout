/*-------------------------------------
 * USINGS
 *-----------------------------------*/

using Breakout.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Breakout.Engine {
    /*-------------------------------------
    * CLASSES
    *-----------------------------------*/

    public class Engine : Game
    {

        /*-------------------------------------
        * PRIVATE FIELDS
        *-----------------------------------*/

        private static Engine inst;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public GameImpl gameImpl;

        /*-------------------------------------
         * PUBLIC PROPERTIES
         *-----------------------------------*/

        public int nextEntityID = 1;
        public Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
        public List<Manager> managers = new List<Manager>();

        /*-------------------------------------
         * CONSTRUCTORS
         *-----------------------------------*/

        public Engine(GameImpl gameImpl) {
			graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			graphics.SynchronizeWithVerticalRetrace = false;
			base.IsFixedTimeStep = false;

            this.gameImpl = gameImpl;
        }

        /*-------------------------------------
         * PROTECTED METHODS
         *-----------------------------------*/

        protected override void Initialize() {
            gameImpl.init();
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent() {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {
            float t  = (float)gameTime.TotalGameTime.TotalSeconds;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var system in managers)
                system.update(t, dt);

            gameImpl.update(t, dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            float t  = (float)gameTime.TotalGameTime.TotalSeconds;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var system in managers)
                system.draw(t, dt);

            base.Draw(gameTime);
        }

        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/

        public static void run(GameImpl gameImpl) {
            inst = new Engine(gameImpl);
            inst.Run();
        }

        public static Engine getInst() {
            return inst;
        }
        
        public Entity addEntity(Component[] components) {
            Entity entity = new Entity();

            foreach (var component in components)
                entity.components.Add(component.GetType(), component);

            entity.id = nextEntityID++;

            this.entities.Add(entity.id, entity);

            return entity;
        }

        public void removeEntity(Entity entity) {
            entities.Remove(entity.id);
        }

		public void clearEntities(){
			entities.Clear();
		}
    }
}
