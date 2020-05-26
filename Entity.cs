/*
世间万物都有粒子组成=>所有对象都是粒子=>所有都是实体
*/
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

namespace EntityFrameWork
{
    public class Entity : IDisponse {

        List<Component> _components = new List<Component>();
        protected World _world;
        public Entity(World w)
        {
            _world = w;
            _world.AddEntity(this);
            
         
        }

        public virtual void Disponse()
        {
            foreach(var component in _components)
            {
                component.Disponse();
                OnRemove(component, 0);
            }
            _world.RemoveEntity(this);
            _world = null;
        
        }
        public Component AddComponent<T>() where T: Component
        {
          
            
            var c = global::System.Activator.CreateInstance(typeof(T)) as Component;

            _components.Add(c);

           OnAwake(c, 0);

            return c;
        }

        public Component AddComponent<T, W>(W w) where T: Component
        {
            var c = global::System.Activator.CreateInstance(typeof(T)) as Component;

            _components.Add(c);    
            OnAwake(c, w);
            return c;
        }


        public void RemoveComponent<T>(Component c, T arg = default(T))
        {
            _components.Remove(c);
            c.Disponse();
           
            OnRemove(c, arg);
           
        }

        public Component[] GetComponents<T>()
        {

             var result = from s  in _components where s.GetType() == typeof(T) select s;

             return result.ToArray();
        }


        void OnAwake<T>(Component c, T arg = default(T))
        {
             var systems = _world.GetInterstSystems(c);

            if(null != systems)
            {
                foreach(var system in systems)
                {
                     system.OnAwake(this, c, arg);
                       
                }
            }

        }

        
        void OnRemove<T>(Component c, T arg = default(T))
        {
             var systems = _world.GetInterstSystems(c);

            if(null != systems)
            {
                foreach(var system in systems)
                {
                     system.OnRemove(c, arg);
                       
                }
            }
        }

     

    }
}