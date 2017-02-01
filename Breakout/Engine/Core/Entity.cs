/*-------------------------------------
 * USINGS
 *-----------------------------------*/
using System;
using System.Collections.Generic;

namespace Breakout.Engine.Core {
    /*-------------------------------------
     * CLASSES
     *-----------------------------------*/
    public class Entity {

        /*-------------------------------------
         * PUBLIC PROPERTIES
         *-----------------------------------*/
        public int id;
        public Dictionary<Type, Component> components = new Dictionary<Type, Component>();

        /*-------------------------------------
         * PUBLIC METHODS
         *-----------------------------------*/
        public T getComponent<T>() where T : Component {
            Component component;
            components.TryGetValue(typeof(T), out component);

            if (component == null)
                return null;
            else
                return (T)component;
        }
    }
}